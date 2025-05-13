using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrainZap_Server.CLASSES;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BrainZap_Server.FORMULARIOS
{
    public partial class FrmPregunta : Form
    {
        public ClPreguntas GestorPreguntas { get; set; }
        public ClPregunta PreguntaActual { get; set; }
        public ClSocketServidor SocketServidor { get; set; }

        private Timer timer;
        private int tiempoRestante = 30; // en segundos

        public FrmPregunta(ClPregunta pregunta,ClSocketServidor socketServidor, ClPreguntas gestorPreguntas)
        {
            InitializeComponent();
            SocketServidor = socketServidor;
            PreguntaActual = pregunta;
            GestorPreguntas = gestorPreguntas;
        }

        private void FrmPregunta_Load(object sender, EventArgs e)
        {
            // enviar la pregunta a todos los jugadores
            string mensaje = GestorPreguntas.ObtenerPreguntaFormateada(PreguntaActual);
            SocketServidor.EnviarMensaje(mensaje);

            // Configurar el formulario
            lblPregunta.Text = PreguntaActual.Texto;
            lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";

            // Asignar las opciones a los botones
            if (PreguntaActual.Opciones.Count > 0) btnOpcion1.Text = PreguntaActual.Opciones[0];
            if (PreguntaActual.Opciones.Count > 1) btnOpcion2.Text = PreguntaActual.Opciones[1];
            if (PreguntaActual.Opciones.Count > 2) btnOpcion3.Text = PreguntaActual.Opciones[2];
            if (PreguntaActual.Opciones.Count > 3) btnOpcion4.Text = PreguntaActual.Opciones[3];

            progressBar.Maximum = tiempoRestante;
            progressBar.Value = tiempoRestante;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";
            progressBar.Value = Math.Max(0, tiempoRestante);

            if (tiempoRestante <= 0)
            {
                timer.Stop();
            }
        }
    }
}
