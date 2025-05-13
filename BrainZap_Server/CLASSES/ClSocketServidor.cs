using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BrainZap_Server.CLASSES
{
    public class ClSocketServidor
    {
        private TcpListener listener;
        private List<ClUsuario> jugadores;
        private bool escuchando = false;
        private FrmMain _frm;

        public ClSocketServidor(List<ClUsuario> jugadores, FrmMain frm)
        {
            this.jugadores = jugadores;
            _frm = frm;
        }

        public void IniciarEscucha()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            escuchando = true;
            _frm.Log("Servidor escuchando en el puerto 5000...");

            Thread hilo = new Thread(() =>
            {
                while (escuchando)
                {
                    if (listener.Pending())
                    {
                        TcpClient cliente = listener.AcceptTcpClient();
                        Thread clienteHilo = new Thread(() => RecibirMensaje(cliente));
                        clienteHilo.Start();
                    }

                    Thread.Sleep(100);
                }
            });
            hilo.Start();
        }

        public void EnviarMensaje(string mensaje)
        {
            byte[] data = Encoding.UTF8.GetBytes(mensaje);

            if (mensaje.StartsWith("PREGUNTA"))
            {
                _frm.Log($"SRV|Enviando pregunta a todos los jugadores: {mensaje}");
                foreach (var jugador in jugadores)
                {
                    try
                    {
                        TcpClient cliente = new TcpClient();
                        cliente.Connect(jugador.IP);
                        NetworkStream stream = cliente.GetStream();
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                        cliente.Close();
                    }
                    catch (Exception ex)
                    {
                        _frm.Log($"SRV|Error enviando pregunta a {jugador.Nickname}: {ex.Message}");
                    }
                }

            }
            else if (mensaje.StartsWith("NICK"))
            {
                // Extraer el nick del mensaje
                string nickDestino = ObtenerNickDesdeMensaje(mensaje);
                if (string.IsNullOrEmpty(nickDestino))
                {
                    _frm.Log($"SRV|Nick no encontrado en el mensaje. No se puede enviar.");
                    return;
                }

                // Buscar jugador por nick
                var jugador = jugadores.FirstOrDefault(j => j.Nickname.Equals(nickDestino, StringComparison.OrdinalIgnoreCase));

                if (jugador == null)
                {
                    _frm.Log($"SRV|Jugador con '{nickDestino}' no encontrado.");
                    return;
                }
                try
                {
                    TcpClient cliente = new TcpClient();
                    cliente.Connect(jugador.IP);
                    NetworkStream stream = cliente.GetStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                    cliente.Close();
                    _frm.Log($"SRV|Mensaje enviado a {jugador.Nickname}: {mensaje}");
                }
                catch (Exception ex)
                {
                    _frm.Log($"SRV|Error enviando mensaje a {jugador.Nickname}: {ex.Message}");
                }
            }
        }

        private void RecibirMensaje(TcpClient cliente)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                byte[] buffer = new byte[1024];
                int leido = stream.Read(buffer, 0, buffer.Length);
                string mensaje = Encoding.UTF8.GetString(buffer, 0, leido);
                GestionarMensaje(mensaje);
                stream.Close();
                cliente.Close();
            }
            catch (Exception ex)
            {
                _frm.Log($"SRV|Error - {ex}");

            }
        }

        private void GestionarMensaje(string mensaje)
        {
            if (mensaje.StartsWith("NICK"))
            {
                // MENSAJE RECIBIDO: "NICK|nick|ip|puerto"
                // si el nick está disponible, añadir el usuario a la lista de jugadores y enviar un mensaje de confirmación al cliente. "NICK|OK"
                // si no, enviar un mensaje de error al cliente. "NICK|ERROR"
                string[] partes = mensaje.Split('|');
                if (partes.Length == 4)
                {
                    string nick = partes[1];
                    string ip = partes[2];
                    int puerto = int.Parse(partes[3]);

                    // Verificar si el nick ya está en uso
                    if (jugadores.Exists(j => j.Nickname == nick) == true)
                    {
                        EnviarMensaje($"NICK|{nick}|ERROR");
                        _frm.Log($"SRV|El nick {nick} ya está en uso.");
                    }
                    else
                    {
                        ClUsuario nuevoJugador = new ClUsuario(nick, ip, puerto);
                        jugadores.Add(nuevoJugador);
                        EnviarMensaje($"NICK|{nick}|OK");
                        _frm.Log($"SRV|Nuevo jugador: {nick}");
                    }
                }
            }
            else if (mensaje.StartsWith("RESPUESTA"))
            {
                // MENSAJE RECIBIDO: "RESPUESTA|nick|pregunta|respuesta"
                // si la respuesta es correcta, enviar un mensaje de confirmación al cliente. "RESPUESTA|nick|CORRECTA|puntos"
                // si no, enviar un mensaje de error al cliente. "RESPUESTA|nick|INCORRECTA"
                string[] partes = mensaje.Split('|');
                if (partes.Length == 4)
                {
                    string nick = partes[1];
                    string pregunta = partes[2];
                    string respuesta = partes[3];

                    // Verificar si la respuesta es correcta
                    ClPregunta preguntaActual = new ClPregunta(); // Aquí deberías obtener la pregunta actual
                    if (preguntaActual.Correcta.ToString() == respuesta)
                    {
                        // Respuesta correcta
                        int puntos = 10; // Aquí deberías calcular los puntos
                        _frm.Log($"SRV|Enviar mensaje a {nick}, respuesta correcta");
                        EnviarMensaje($"RESPUESTA|{nick}|CORRECTA|{puntos}");
                    }
                    else
                    {
                        // Respuesta incorrecta
                        _frm.Log($"SRV|Enviar mensaje a {nick}, respuesta incorrecta");
                        EnviarMensaje($"RESPUESTA|{nick}|INCORRECTA");
                    }
                }
            }
            else
            {
                _frm.Log(mensaje);
            }
        }

        private string ObtenerNickDesdeMensaje(string mensaje)
        {
            // Ejemplo: "TIPO|Juan|cosas"
            // Retorna "Juan"
            string[] partes = mensaje.Split('|');
            if (partes.Length > 1)
            {
                return partes[1];
            }
            return null;
        }
    }
}
