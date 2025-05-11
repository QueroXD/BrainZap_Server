namespace BrainZap_Server.FORMULARIOS
{
    partial class FrmPregunta
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Label lblResultadoFinal;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Button[] btnOpciones;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox lstResultados;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.lblResultadoFinal = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lstResultados = new System.Windows.Forms.ListBox();
            this.btnOpciones = new System.Windows.Forms.Button[4];

            this.btnOpciones[0] = new System.Windows.Forms.Button();
            this.btnOpciones[1] = new System.Windows.Forms.Button();
            this.btnOpciones[2] = new System.Windows.Forms.Button();
            this.btnOpciones[3] = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblPregunta
            this.lblPregunta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.Location = new System.Drawing.Point(30, 20);
            this.lblPregunta.Size = new System.Drawing.Size(740, 60);
            this.lblPregunta.Text = "¿Pregunta?";

            // lblTiempo
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTiempo.Location = new System.Drawing.Point(30, 70);
            this.lblTiempo.Size = new System.Drawing.Size(740, 25);
            this.lblTiempo.Text = "Tiempo restante:";

            // lblResultadoFinal
            this.lblResultadoFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultadoFinal.Location = new System.Drawing.Point(30, 450);
            this.lblResultadoFinal.Size = new System.Drawing.Size(740, 30);

            // Botón 0
            this.btnOpciones[0].Location = new System.Drawing.Point(30, 100);
            this.btnOpciones[0].Size = new System.Drawing.Size(350, 50);
            this.btnOpciones[0].Text = "Opción 1";
            this.btnOpciones[0].Name = "btnOpcion0";

            // Botón 1
            this.btnOpciones[1].Location = new System.Drawing.Point(410, 100);
            this.btnOpciones[1].Size = new System.Drawing.Size(350, 50);
            this.btnOpciones[1].Text = "Opción 2";
            this.btnOpciones[1].Name = "btnOpcion1";

            // Botón 2
            this.btnOpciones[2].Location = new System.Drawing.Point(30, 170);
            this.btnOpciones[2].Size = new System.Drawing.Size(350, 50);
            this.btnOpciones[2].Text = "Opción 3";
            this.btnOpciones[2].Name = "btnOpcion2";

            // Botón 3
            this.btnOpciones[3].Location = new System.Drawing.Point(410, 170);
            this.btnOpciones[3].Size = new System.Drawing.Size(350, 50);
            this.btnOpciones[3].Text = "Opción 4";
            this.btnOpciones[3].Name = "btnOpcion3";

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(30, 240);
            this.progressBar.Size = new System.Drawing.Size(740, 25);
            this.progressBar.Maximum = 100;

            // lstResultados
            this.lstResultados.FormattingEnabled = true;
            this.lstResultados.ItemHeight = 20;
            this.lstResultados.Location = new System.Drawing.Point(30, 280);
            this.lstResultados.Size = new System.Drawing.Size(740, 160);

            // FrmPregunta
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lstResultados);
            this.Controls.Add(this.btnOpciones[0]);
            this.Controls.Add(this.btnOpciones[1]);
            this.Controls.Add(this.btnOpciones[2]);
            this.Controls.Add(this.btnOpciones[3]);
            this.Name = "FrmPregunta";
            this.Text = "Pregunta Actual";
            this.ResumeLayout(false);
        }
    }
}
