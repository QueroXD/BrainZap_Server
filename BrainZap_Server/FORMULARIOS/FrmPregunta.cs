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
        public event EventHandler PreguntasCerrado;
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
            ResponsiveForm();
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

            btnSiguiente.Visible = false;
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
                btnSiguiente.Visible = true;
                lblTiempo.Text = "Tiempo agotado!";
                lblTiempo.ForeColor = Color.Red;
                lblResultadoFinal.Text = $"Respuesta correcta: {GestorPreguntas.ObtenerRespuestaCorrecta(PreguntaActual)}";
                timer.Stop();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            // al hacer click, pasa la pregunta y la envia a todos los jugadores
            PreguntaActual = GestorPreguntas.ObtenerSiguiente();
            if (PreguntaActual != null)
            {
                tiempoRestante = 30;

                string mensaje = GestorPreguntas.ObtenerPreguntaFormateada(PreguntaActual);
                SocketServidor.EnviarMensaje(mensaje);
                lblPregunta.Text = PreguntaActual.Texto;
                lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";

                // Asignar las opciones a los botones
                if (PreguntaActual.Opciones.Count > 0) btnOpcion1.Text = PreguntaActual.Opciones[0];
                if (PreguntaActual.Opciones.Count > 1) btnOpcion2.Text = PreguntaActual.Opciones[1];
                if (PreguntaActual.Opciones.Count > 2) btnOpcion3.Text = PreguntaActual.Opciones[2];
                if (PreguntaActual.Opciones.Count > 3) btnOpcion4.Text = PreguntaActual.Opciones[3];

                btnSiguiente.Visible = false;
                progressBar.Value = tiempoRestante;

                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                this.Close();
            }
        }

        private void ResponsiveForm()
        {
            if (this.Controls.Count == 0) return;

            // Calcular el área total ocupada por los controles
            Rectangle bounds = this.Controls[0].Bounds;

            foreach (Control control in this.Controls)
            {
                bounds = Rectangle.Union(bounds, control.Bounds);
            }

            // Calcular el desplazamiento para centrar los controles
            int offsetX = (this.ClientSize.Width - bounds.Width) / 2 - bounds.X;
            int offsetY = (this.ClientSize.Height - bounds.Height) / 2 - bounds.Y;

            // Mover los controles
            foreach (Control control in this.Controls)
            {
                control.Location = new Point(control.Location.X + offsetX, control.Location.Y + offsetY);
            }
        }


        private void FrmPregunta_FormClosed(object sender, FormClosedEventArgs e)
        {
            PreguntasCerrado?.Invoke(this, EventArgs.Empty);
        }
    }
}