using BrainZap_Server.CLASSES;
using BrainZap_Server.FORMULARIOS;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<ClUsuario> jugadores = new List<ClUsuario>();
        private string rutaPreguntas = "preguntas.json";

        public FrmMain()
        {
            InitializeComponent();
            socketServidor = new ClSocketServidor(this, jugadores);
        }

        private void btnEscuchar_Click(object sender, EventArgs e)
        {
            string ip = ObtenerIPLocal();
            socketServidor.Iniciar();
            lblIp.Text = $"IP Servidor: {ip}";
            btnEscuchar.Enabled = false;
        }

        private void btnIniciarPartida_Click(object sender, EventArgs e)
        {
            if (jugadores.Count > 0)
            {
                gestorPreguntas = new ClPreguntas(rutaPreguntas);

                ClPregunta pregunta = gestorPreguntas.ObtenerSiguiente();
                FrmPregunta frmPregunta = new FrmPregunta(pregunta, socketServidor, gestorPreguntas);
                socketServidor.AsignarFrmPregunta(frmPregunta);
                frmPregunta.PreguntasCerrado += FrmPreguntasCerradoHandler;
                frmPregunta.Show();
            }
            else
            {
                MessageBox.Show("No hay jugadores conectados.");
            }
        }

        // Mostrar lista de jugadores en lstJugadores
        public void MostrarJugadores()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(MostrarJugadores));
            }
            else
            {
                lstJugadores.Items.Clear();
                foreach (var jugador in jugadores)
                {
                    lstJugadores.Items.Add($"{jugador.Nickname}");
                }
            }
        }


        public void Log(string mensaje)
        {
            // eliminar '/n' al final del mensaje
            if (mensaje.EndsWith("\n"))
            {
                mensaje = mensaje.Substring(0, mensaje.Length - 1);
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(mensaje)));
            }
            else
            {
                Color color;

                if (mensaje.Contains("ERROR"))
                {
                    color = Color.Red;
                }
                else if (mensaje.Contains("Respuesta recibida") || mensaje.Contains("Cliente conectado"))
                {
                    color = Color.Blue;
                }
                else
                {
                    color = Color.Green;
                }

                richLog.SelectionStart = richLog.TextLength;
                richLog.SelectionLength = 0;

                richLog.SelectionColor = color;
                richLog.AppendText($"{DateTime.Now:HH:mm:ss} | {mensaje}\n");
                richLog.SelectionColor = richLog.ForeColor;

                richLog.ScrollToCaret();
            }
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

        // Cuando el formulario de preguntas se cierra, se abre el formulario FrmRanking
        private void FrmPreguntasCerradoHandler(object sender, EventArgs e)
        {
            FrmRanking frmRanking = new FrmRanking(jugadores, socketServidor);
            frmRanking.Show();
        }

    }
}
