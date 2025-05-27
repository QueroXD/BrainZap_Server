using BrainZap_Server.FORMULARIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace BrainZap_Server.CLASSES
{
    public class ClSocketServidor
    {
        const string CERTIFICAT = "elMeuCertificat.pfx";
        const string psw = "Educem00.";

        private TcpListener server;
        private List<ClUsuario> jugadores = new List<ClUsuario>();
        private ClPreguntas preguntas;
        private FrmMain _frm;
        private FrmPregunta _frmPregunta;
        private Dictionary<string, List<string>> respuestasPorPregunta = new Dictionary<string, List<string>>();
        private Dictionary<(string preguntaTexto, string nick), string> respuestasIndividuales = new Dictionary<(string preguntaTexto, string nick), string>();

        X509Certificate2 elMeuCertificat = new X509Certificate2(CERTIFICAT, psw);

        public ClSocketServidor(FrmMain frm, List<ClUsuario> jugadoresExternos)
        {
            _frm = frm;
            jugadores = jugadoresExternos;
            preguntas = new ClPreguntas("preguntas.json");
        }

        public void Iniciar()
        {
            server = new TcpListener(IPAddress.Any, 5000); // Siempre puerto 5000
            server.Start();
            _frm.Log("SRV | Servidor iniciado en puerto 5000");

            Thread aceptarClientes = new Thread(() =>
            {
                while (true)
                {
                    TcpClient cliente = server.AcceptTcpClient();
                    SslStream stream = new SslStream(cliente.GetStream(), false, validarCertificat);
                    try
                    {
                        stream.AuthenticateAsServer(elMeuCertificat);
                    }
                    catch (Exception ex)
                    {
                        _frm.Log($"SRV | ERROR autenticando SSL: {ex.Message}");
                        cliente.Close();
                        continue;
                    }

                    _frm.Log($"SRV | Cliente conectado desde {cliente.Client.RemoteEndPoint}");

                    Thread recibir = new Thread(() => RecibirMensaje(cliente, stream));
                    recibir.IsBackground = true;
                    recibir.Start();
                }
            });

            aceptarClientes.IsBackground = true;
            aceptarClientes.Start();
        }

        private void RecibirMensaje(TcpClient cliente, SslStream stream)
        {
            _frm.Log("SRV | Hilo de recepción iniciado.");

            try
            {
                byte[] buffer = new byte[4096];
                StringBuilder mensajeBuilder = new StringBuilder();

                while (true)
                {
                    int bytesLeidos = stream.Read(buffer, 0, buffer.Length);

                    if (bytesLeidos == 0)
                    {
                        // Cliente cerró conexión
                        _frm.Log("SRV | Cliente desconectado.");
                        // Remover jugador de la lista, cerramos stream y cliente
                        RemoverJugadorPorStream(stream);
                        break;
                    }

                    string mensajeParcial = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                    mensajeBuilder.Append(mensajeParcial);

                    string mensaje = mensajeBuilder.ToString().Trim();
                    mensajeBuilder.Clear();

                    if (mensaje.StartsWith("NICK"))
                    {
                        GestionarNick(mensaje, cliente, stream);
                    }
                    else if (mensaje.StartsWith("RESPUESTA"))
                    {
                        _frm.Log($"SRV | Respuesta recibida: {mensaje}");
                        GestionarRespuesta(mensaje);
                    }
                    else
                    {
                        _frm.Log($"SRV | Mensaje desconocido: {mensaje}");
                    }
                }
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV | ERROR al recibir mensaje: {ex.Message}");
                RemoverJugadorPorStream(stream);
            }
            finally
            {
                stream.Close();
                cliente.Close();
            }
        }

        private void RemoverJugadorPorStream(SslStream stream)
        {
            lock (jugadores)
            {
                var jugador = jugadores.FirstOrDefault(j => j.Stream == stream);
                if (jugador != null)
                {
                    jugadores.Remove(jugador);
                    _frm.Log($"SRV | Jugador {jugador.Nickname} removido por desconexión.");
                    _frm.MostrarJugadores();
                }
            }
        }

        private void GestionarNick(string mensaje, TcpClient tcpClient, SslStream stream)
        {
            string[] partes = mensaje.Split('|');
            if (partes.Length != 4)
            {
                _frm.Log("SRV | Formato NICK inválido.");
                return;
            }

            string nick = partes[1];
            string ip = partes[2];
            int puerto = int.Parse(partes[3]);

            lock (jugadores)
            {
                if (jugadores.Any(j => j.Nickname == nick))
                {
                    mensaje = $"NICK|{nick}|ERROR";
                    EnviarPorStream(ip, puerto, mensaje);
                    _frm.Log($"SRV | El nick {nick} ya está en uso.");
                }
                else
                {
                    ClUsuario nuevo = new ClUsuario
                    {
                        Nickname = nick,
                        IP = ip,
                        Puerto = puerto,
                        Stream = stream,
                        Cliente = tcpClient
                    };

                    jugadores.Add(nuevo);
                    mensaje = $"NICK|{nick}|OK";
                    EnviarPorStream(ip, puerto, mensaje);

                    _frm.MostrarJugadores();
                    _frm.Log($"SRV | Jugador registrado: {nick} ({ip}:{puerto})");
                }
            }
        }

        private void GestionarRespuesta(string mensaje)
        {
            string[] partes = mensaje.Split('|');
            if (partes.Length != 4)
            {
                _frm.Log("SRV | Formato RESPUESTA inválido.");
                return;
            }

            string nick = partes[1];
            string textoPregunta = partes[2];
            string respuestaUsuario = partes[3].Trim();

            var jugador = jugadores.FirstOrDefault(j => j.Nickname == nick);
            if (jugador == null)
            {
                _frm.Log($"SRV | Jugador '{nick}' no encontrado.");
                return;
            }

            var pregunta = preguntas.ObtenerPreguntaPorTexto(textoPregunta);
            if (pregunta == null)
            {
                _frm.Log($"SRV | Pregunta no encontrada: {textoPregunta}");
                return;
            }

            lock (respuestasPorPregunta)
            {
                if (!respuestasPorPregunta.ContainsKey(textoPregunta))
                    respuestasPorPregunta[textoPregunta] = new List<string>();

                var lista = respuestasPorPregunta[textoPregunta];

                if (!lista.Contains(nick))
                {
                    lista.Add(nick);
                    respuestasIndividuales[(textoPregunta, nick)] = respuestaUsuario;
                }

                if (lista.Count == jugadores.Count)
                {
                    _frm.Log("SRV | Todos los jugadores han respondido.");
                    _frmPregunta?.ForzarFinDePreguntaDesdeServidor(); // Avisamos al FrmPregunta
                }
            }
        }

        public void EvaluarYEnviarResultados(string textoPregunta)
        {
            var pregunta = preguntas.ObtenerPreguntaPorTexto(textoPregunta);
            if (pregunta == null) return;

            string respuestaCorrecta = preguntas.ObtenerRespuestaCorrecta(pregunta);

            Dictionary<string, (string estado, int puntos)> resultadosPorJugador = new Dictionary<string, (string estado, int puntos)>();

            lock (respuestasPorPregunta)
            {
                var lista = respuestasPorPregunta.ContainsKey(textoPregunta)
                    ? respuestasPorPregunta[textoPregunta]
                    : new List<string>();

                foreach (var jugador in jugadores)
                {
                    string nick = jugador.Nickname;
                    string respuestaUsuario = respuestasIndividuales.ContainsKey((textoPregunta, nick))
                        ? respuestasIndividuales[(textoPregunta, nick)]
                        : "";

                    string estado = "INCORRECTA";
                    int puntos = 0;

                    if (respuestaUsuario == respuestaCorrecta)
                    {
                        int ordenRespuesta = lista.IndexOf(nick);
                        puntos = Math.Max(25, 200 - (ordenRespuesta * 25));
                        estado = "CORRECTO";
                    }

                    resultadosPorJugador[nick] = (estado, puntos);
                }

                foreach (var jugador in jugadores)
                {
                    jugador.Puntos += resultadosPorJugador[jugador.Nickname].puntos;
                }

                var topRanking = jugadores
                    .OrderByDescending(j => j.Puntos)
                    .Take(3)
                    .Select(j => $"{j.Nickname}:{j.Puntos}")
                    .ToList();

                foreach (var jugador in jugadores)
                {
                    string nick = jugador.Nickname;
                    var (estado, puntos) = resultadosPorJugador[nick];

                    string resultado = $"RESULTADO|{nick}|{estado}|{puntos}|{string.Join(",", topRanking)}";
                    EnviarResultado(nick, resultado);
                }
            }
        }

        private void EnviarResultado(string nick, string mensaje)
        {
            var jugador = jugadores.FirstOrDefault(j => j.Nickname == nick);
            if (jugador != null && jugador.Stream != null)
            {
                EnviarPorStream(jugador.IP, jugador.Puerto, mensaje);
                _frm.Log($"SRV | Resultado enviado a {nick}: {mensaje}");
            }
            else
            {
                _frm.Log($"SRV | ERROR: no se encontró jugador o stream para {nick}.");
            }
        }

        public void AsignarFrmPregunta(FrmPregunta frmPregunta)
        {
            _frmPregunta = frmPregunta;
        }

        public void EnviarMensajeIndividual(string nick, string mensaje)
        {
            var jugador = jugadores.FirstOrDefault(j => j.Nickname == nick);
            if (jugador != null && jugador.Stream != null)
            {
                EnviarPorStream(jugador.IP, jugador.Puerto, mensaje);
                _frm.Log($"SRV | Mensaje individual enviado a {nick}: {mensaje}");
            }
            else
            {
                _frm.Log($"SRV | ERROR enviando mensaje individual a {nick}: jugador o stream no encontrado");
            }
        }

        public void EnviarMensaje(string mensaje)
        {
            if (!mensaje.StartsWith("PREGUNTA"))
                return;

            lock (jugadores)
            {
                foreach (var jugador in jugadores)
                {
                    if (jugador.Stream != null)
                    {
                        try
                        {
                            EnviarPorStream(jugador.IP, jugador.Puerto, mensaje);
                            _frm.Log($"SRV | Pregunta enviada a {jugador.Nickname} ({jugador.IP}:{jugador.Puerto})");
                        }
                        catch (Exception ex)
                        {
                            _frm.Log($"SRV | ERROR enviando pregunta a {jugador.Nickname}: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void EnviarPorStream(string ipDestino, int puertoDestino, string mensaje)
        {
            try
            {
                using (TcpClient cliente = new TcpClient())
                {
                    cliente.Connect(ipDestino, puertoDestino);
                    using (SslStream sslStream = new SslStream(cliente.GetStream(), false, validarCertificat))
                    {
                        sslStream.AuthenticateAsClient(ipDestino);
                        byte[] data = Encoding.UTF8.GetBytes(mensaje + "\n");
                        sslStream.Write(data, 0, data.Length);
                        sslStream.Flush();

                        _frm.Log($"SRV | Mensaje enviado como cliente a {ipDestino}:{puertoDestino} => {mensaje}");
                    }
                }
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV | ERROR al enviar mensaje como cliente a {ipDestino}:{puertoDestino}: {ex.Message}");
            }
        }

        private bool validarCertificat(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
