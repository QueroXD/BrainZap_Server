using BrainZap_Server.CLASSES;
using BrainZap_Server.FORMULARIOS;
using System;
using System.Collections.Generic;
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
        private string rutaPreguntas = "preguntas.json"; // Asegúrate de incluir este archivo

        public FrmMain()
        {
            InitializeComponent();
            socketServidor = new ClSocketServidor(jugadores, this);
            socketServidor.IniciarEscucha();
        }

        private void btnEscuchar_Click(object sender, EventArgs e)
        {
            string ip = ObtenerIPLocal();
            lblIp.Text = $"IP Servidor: {ip}";
            btnEscuchar.Enabled = false;
        }

        private void btnIniciarPartida_Click(object sender, EventArgs e)
        {
            gestorPreguntas = new ClPreguntas(rutaPreguntas);

            ClPregunta pregunta = gestorPreguntas.ObtenerSiguiente();
            FrmPregunta frmPregunta = new FrmPregunta(pregunta, socketServidor, gestorPreguntas);
            frmPregunta.Show();
        }

        public void Log(string mensaje)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(mensaje)));
            }
            else
            {
                listLog.Items.Add(mensaje);
                listLog.TopIndex = listLog.Items.Count - 1; // Scroll automático
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
    }
}
