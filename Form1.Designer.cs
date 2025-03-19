namespace purecs2CC_Installer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblStatus = new Label();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(73, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ожидание...";
            // 
            // progressBar1
            // 
            progressBar1.AccessibleRole = AccessibleRole.StatusBar;
            progressBar1.BackColor = SystemColors.ActiveCaption;
            progressBar1.Cursor = Cursors.PanNW;
            progressBar1.Location = new Point(12, 50);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(205, 23);
            progressBar1.TabIndex = 2;
            progressBar1.Click += progressBar1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(229, 85);
            Controls.Add(progressBar1);
            Controls.Add(lblStatus);
            Cursor = Cursors.PanNW;
            DoubleBuffered = true;
            ForeColor = SystemColors.ButtonFace;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Installer";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
