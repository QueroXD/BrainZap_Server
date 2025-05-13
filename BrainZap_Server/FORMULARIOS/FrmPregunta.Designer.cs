namespace BrainZap_Server.FORMULARIOS
{
    partial class FrmPregunta
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTiempo = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnOpcion4 = new System.Windows.Forms.Button();
            this.btnOpcion3 = new System.Windows.Forms.Button();
            this.btnOpcion2 = new System.Windows.Forms.Button();
            this.btnOpcion1 = new System.Windows.Forms.Button();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lblResultadoFinal = new System.Windows.Forms.Label();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTiempo
            // 
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTiempo.Location = new System.Drawing.Point(29, 365);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(740, 25);
            this.lblTiempo.TabIndex = 1;
            this.lblTiempo.Text = "Tiempo restante:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 403);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(740, 25);
            this.progressBar.TabIndex = 3;
            // 
            // btnOpcion4
            // 
            this.btnOpcion4.BackColor = System.Drawing.Color.Green;
            this.btnOpcion4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpcion4.ForeColor = System.Drawing.Color.White;
            this.btnOpcion4.Location = new System.Drawing.Point(426, 245);
            this.btnOpcion4.Name = "btnOpcion4";
            this.btnOpcion4.Size = new System.Drawing.Size(320, 100);
            this.btnOpcion4.TabIndex = 9;
            this.btnOpcion4.Text = "Respuesta 4";
            this.btnOpcion4.UseVisualStyleBackColor = false;
            // 
            // btnOpcion3
            // 
            this.btnOpcion3.BackColor = System.Drawing.Color.Gold;
            this.btnOpcion3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpcion3.ForeColor = System.Drawing.Color.Black;
            this.btnOpcion3.Location = new System.Drawing.Point(46, 245);
            this.btnOpcion3.Name = "btnOpcion3";
            this.btnOpcion3.Size = new System.Drawing.Size(320, 100);
            this.btnOpcion3.TabIndex = 8;
            this.btnOpcion3.Text = "Respuesta 3";
            this.btnOpcion3.UseVisualStyleBackColor = false;
            // 
            // btnOpcion2
            // 
            this.btnOpcion2.BackColor = System.Drawing.Color.Blue;
            this.btnOpcion2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpcion2.ForeColor = System.Drawing.Color.White;
            this.btnOpcion2.Location = new System.Drawing.Point(426, 115);
            this.btnOpcion2.Name = "btnOpcion2";
            this.btnOpcion2.Size = new System.Drawing.Size(320, 100);
            this.btnOpcion2.TabIndex = 7;
            this.btnOpcion2.Text = "Respuesta 2";
            this.btnOpcion2.UseVisualStyleBackColor = false;
            // 
            // btnOpcion1
            // 
            this.btnOpcion1.BackColor = System.Drawing.Color.Red;
            this.btnOpcion1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpcion1.ForeColor = System.Drawing.Color.White;
            this.btnOpcion1.Location = new System.Drawing.Point(46, 115);
            this.btnOpcion1.Name = "btnOpcion1";
            this.btnOpcion1.Size = new System.Drawing.Size(320, 100);
            this.btnOpcion1.TabIndex = 6;
            this.btnOpcion1.Text = "Respuesta 1";
            this.btnOpcion1.UseVisualStyleBackColor = false;
            // 
            // lblPregunta
            // 
            this.lblPregunta.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.Location = new System.Drawing.Point(46, 15);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(700, 80);
            this.lblPregunta.TabIndex = 5;
            this.lblPregunta.Text = "Aquí aparecerá la pregunta";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResultadoFinal
            // 
            this.lblResultadoFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultadoFinal.Location = new System.Drawing.Point(30, 445);
            this.lblResultadoFinal.Name = "lblResultadoFinal";
            this.lblResultadoFinal.Size = new System.Drawing.Size(641, 30);
            this.lblResultadoFinal.TabIndex = 2;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(677, 434);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(93, 48);
            this.btnSiguiente.TabIndex = 10;
            this.btnSiguiente.Text = "Siguiente Pregunta";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // FrmPregunta
            // 
            this.ClientSize = new System.Drawing.Size(791, 495);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnOpcion4);
            this.Controls.Add(this.btnOpcion3);
            this.Controls.Add(this.btnOpcion2);
            this.Controls.Add(this.btnOpcion1);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.lblTiempo);
            this.Controls.Add(this.lblResultadoFinal);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPregunta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pregunta Actual";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPregunta_FormClosed);
            this.Load += new System.EventHandler(this.FrmPregunta_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnOpcion4;
        private System.Windows.Forms.Button btnOpcion3;
        private System.Windows.Forms.Button btnOpcion2;
        private System.Windows.Forms.Button btnOpcion1;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Label lblResultadoFinal;
        private System.Windows.Forms.Button btnSiguiente;
    }
}
