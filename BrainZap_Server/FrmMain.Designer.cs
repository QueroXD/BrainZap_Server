namespace BrainZap_Server
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.ListBox lstJugadores;
        private System.Windows.Forms.Button btnEscuchar;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblIp = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lstJugadores = new System.Windows.Forms.ListBox();
            this.btnEscuchar = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(20, 20);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(136, 16);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "IP del servidor: 0.0.0.0";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(20, 300);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(500, 120);
            this.txtLog.TabIndex = 0;
            // 
            // lstJugadores
            // 
            this.lstJugadores.FormattingEnabled = true;
            this.lstJugadores.ItemHeight = 16;
            this.lstJugadores.Location = new System.Drawing.Point(20, 60);
            this.lstJugadores.Name = "lstJugadores";
            this.lstJugadores.Size = new System.Drawing.Size(300, 260);
            this.lstJugadores.TabIndex = 1;
            // 
            // btnEscuchar
            // 
            this.btnEscuchar.Location = new System.Drawing.Point(350, 60);
            this.btnEscuchar.Name = "btnEscuchar";
            this.btnEscuchar.Size = new System.Drawing.Size(150, 40);
            this.btnEscuchar.TabIndex = 2;
            this.btnEscuchar.Text = "Empezar a escuchar";
            this.btnEscuchar.UseVisualStyleBackColor = true;
            this.btnEscuchar.Click += new System.EventHandler(this.btnEscuchar_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(350, 120);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(150, 40);
            this.btnIniciar.TabIndex = 3;
            this.btnIniciar.Text = "Iniciar Partida";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciarPartida_Click);
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(540, 350);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.lstJugadores);
            this.Controls.Add(this.btnEscuchar);
            this.Controls.Add(this.btnIniciar);
            this.Name = "FrmMain";
            this.Text = "Servidor BrainZap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
