namespace BrainZap_Server.FORMULARIOS
{
    partial class FrmRanking
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTop1;
        private System.Windows.Forms.Label lblTop2;
        private System.Windows.Forms.Label lblTop3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTop1 = new System.Windows.Forms.Label();
            this.lblTop2 = new System.Windows.Forms.Label();
            this.lblTop3 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(200, 20);
            this.lblTitulo.Size = new System.Drawing.Size(400, 40);
            this.lblTitulo.Text = "🏆 Ranking Final";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblTop1
            this.lblTop1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTop1.ForeColor = System.Drawing.Color.Gold;
            this.lblTop1.Location = new System.Drawing.Point(100, 80);
            this.lblTop1.Size = new System.Drawing.Size(600, 40);
            this.lblTop1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblTop2
            this.lblTop2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblTop2.ForeColor = System.Drawing.Color.Silver;
            this.lblTop2.Location = new System.Drawing.Point(100, 130);
            this.lblTop2.Size = new System.Drawing.Size(600, 40);
            this.lblTop2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblTop3
            this.lblTop3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTop3.ForeColor = System.Drawing.Color.Peru;
            this.lblTop3.Location = new System.Drawing.Point(100, 180);
            this.lblTop3.Size = new System.Drawing.Size(600, 40);
            this.lblTop3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // FrmRanking
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblTop1);
            this.Controls.Add(this.lblTop2);
            this.Controls.Add(this.lblTop3);
            this.Name = "FrmRanking";
            this.Text = "Ranking Final";
            this.ResumeLayout(false);
        }
    }
}
