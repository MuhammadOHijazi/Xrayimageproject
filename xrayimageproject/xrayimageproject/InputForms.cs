using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrayimageproject
{

    internal class InputForm:Form
    {
        public InputForm() => InitializeComponent();

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient Name:";

            // txtPatientName
            this.txtPatientName.Location = new System.Drawing.Point(120, 27);
            this.txtPatientName.Name = "caption";
            this.txtPatientName.Size = new System.Drawing.Size(200, 23);
            this.txtPatientName.TabIndex = 1;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Diagnosis:";

            // txtDiagnosis
            this.txtDiagnosis.Location = new System.Drawing.Point(120, 67);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(200, 100);
            this.txtDiagnosis.TabIndex = 3;

            // btnGenerateReport
            this.btnGenerateReport.Location = new System.Drawing.Point(120, 180);
            this.btnGenerateReport.Name = "btnAddCaption";
            this.btnGenerateReport.Size = new System.Drawing.Size(120, 30);
            this.btnGenerateReport.TabIndex = 4;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(btnGenerateReport_Click);

            // InputForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 231);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.label1);
            this.Name = "InputForm";
            this.Text = "Enter Patient Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPatientName;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDiagnosis;
        public System.Windows.Forms.Button btnGenerateReport;

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
   
    }

    internal class CaptionInputForm : Form
    {
        public CaptionInputForm() => InitializeComponent();

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.caption = new System.Windows.Forms.TextBox();
            this.btnAddCaption = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caption";

            // txtPatientName
            this.caption.Location = new System.Drawing.Point(120, 27);
            this.caption.Name = "caption";
            this.caption.Size = new System.Drawing.Size(200, 100);
            this.caption.TabIndex = 1;
           
            // btnGenerateReport
            this.btnAddCaption.Location = new System.Drawing.Point(120, 180);
            this.btnAddCaption.Name = "btnAddCaption";
            this.btnAddCaption.Size = new System.Drawing.Size(120, 30);
            this.btnAddCaption.TabIndex = 2;
            this.btnAddCaption.Text = "Add Caption";
            this.btnAddCaption.UseVisualStyleBackColor = true;
            this.btnAddCaption.Click += new System.EventHandler(btnAddCaption_Click);

            // CaptionInputForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 231);
            this.Controls.Add(this.btnAddCaption);
            this.Controls.Add(this.caption);
            this.Controls.Add(this.label1);
            this.Name = "InputForm";
            this.Text = "Enter Caption to be Added";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox caption;
        public System.Windows.Forms.Button btnAddCaption;

        private void btnAddCaption_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
