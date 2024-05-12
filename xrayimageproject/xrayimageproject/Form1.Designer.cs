namespace xrayimageproject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.PaleTurquoise;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(338, 120);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(877, 466);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1300, 75);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(84, 22);
            label1.Name = "label1";
            label1.Size = new Size(307, 34);
            label1.TabIndex = 5;
            label1.Text = "X-Ray images project";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(66, 57);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(guna2Button3);
            panel2.Controls.Add(guna2Button2);
            panel2.Controls.Add(guna2Button1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 75);
            panel2.Name = "panel2";
            panel2.Size = new Size(180, 645);
            panel2.TabIndex = 3;
            // 
            // guna2Button1
            // 
            guna2Button1.BackColor = SystemColors.Control;
            guna2Button1.BorderRadius = 20;
            guna2Button1.CustomizableEdges = customizableEdges5;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = SystemColors.Control;
            guna2Button1.FocusedColor = Color.FromArgb(30, 180, 231);
            guna2Button1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button1.ForeColor = Color.Gray;
            guna2Button1.HoverState.FillColor = Color.FromArgb(30, 180, 231);
            guna2Button1.HoverState.ForeColor = Color.White;
            guna2Button1.Image = (Image)resources.GetObject("guna2Button1.Image");
            guna2Button1.ImageAlign = HorizontalAlignment.Left;
            guna2Button1.ImageOffset = new Point(5, 0);
            guna2Button1.Location = new Point(3, 62);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button1.Size = new Size(174, 56);
            guna2Button1.TabIndex = 0;
            guna2Button1.Text = "Import";
            guna2Button1.TextAlign = HorizontalAlignment.Left;
            guna2Button1.TextOffset = new Point(10, 0);
            guna2Button1.Click += guna2Button1_Click;
            // 
            // guna2Button2
            // 
            guna2Button2.BackColor = SystemColors.Control;
            guna2Button2.BorderRadius = 20;
            guna2Button2.CustomizableEdges = customizableEdges3;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.FillColor = SystemColors.Control;
            guna2Button2.FocusedColor = Color.FromArgb(30, 180, 231);
            guna2Button2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button2.ForeColor = Color.Gray;
            guna2Button2.HoverState.FillColor = Color.FromArgb(30, 180, 231);
            guna2Button2.HoverState.ForeColor = Color.White;
            guna2Button2.Image = (Image)resources.GetObject("guna2Button2.Image");
            guna2Button2.ImageAlign = HorizontalAlignment.Left;
            guna2Button2.ImageOffset = new Point(5, 0);
            guna2Button2.Location = new Point(3, 145);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button2.Size = new Size(174, 56);
            guna2Button2.TabIndex = 0;
            guna2Button2.Text = "Import";
            guna2Button2.TextAlign = HorizontalAlignment.Left;
            guna2Button2.TextOffset = new Point(10, 0);
            guna2Button2.Click += guna2Button1_Click;
            // 
            // guna2Button3
            // 
            guna2Button3.BackColor = SystemColors.Control;
            guna2Button3.BorderRadius = 20;
            guna2Button3.CustomizableEdges = customizableEdges1;
            guna2Button3.DisabledState.BorderColor = Color.DarkGray;
            guna2Button3.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button3.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button3.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button3.FillColor = SystemColors.Control;
            guna2Button3.FocusedColor = Color.FromArgb(30, 180, 231);
            guna2Button3.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button3.ForeColor = Color.Gray;
            guna2Button3.HoverState.FillColor = Color.FromArgb(30, 180, 231);
            guna2Button3.HoverState.ForeColor = Color.White;
            guna2Button3.Image = (Image)resources.GetObject("guna2Button3.Image");
            guna2Button3.ImageAlign = HorizontalAlignment.Left;
            guna2Button3.ImageOffset = new Point(5, 0);
            guna2Button3.Location = new Point(3, 207);
            guna2Button3.Name = "guna2Button3";
            guna2Button3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button3.Size = new Size(174, 56);
            guna2Button3.TabIndex = 0;
            guna2Button3.Text = "Import";
            guna2Button3.TextAlign = HorizontalAlignment.Left;
            guna2Button3.TextOffset = new Point(10, 0);
            guna2Button3.Click += guna2Button1_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1300, 720);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "XRayProject";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
    }
}
