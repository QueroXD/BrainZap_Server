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
            this.Resize += (s, e) => ResponsiveForm();
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
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            progressBar.Value = tiempoRestante;
            lblTiempo.Text = $"{tiempoRestante}s";

            if (tiempoRestante <= 0)
            {
                timer.Stop();
                lblResultadoFinal.Text = $"Tiempo agotado. Respuesta correcta: {GestorPreguntas.ObtenerRespuestaCorrecta(PreguntaActual)}";
                btnSiguiente.Visible = true;

                SocketServidor.EvaluarYEnviarResultados(PreguntaActual.Texto); // <<<<< ESTE LLAMADO
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
                timer.Tick += timer_Tick;
                timer.Start();
            }
            else
            {
                this.Close();
            }
        }

        private void ResponsiveForm()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Centrar lblPregunta arriba
            lblPregunta.Left = (formWidth - lblPregunta.Width) / 2;
            lblPregunta.Top = 30;

            // Posicionamiento horizontal de botones en dos columnas
            int spacing = 40;
            int buttonWidth = btnOpcion1.Width;
            int buttonHeight = btnOpcion1.Height;
            int totalButtonHeight = buttonHeight * 2 + spacing;

            int topButtonsY = (formHeight - totalButtonHeight) / 2;

            // Primera fila
            btnOpcion1.Left = (formWidth / 2) - buttonWidth - spacing / 2;
            btnOpcion1.Top = topButtonsY;

            btnOpcion2.Left = (formWidth / 2) + spacing / 2;
            btnOpcion2.Top = topButtonsY;

            // Segunda fila
            btnOpcion3.Left = btnOpcion1.Left;
            btnOpcion3.Top = btnOpcion1.Bottom + spacing;

            btnOpcion4.Left = btnOpcion2.Left;
            btnOpcion4.Top = btnOpcion2.Bottom + spacing;

            // Tiempo y barra de progreso
            lblTiempo.Left = (formWidth - lblTiempo.Width) / 2;
            lblTiempo.Top = formHeight - 100;

            progressBar.Left = (formWidth - progressBar.Width) / 2;
            progressBar.Top = lblTiempo.Bottom + 5;

            lblResultadoFinal.Left = progressBar.Left;
            lblResultadoFinal.Top = progressBar.Bottom + 5;

            // Botón siguiente alineado a la derecha
            btnSiguiente.Left = formWidth - btnSiguiente.Width - 30;
            btnSiguiente.Top = formHeight - btnSiguiente.Height - 20;
        }

        public void ForzarFinDePreguntaDesdeServidor()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ForzarFinDePreguntaDesdeServidor));
                return;
            }

            if (timer.Enabled)
            {
                timer.Stop();
                tiempoRestante = 0;
                lblTiempo.Text = "Todos han respondido!";
                lblTiempo.ForeColor = Color.Green;
                progressBar.Value = 0;
                lblResultadoFinal.Text = $"Respuesta correcta: {GestorPreguntas.ObtenerRespuestaCorrecta(PreguntaActual)}";
                btnSiguiente.Visible = true;

                SocketServidor.EvaluarYEnviarResultados(PreguntaActual.Texto);
            }
        }


        private void FrmPregunta_FormClosed(object sender, FormClosedEventArgs e)
        {
            PreguntasCerrado?.Invoke(this, EventArgs.Empty);
        }
    }
}