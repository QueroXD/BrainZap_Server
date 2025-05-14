namespace BrainZap_Server.FORMULARIOS
{
    partial class FrmRanking
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelRanking;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelPodio;
        private System.Windows.Forms.Label lblTop1;
        private System.Windows.Forms.Label lblTop2;
        private System.Windows.Forms.Label lblTop3;
        private System.Windows.Forms.Label lblPos1;
        private System.Windows.Forms.Label lblPos2;
        private System.Windows.Forms.Label lblPos3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelRanking = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelPodio = new System.Windows.Forms.Panel();
            this.lblTop1 = new System.Windows.Forms.Label();
            this.lblTop2 = new System.Windows.Forms.Label();
            this.lblTop3 = new System.Windows.Forms.Label();
            this.lblPos1 = new System.Windows.Forms.Label();
            this.lblPos2 = new System.Windows.Forms.Label();
            this.lblPos3 = new System.Windows.Forms.Label();
            this.panelRanking.SuspendLayout();
            this.panelPodio.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRanking
            // 
            this.panelRanking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelRanking.Controls.Add(this.lblTitulo);
            this.panelRanking.Controls.Add(this.panelPodio);
            this.panelRanking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRanking.Location = new System.Drawing.Point(0, 0);
            this.panelRanking.Name = "panelRanking";
            this.panelRanking.Size = new System.Drawing.Size(800, 400);
            this.panelRanking.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(800, 60);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏆 Ranking Final";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPodio
            // 
            this.panelPodio.BackColor = System.Drawing.Color.Transparent;
            this.panelPodio.Controls.Add(this.lblTop1);
            this.panelPodio.Controls.Add(this.lblTop2);
            this.panelPodio.Controls.Add(this.lblTop3);
            this.panelPodio.Controls.Add(this.lblPos1);
            this.panelPodio.Controls.Add(this.lblPos2);
            this.panelPodio.Controls.Add(this.lblPos3);
            this.panelPodio.Location = new System.Drawing.Point(100, 100);
            this.panelPodio.Name = "panelPodio";
            this.panelPodio.Size = new System.Drawing.Size(600, 250);
            this.panelPodio.TabIndex = 1;
            // 
            // lblTop1
            // 
            this.lblTop1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTop1.ForeColor = System.Drawing.Color.White;
            this.lblTop1.Location = new System.Drawing.Point(200, 60);
            this.lblTop1.Name = "lblTop1";
            this.lblTop1.Size = new System.Drawing.Size(200, 30);
            this.lblTop1.TabIndex = 0;
            this.lblTop1.Text = "Jugador 1";
            this.lblTop1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTop2
            // 
            this.lblTop2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTop2.ForeColor = System.Drawing.Color.White;
            this.lblTop2.Location = new System.Drawing.Point(10, 120);
            this.lblTop2.Name = "lblTop2";
            this.lblTop2.Size = new System.Drawing.Size(160, 25);
            this.lblTop2.TabIndex = 1;
            this.lblTop2.Text = "Jugador 2";
            this.lblTop2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTop3
            // 
            this.lblTop3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTop3.ForeColor = System.Drawing.Color.White;
            this.lblTop3.Location = new System.Drawing.Point(430, 120);
            this.lblTop3.Name = "lblTop3";
            this.lblTop3.Size = new System.Drawing.Size(160, 25);
            this.lblTop3.TabIndex = 2;
            this.lblTop3.Text = "Jugador 3";
            this.lblTop3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPos1
            // 
            this.lblPos1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPos1.ForeColor = System.Drawing.Color.Gold;
            this.lblPos1.Location = new System.Drawing.Point(250, 20);
            this.lblPos1.Name = "lblPos1";
            this.lblPos1.Size = new System.Drawing.Size(100, 40);
            this.lblPos1.TabIndex = 3;
            this.lblPos1.Text = "🥇 1º";
            this.lblPos1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPos2
            // 
            this.lblPos2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPos2.ForeColor = System.Drawing.Color.Silver;
            this.lblPos2.Location = new System.Drawing.Point(50, 80);
            this.lblPos2.Name = "lblPos2";
            this.lblPos2.Size = new System.Drawing.Size(80, 40);
            this.lblPos2.TabIndex = 4;
            this.lblPos2.Text = "🥈 2º";
            this.lblPos2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPos3
            // 
            this.lblPos3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPos3.ForeColor = System.Drawing.Color.Peru;
            this.lblPos3.Location = new System.Drawing.Point(470, 80);
            this.lblPos3.Name = "lblPos3";
            this.lblPos3.Size = new System.Drawing.Size(80, 40);
            this.lblPos3.TabIndex = 5;
            this.lblPos3.Text = "🥉 3º";
            this.lblPos3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRanking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.panelRanking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRanking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ranking Final";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRanking_Load);
            this.panelRanking.ResumeLayout(false);
            this.panelPodio.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
