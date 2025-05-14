using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrainZap_Server.CLASSES;

namespace BrainZap_Server.FORMULARIOS
{
    public partial class FrmRanking : Form
    {
        private List<ClUsuario> jugadores;

        public FrmRanking(List<ClUsuario> jugadores)
        {
            InitializeComponent();
            this.jugadores = jugadores;
            this.Resize += (s, e) => ResponsiveForm();
        }

        private void FrmRanking_Load(object sender, EventArgs e)
        {
            // Ordenar jugadores por puntaje descendente
            var topJugadores = jugadores.OrderByDescending(j => j.Puntos).Take(3).ToList();

            if (topJugadores.Count > 0)
                lblTop1.Text = $"🥇 {topJugadores[0].Nickname} - {topJugadores[0].Puntos} puntos";
            if (topJugadores.Count > 1)
                lblTop2.Text = $"🥈 {topJugadores[1].Nickname} - {topJugadores[1].Puntos} puntos";
            if (topJugadores.Count > 2)
                lblTop3.Text = $"🥉 {topJugadores[2].Nickname} - {topJugadores[2].Puntos} puntos";

            ResponsiveForm();
        }

        private void ResponsiveForm()
        {
            // Centrar lblTitulo horizontalmente
            lblTitulo.Left = (panelRanking.Width - lblTitulo.Width) / 2;

            // Centrar panelPodio en el centro del formulario
            panelPodio.Left = (panelRanking.Width - panelPodio.Width) / 2;
            panelPodio.Top = (panelRanking.Height - panelPodio.Height) / 2;
        }
    }

}
