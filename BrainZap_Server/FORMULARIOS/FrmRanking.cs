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
        private List<ClJugador> jugadores;

        public FrmRanking(List<ClJugador> jugadores)
        {
            InitializeComponent();
            this.jugadores = jugadores;
        }

        private void FrmRanking_Load(object sender, EventArgs e)
        {
            var top3 = jugadores
                        .OrderByDescending(j => j.Puntos)
                        .Take(3)
                        .ToList();

            if (top3.Count > 0)
                lblTop1.Text = $"🥇 {top3[0].Nickname} - {top3[0].Puntos} pts";
            if (top3.Count > 1)
                lblTop2.Text = $"🥈 {top3[1].Nickname} - {top3[1].Puntos} pts";
            if (top3.Count > 2)
                lblTop3.Text = $"🥉 {top3[2].Nickname} - {top3[2].Puntos} pts";
        }
    }
}
