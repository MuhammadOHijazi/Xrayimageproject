namespace xrayimageproject
{
    partial class LayerItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkBox1 = new CheckBox();
            label1 = new Label();
            editingPictureBox = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)editingPictureBox).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(182, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 24);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckStateChanged += checkBox1_CheckStateChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(53, 23);
            label1.TabIndex = 1;
            label1.Text = "Layer";
            // 
            // editingPictureBox
            // 
            editingPictureBox.Location = new Point(3, 29);
            editingPictureBox.Name = "editingPictureBox";
            editingPictureBox.Size = new Size(280, 219);
            editingPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            editingPictureBox.TabIndex = 2;
            editingPictureBox.TabStop = false;
            editingPictureBox.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Cornsilk;
            panel1.Location = new Point(0, 259);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(302, 24);
            panel1.TabIndex = 3;
            // 
            // LayerItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(panel1);
            Controls.Add(editingPictureBox);
            Controls.Add(label1);
            Controls.Add(checkBox1);
            ForeColor = Color.Cornsilk;
            Name = "LayerItem";
            Size = new Size(287, 273);
            ((System.ComponentModel.ISupportInitialize)editingPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private CheckBox checkBox1;
        private Label label1;
        private PictureBox editingPictureBox;
        private Panel panel1;
    }
}
