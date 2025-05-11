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
        public ClPregunta PreguntaActual { get; set; }
        public List<ClJugador> Jugadores { get; set; }

        private Timer timer;
        private int tiempoRestante = 10; // en segundos

        public FrmPregunta()
        {
            InitializeComponent();
        }

        private void FrmPregunta_Load(object sender, EventArgs e)
        {
            lblPregunta.Text = PreguntaActual.Texto;
            lblTiempo.Text = $"Tiempo restante: {tiempoRestante}s";

            for (int i = 0; i < PreguntaActual.Opciones.Count; i++)
            {
                btnOpciones[i].Text = PreguntaActual.Opciones[i];
            }

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
                MostrarResultados();
            }

            // Si todos respondieron antes del tiempo
            if (Jugadores.All(j => j.YaRespondio))
            {
                timer.Stop();
                MostrarResultados();
            }
        }

        private void MostrarResultados()
        {
            Dictionary<int, int> conteo = new Dictionary<int, int>();

            for (int i = 0; i < PreguntaActual.Opciones.Count; i++)
                conteo[i] = 0;

            foreach (var jugador in Jugadores)
            {
                if (jugador.UltimaRespuesta >= 0 && conteo.ContainsKey(jugador.UltimaRespuesta))
                    conteo[jugador.UltimaRespuesta]++;
            }

            lstResultados.Items.Clear();
            for (int i = 0; i < PreguntaActual.Opciones.Count; i++)
            {
                string texto = $"{PreguntaActual.Opciones[i]}: {conteo[i]} jugador(es)";
                lstResultados.Items.Add(texto);

                if (i == PreguntaActual.Correcta)
                    lstResultados.Items[lstResultados.Items.Count - 1] += " ✅";
            }

            lblResultadoFinal.Text = $"Respuesta correcta: {PreguntaActual.Opciones[PreguntaActual.Correcta]}";
        }
    }
}
