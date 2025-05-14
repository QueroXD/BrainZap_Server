namespace BrainZap_Server
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.ListBox lstJugadores;
        private System.Windows.Forms.Button btnEscuchar;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.RichTextBox richLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lblIp = new System.Windows.Forms.Label();
            this.lstJugadores = new System.Windows.Forms.ListBox();
            this.btnEscuchar = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.richLog = new System.Windows.Forms.RichTextBox();
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
            // lstJugadores
            // 
            this.lstJugadores.FormattingEnabled = true;
            this.lstJugadores.ItemHeight = 16;
            this.lstJugadores.Location = new System.Drawing.Point(20, 60);
            this.lstJugadores.Name = "lstJugadores";
            this.lstJugadores.Size = new System.Drawing.Size(373, 100);
            this.lstJugadores.TabIndex = 1;
            // 
            // btnEscuchar
            // 
            this.btnEscuchar.Location = new System.Drawing.Point(416, 67);
            this.btnEscuchar.Name = "btnEscuchar";
            this.btnEscuchar.Size = new System.Drawing.Size(150, 40);
            this.btnEscuchar.TabIndex = 2;
            this.btnEscuchar.Text = "Empezar a escuchar";
            this.btnEscuchar.UseVisualStyleBackColor = true;
            this.btnEscuchar.Click += new System.EventHandler(this.btnEscuchar_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(416, 113);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(150, 40);
            this.btnIniciar.TabIndex = 3;
            this.btnIniciar.Text = "Iniciar Partida";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciarPartida_Click);
            // 
            // richLog
            // 
            this.richLog.BackColor = System.Drawing.Color.LightGray;
            this.richLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.richLog.ForeColor = System.Drawing.Color.White;
            this.richLog.Location = new System.Drawing.Point(20, 166);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.Size = new System.Drawing.Size(898, 328);
            this.richLog.TabIndex = 0;
            this.richLog.Text = "";
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(930, 506);
            this.Controls.Add(this.richLog);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.lstJugadores);
            this.Controls.Add(this.btnEscuchar);
            this.Controls.Add(this.btnIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Admin panel - BrainZap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
