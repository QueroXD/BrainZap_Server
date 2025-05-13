using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BrainZap_Server.CLASSES
{
    public class ClSocketServidor
    {
        private TcpListener server;
        private List<ClUsuario> jugadores = new List<ClUsuario>();
        private ClPreguntas preguntas;
        private FrmMain _frm;

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
                    _frm.Log($"SRV | Cliente conectado desde {cliente.Client.RemoteEndPoint}");

                    Thread recibir = new Thread(() => RecibirMensaje(cliente));
                    recibir.IsBackground = true;
                    recibir.Start();
                }
            });

            aceptarClientes.IsBackground = true;
            aceptarClientes.Start();
        }

        private void RecibirMensaje(TcpClient cliente)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                byte[] buffer = new byte[1024];
                int leido = stream.Read(buffer, 0, buffer.Length);
                string mensaje = Encoding.UTF8.GetString(buffer, 0, leido);

                if (mensaje.StartsWith("NICK"))
                {
                    GestionarNick(mensaje);
                }
                else if (mensaje.StartsWith("RESPUESTA"))
                {
                    _frm.Log($"SRV | Respuesta recibida: {mensaje}");
                    GestionarRespuesta(mensaje);
                }
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV | Error al recibir mensaje: {ex.Message}");
            }
            finally
            {
                cliente.Close(); // Cerramos siempre, solo al final
            }
        }


        private void GestionarNick(string mensaje)
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

            if (jugadores.Any(j => j.Nickname == nick))
            {
                EnviarRespuestaNick(ip, puerto, $"NICK|{nick}|ERROR");
                _frm.Log($"SRV | El nick {nick} ya está en uso.");
            }
            else
            {
                ClUsuario nuevo = new ClUsuario(nick, ip, puerto);
                jugadores.Add(nuevo);
                EnviarRespuestaNick(ip, puerto, $"NICK|{nick}|OK");
                _frm.Log($"SRV | Jugador registrado: {nick} ({ip}:{puerto})");
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
            string respuestaUsuario = partes[3];

            int puntosGanados = 0;

            var jugador = jugadores.FirstOrDefault(j => j.Nickname == nick);
            if (jugador == null)
            {
                _frm.Log($"SRV | Jugador '{nick}' no encontrado.");
                return;
            }

            string respuestaCorrecta = preguntas.ObtenerRespuestaCorrecta(preguntas.ObtenerPreguntaPorTexto(textoPregunta));
            if (respuestaUsuario == respuestaCorrecta)
            {
                puntosGanados = 10;
                jugador.Puntos += puntosGanados;
            }

            // Construimos ranking ordenado
            var topRanking = jugadores
                .OrderByDescending(j => j.Puntos)
                .Take(3)
                .Select(j => $"{j.Nickname}:{j.Puntos}")
                .ToList();

            string resultado = $"RESULTADO|{nick}|{respuestaUsuario}|{jugador.Puntos}|{string.Join(",", topRanking)}";
            EnviarResultado(jugador.IP, jugador.Puerto, resultado);
            _frm.Log($"SRV | Resultado enviado a {jugador.Nickname}: {resultado}");
        }


        private void EnviarRespuestaNick(string ip, int puerto, string mensaje)
        {
            try
            {
                using (TcpClient cliente = new TcpClient())
                {
                    cliente.Connect(ip, puerto);
                    NetworkStream stream = cliente.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(mensaje);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    _frm.Log($"SRV | Enviado a {ip}:{puerto} -> {mensaje}");
                }
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV | Error enviando a {ip}:{puerto}: {ex.Message}");
            }
        }

        private void EnviarResultado(string ip, int puerto, string mensaje)
        {
            try
            {
                using (TcpClient cliente = new TcpClient())
                {
                    cliente.Connect(ip, puerto);
                    NetworkStream stream = cliente.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(mensaje);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV | Error enviando resultado a {ip}:{puerto}: {ex.Message}");
            }
        }


        public void EnviarMensaje(string mensaje)
        {
            byte[] data = Encoding.UTF8.GetBytes(mensaje);

            if (mensaje.StartsWith("PREGUNTA"))
            {
                foreach (var jugador in jugadores)
                {
                    try
                    {
                        using (TcpClient cliente = new TcpClient())
                        {
                            cliente.Connect(jugador.IP, jugador.Puerto);
                            NetworkStream stream = cliente.GetStream();
                            stream.Write(data, 0, data.Length);
                            stream.Flush();
                            _frm.Log($"SRV | Pregunta enviada a {jugador.Nickname} ({jugador.IP}:{jugador.Puerto})");
                        }
                    }
                    catch (Exception ex)
                    {
                        _frm.Log($"SRV | Error enviando pregunta a {jugador.Nickname}: {ex.Message}");
                    }
                }
            }
            else if (mensaje.StartsWith("RESULTADO"))
            {
                // Enviar el resultado a todos los jugadores
                _frm.Log($"SRV | Resultado recibido: {mensaje}");
            }
        }
    }
}
