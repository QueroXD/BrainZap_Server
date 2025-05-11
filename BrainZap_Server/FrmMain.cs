using BrainZap_Server.CLASSES;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BrainZap_Server
{
    public partial class FrmMain : Form
    {
        private ClSocketServidor socketServidor;
        private ClPreguntas gestorPreguntas;
        private string rutaPreguntas = "preguntas.json"; // Asegúrate de incluir este archivo

        public FrmMain()
        {
            InitializeComponent();
            InicializarServidor();
        }

        private void InicializarServidor()
        {
            socketServidor = new ClSocketServidor();
            socketServidor.Log = Log;
            socketServidor.ActualizarJugadoresUI = ActualizarListaJugadores;
            socketServidor.MensajeRecibido += ProcesarMensaje;
        }

        private void btnEscuchar_Click(object sender, EventArgs e)
        {
            string ip = ObtenerIPLocal();
            lblIp.Text = $"IP Servidor: {ip}";
            socketServidor.Iniciar(ip, 5000);
        }

        private void btnIniciarPartida_Click(object sender, EventArgs e)
        {
            gestorPreguntas = new ClPreguntas(rutaPreguntas);
            EnviarSiguientePregunta();
        }

        private void EnviarSiguientePregunta()
        {
            var pregunta = gestorPreguntas.ObtenerSiguiente();
            if (pregunta == null)
            {
                string resultadoFinal = "FINPARTIDA|" + string.Join(",", socketServidor.ObtenerJugadores()
                    .OrderByDescending(j => j.Puntos)
                    .Select(j => $"{j.Nickname}:{j.Puntos}"));
                socketServidor.EnviarATodos(resultadoFinal);
                Log("Partida finalizada.");
                return;
            }

            foreach (var j in socketServidor.ObtenerJugadores())
                j.ReiniciarParaNuevaPregunta();

            string mensaje = $"PREGUNTA|{pregunta.Texto}|{string.Join("|", pregunta.Opciones)}";
            socketServidor.EnviarATodos(mensaje);
            Log($"Enviada pregunta: {pregunta.Texto}");

            // Aquí puedes lanzar un timer o esperar respuestas manualmente si no haces temporizador
        }

        private void ProcesarMensaje(string mensaje, TcpClient cliente)
        {
            Invoke(new Action(() =>
            {
                Log($"Recibido: {mensaje}");

                if (mensaje.StartsWith("NICK|"))
                {
                    string nick = mensaje.Substring(5).Trim();
                    bool ok = socketServidor.AgregarJugador(nick, cliente);
                    if (ok)
                        socketServidor.EnviarA(cliente, "OK");
                    else
                        socketServidor.EnviarA(cliente, "ERROR|nickname_duplicado");
                }
                else if (mensaje.StartsWith("RESPUESTA|"))
                {
                    var jugador = socketServidor.GetJugadorPorCliente(cliente);
                    if (jugador == null) return;

                    string[] partes = mensaje.Split('|');
                    if (int.TryParse(partes[1], out int indice))
                    {
                        jugador.UltimaRespuesta = indice;
                        jugador.YaRespondio = true;

                        // Cuando todos respondieron
                        if (socketServidor.ObtenerJugadores().All(j => j.YaRespondio))
                        {
                            EvaluarRespuestasYEnviarResultado();
                            EnviarSiguientePregunta();
                        }
                    }
                }
            }));
        }

        private void EvaluarRespuestasYEnviarResultado()
        {
            var preguntaActual = gestorPreguntas.ObtenerSiguiente(); // ¡Esto AVANZA! => ¡Debes guardar actual!
            gestorPreguntas.Reset(); // Esto es para volver al anterior temporalmente

            var jugadores = socketServidor.ObtenerJugadores();
            var respuestasCorrectas = jugadores
                .Where(j => j.UltimaRespuesta == preguntaActual.Correcta)
                .OrderBy(j => j.UltimaRespuesta) // simulamos orden llegada
                .ToList();

            int puntos = 500;
            foreach (var j in respuestasCorrectas)
            {
                j.SumarPuntos(puntos);
                puntos -= 100;
                if (puntos < 100) break;
            }

            string resultado = $"RESULTADO|{preguntaActual.Correcta}|{respuestasCorrectas.Count}|" +
                string.Join(",", jugadores.OrderByDescending(j => j.Puntos)
                .Select(j => $"{j.Nickname}:{j.Puntos}"));

            socketServidor.EnviarATodos(resultado);
        }

        private void ActualizarListaJugadores()
        {
            lstJugadores.Items.Clear();
            foreach (var j in socketServidor.ObtenerJugadores())
            {
                lstJugadores.Items.Add(j.ToString());
            }
        }

        private void Log(string texto)
        {
            txtLog.AppendText(texto + Environment.NewLine);
        }

        private string ObtenerIPLocal()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }
    }
}
