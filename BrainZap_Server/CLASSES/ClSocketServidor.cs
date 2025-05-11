using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BrainZap_Server.CLASSES
{
    public class ClSocketServidor
    {
        private TcpListener servidor;
        private Thread hiloEscucha;
        private Dictionary<TcpClient, ClJugador> jugadores = new Dictionary<TcpClient, ClJugador>();
        private object lockJugadores = new object();

        public Action<string> Log { get; set; } // Para mostrar en FrmMain
        public Action ActualizarJugadoresUI { get; set; }
        public event Action<string, TcpClient> MensajeRecibido;

        public void Iniciar(string ip, int puerto)
        {
            IPAddress ipAddr = IPAddress.Parse(ip);
            servidor = new TcpListener(ipAddr, puerto);
            servidor.Start();

            hiloEscucha = new Thread(EscucharClientes);
            hiloEscucha.IsBackground = true;
            hiloEscucha.Start();

            Log?.Invoke($"Servidor iniciado en {ip}:{puerto}");
        }

        private void EscucharClientes()
        {
            while (true)
            {
                TcpClient cliente = servidor.AcceptTcpClient();
                Thread hiloCliente = new Thread(() => ManejarCliente(cliente));
                hiloCliente.IsBackground = true;
                hiloCliente.Start();
            }
        }

        private void ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            byte[] buffer = new byte[1024];

            while (cliente.Connected)
            {
                try
                {
                    int bytesLeidos = stream.Read(buffer, 0, buffer.Length);
                    if (bytesLeidos <= 0) break;

                    string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                    foreach (var linea in mensaje.Split('\n'))
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                            MensajeRecibido?.Invoke(linea.Trim(), cliente);
                    }
                }
                catch
                {
                    break;
                }
            }

            lock (lockJugadores)
            {
                if (jugadores.ContainsKey(cliente))
                {
                    Log?.Invoke($"{jugadores[cliente].Nickname} se desconectó.");
                    jugadores.Remove(cliente);
                    ActualizarJugadoresUI?.Invoke();
                }
            }

            cliente.Close();
        }

        public bool AgregarJugador(string nick, TcpClient cliente)
        {
            lock (lockJugadores)
            {
                foreach (var j in jugadores.Values)
                {
                    if (j.Nickname.Equals(nick, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                var jugador = new ClJugador(nick, (IPEndPoint)cliente.Client.RemoteEndPoint);
                jugadores[cliente] = jugador;
                ActualizarJugadoresUI?.Invoke();
                return true;
            }
        }

        public List<ClJugador> ObtenerJugadores()
        {
            lock (lockJugadores)
            {
                return new List<ClJugador>(jugadores.Values);
            }
        }

        public void EnviarATodos(string mensaje)
        {
            lock (lockJugadores)
            {
                byte[] datos = Encoding.UTF8.GetBytes(mensaje + "\n");
                foreach (var cliente in jugadores.Keys)
                {
                    try
                    {
                        cliente.GetStream().Write(datos, 0, datos.Length);
                    }
                    catch { }
                }
            }
        }

        public void EnviarA(TcpClient cliente, string mensaje)
        {
            try
            {
                byte[] datos = Encoding.UTF8.GetBytes(mensaje + "\n");
                cliente.GetStream().Write(datos, 0, datos.Length);
            }
            catch { }
        }

        public ClJugador GetJugadorPorCliente(TcpClient c)
        {
            lock (lockJugadores)
            {
                return jugadores.ContainsKey(c) ? jugadores[c] : null;
            }
        }
    }
}
