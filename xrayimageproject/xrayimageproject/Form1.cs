using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using AForge.Imaging.Filters;
using System.ComponentModel;
using System.Drawing.Imaging;


namespace xrayimageproject
{
    public partial class Form1 : Form
    {
        // Patient ID
        static int id = 1000;

        // Declare variables to track the selection
        bool isSelecting = false;
        Point selectionStart, selectionEnd;
        Rectangle SelectedArea;
        Bitmap selectedBitmap;

        public Form1()
        {
            InitializeComponent();
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);

            pictureBox1.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            pictureBox1.Paint += new PaintEventHandler(pictureBox_Paint);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Start the selection from the current mouse location
            isSelecting = true;
            selectionStart = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                // Update the end point of the selection while the mouse moves
                selectionEnd = e.Location;
                // Redraw the pictureBox to show the selection rectangle
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                // End the selection when the mouse is up
                isSelecting = false;

            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isSelecting)
            {
                // Draw a rectangle to represent the selected area
                SelectedArea = GetRectangle(selectionStart, selectionEnd);
                e.Graphics.DrawRectangle(Pens.Black, SelectedArea);

            }
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private static Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayscaleBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(grayscaleBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
                    });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return grayscaleBitmap;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);

                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Bitmap originalImage = (Bitmap)pictureBox1.Image;

            Rectangle selectedArea = new Rectangle(SelectedArea.X, SelectedArea.Y, SelectedArea.Width, SelectedArea.Height);

            if (selectedArea.Right > originalImage.Width || selectedArea.Bottom > originalImage.Height)
            {
                MessageBox.Show("Selected area is out of the image bounds.");
                return;
            }
            selectedBitmap = originalImage.Clone(selectedArea, originalImage.PixelFormat);

            for (int x = 0; x < selectedBitmap.Width; x++)
            {
                for (int y = 0; y < selectedBitmap.Height; y++)
                {
                    Color originalColor = selectedBitmap.GetPixel(x, y);
                    int intensity = (originalColor.R + originalColor.G + originalColor.B) / 3;
                    Color mappedColor = MapIntensityToColor(intensity);
                    selectedBitmap.SetPixel(x, y, mappedColor);
                }
            }
            using (Graphics g = Graphics.FromImage(originalImage))
            {

                g.DrawImage(selectedBitmap, selectedArea.Location);
            }

            selectedBitmap.Dispose();

            pictureBox1.Refresh();
        }
        private Color MapIntensityToColor(int intensity)
        {
            intensity = Math.Clamp(intensity, 0, 255);

            int colorValue = intensity * 2;
            colorValue = Math.Min(colorValue, 255);

            // Return the color with the clamped values
            return Color.FromArgb(255, colorValue, colorValue);
        }

        private float[,] GetBrightnessMatrix(Bitmap grayscaleImage, Rectangle selection)
        {
            int width = selection.Width;
            int height = selection.Height;
            float[,] brightnessMatrix = new float[width, height];

            // Lock the bitmap's bits.
            BitmapData bmpData = grayscaleImage.LockBits(selection, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Calculate the brightness of each pixel within the selection.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Calculate the offset for the current pixel.
                    int position = (y * bmpData.Stride) + (x * 3);
                    // Since the image is grayscale, R=G=B, so it doesn't matter which one we choose.
                    // Normalize the brightness to be between 0 (black) and 1 (white).
                    float brightness = rgbValues[position] / 255.0f;
                    brightnessMatrix[x, y] = brightness;
                }
            }
            // Unlock the bits.
            grayscaleImage.UnlockBits(bmpData);

            return brightnessMatrix;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sf.FileName);
            }
        }
        private void InitializeComboBox()
        {
            guna2ComboBox1.SelectedIndexChanged += new EventHandler(guna2ComboBox1_SelectedIndexChanged);
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (guna2ComboBox1.SelectedItem.ToString())
            {
                case "Gray-Scale Mode":
                    Console.WriteLine("Gray-Scale Mode Selected");
                    Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
                    Bitmap grayscaleBitmap = ConvertToGrayscale(originalBitmap);
                    pictureBox1.Image = grayscaleBitmap;
                    break;
                case "CMY Mode":
                    Console.WriteLine("CMY Mode Selected");
                    ConvertToCMY();
                    break;
                case "Hot Mode":
                    Console.WriteLine("Hot Colormap Selected");
                    ApplyHotColormap();
                    break;
                case "Jet Mode":
                    Console.WriteLine("Jet Colormap Selected");
                    ApplyJetColormap();
                    break;
                case "Pink Mode":
                    Console.WriteLine("Pink Colormap Selected");
                    ApplyPinkColormap();
                    break;
                case "Plasma Mode":
                    Console.WriteLine("Plasma Colormap Selected");
                    ApplyPlasmaColormap();
                    break;
                case "Bone Mode":
                    Console.WriteLine("Bone Colormap Selected");
                    ApplyBoneColormap();
                    break;
            }
        }

        // Coloring Systems
        // Bone ColorMap
        private void ApplyBoneColormap()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap boneColormapBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color originalColor = originalBitmap.GetPixel(x, y);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color boneColor = CalculateBoneColor(intensity);
                    boneColormapBitmap.SetPixel(x, y, boneColor);
                }
            }

            pictureBox1.Image = boneColormapBitmap;
        }

        private Color CalculateBoneColor(int intensity)
        {
            float normalizedIntensity = intensity / 255f;
            float r = normalizedIntensity < 0.75f ? normalizedIntensity : (0.75f * normalizedIntensity + 0.25f);
            float g = normalizedIntensity < 0.75f ? normalizedIntensity : (0.75f * normalizedIntensity + 0.25f);
            float b = normalizedIntensity < 0.5f ? 0.5f * normalizedIntensity : (0.75f * normalizedIntensity - 0.25f);

            // Ensure the values are within [0, 255]
            r = ClampBone(r);
            g = ClampBone(g);
            b = ClampBone(b);

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
        private float ClampBone(float value)
        {
            return Math.Max(0.0f, Math.Min(1.0f, value));
        }

        // Plasma ColorMap
        private void ApplyPlasmaColormap()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap plasmaColormapBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color originalColor = originalBitmap.GetPixel(x, y);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color plasmaColor = CalculatePlasmaColor(intensity);
                    plasmaColormapBitmap.SetPixel(x, y, plasmaColor);
                }
            }

            pictureBox1.Image = plasmaColormapBitmap;
        }

        private Color CalculatePlasmaColor(int intensity)
        {
            float normalizedIntensity = intensity / 255f;
            float r = ClampPlasma(PlasmaRedComponent(normalizedIntensity));
            float g = ClampPlasma(PlasmaGreenComponent(normalizedIntensity));
            float b = ClampPlasma(PlasmaBlueComponent(normalizedIntensity));

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
        // Helper method to clamp color values
        private float ClampPlasma(float value)
        {
            return Math.Max(0.0f, Math.Min(1.0f, value));
        }
        private float PlasmaRedComponent(float value)
        {
            if (value <= 0.5f)
                return 1.0f;
            else if (value <= 0.75f)
                return -4.0f * value + 3.5f;
            else
                return 0.5f;
        }
        private float PlasmaGreenComponent(float value)
        {
            if (value <= 0.25f)
                return 4.0f * value;
            else if (value <= 0.5f)
                return 1.0f;
            else if (value <= 0.75f)
                return -4.0f * value + 3.5f;
            else
                return 0.5f;
        }
        private float PlasmaBlueComponent(float value)
        {
            if (value <= 0.25f)
                return 0.5f;
            else if (value <= 0.5f)
                return 4.0f * value - 0.5f;
            else
                return 1.0f;
        }


        // Pink ColorMap
        private void ApplyPinkColormap()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap pinkColormapBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color originalColor = originalBitmap.GetPixel(x, y);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color pinkColor = CalculatePinkColor(intensity);
                    pinkColormapBitmap.SetPixel(x, y, pinkColor);
                }
            }

            pictureBox1.Image = pinkColormapBitmap;
        }

        private Color CalculatePinkColor(int intensity)
        {
            float normalizedIntensity = intensity / 255f;
            float r = normalizedIntensity;
            float g = (float)Math.Sqrt(normalizedIntensity);
            float b = (float)Math.Sqrt(g);

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        // Jet ColorMap
        private void ApplyJetColormap()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap jetColormapBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color originalColor = originalBitmap.GetPixel(x, y);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color jetColor = CalculateJetColor(intensity);
                    jetColormapBitmap.SetPixel(x, y, jetColor);
                }
            }

            pictureBox1.Image = jetColormapBitmap;
        }

        private Color CalculateJetColor(int intensity)
        {
            float normalizedIntensity = intensity / 255f;
            float r = ClampValue(1.5f - Math.Abs(4f * normalizedIntensity - 3f));
            float g = ClampValue(1.5f - Math.Abs(4f * normalizedIntensity - 2f));
            float b = ClampValue(1.5f - Math.Abs(4f * normalizedIntensity - 1f));

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private float ClampValue(float value)
        {
            return Math.Max(0.0f, Math.Min(1.0f, value));
        }

        // Hot ColorMap
        private void ApplyHotColormap()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap hotColormapBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color originalColor = originalBitmap.GetPixel(x, y);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color hotColor = CalculateHotColor(intensity);
                    hotColormapBitmap.SetPixel(x, y, hotColor);
                }
            }

            pictureBox1.Image = hotColormapBitmap;
        }

        private Color CalculateHotColor(int intensity)
        {
            float normalizedIntensity = intensity / 255f;
            int r = normalizedIntensity < 0.3f ? (int)(normalizedIntensity / 0.3f * 255) : 255;
            int g = normalizedIntensity < 0.3f ? 0 : normalizedIntensity < 0.6f ? (int)((normalizedIntensity - 0.3f) / 0.3f * 255) : 255;
            int b = normalizedIntensity < 0.6f ? 0 : (int)((normalizedIntensity - 0.6f) / 0.4f * 255);

            // Ensure the values are within [0, 255]
            r = Math.Min(255, Math.Max(0, r));
            g = Math.Min(255, Math.Max(0, g));
            b = Math.Min(255, Math.Max(0, b));

            return Color.FromArgb(r, g, b);
        }
        // CMY System Color
        private void ConvertToCMY()
        {
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap cmykBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color pixelColor = originalBitmap.GetPixel(x, y);
                    Color cmykColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    cmykBitmap.SetPixel(x, y, cmykColor);
                }
            }
            pictureBox1.Image = cmykBitmap;
        }

        // Clibboard button
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            MessageBox.Show("Image copied to clipboard. Please paste it into your social media post.");

        }

        // HPF Fourier button
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            float cutoffFrequency = 0.0f;
            Bitmap resultImage = HighAndLowPassFilter.ApplyFilter(pictureBox1.Image, cutoffFrequency, HighAndLowPassFilter.FilterType.High);
            pictureBox3.Image = resultImage;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        // LPF Fourier button
        private void guna2Button14_Click(object sender, EventArgs e)
        {
            float cutoffFrequency = 30.0f;
            Bitmap resultImage = HighAndLowPassFilter.ApplyFilter(pictureBox1.Image, cutoffFrequency, HighAndLowPassFilter.FilterType.Low);
            pictureBox3.Image = resultImage;
        }

        // compress image
        private void guna2Button15_Click(object sender, EventArgs e)
        {
            string outputPath = "..\\..\\..\\compressedImages\\compressed" +
                System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            int quality = 75;
            ImageCompression.CompressJpegImage(pictureBox1.Image, outputPath, quality);
        }
        // compress audio
        private void guna2Button16_Click(object sender, EventArgs e)
        {
            int compressionRate = 128000; // 128 kbps for standard voice quality.
            IAudioCompressor compressor = new Mp3AudioCompressor(compressionRate);
            // brows to choose an wav audio file to compress
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "..\\..\\..\\";
                openFileDialog.Filter = "WAV files (*.wav)|*.wav";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string inputPath = openFileDialog.FileName;

                    // Set the output path, for example, same directory with .mp3 extension
                    string outputPath = Path.ChangeExtension(inputPath, ".mp3");

                    // Call the CompressAudio method
                    AudioCompression.CompressAudio(inputPath, outputPath, compressor);

                    MessageBox.Show("Compression completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // generating report
        private void guna2Button12_Click_1(object sender, EventArgs e)
        {
            ReportInputForm inputForm = new();
            DialogResult result = inputForm.ShowDialog(); // Show as a modal dialog

            if (result == DialogResult.OK)
            {
                // Getting the input from the previous dialog and the current image
                string patientName = inputForm.txtPatientName.Text;
                id++;
                string diagnosisText = inputForm.txtDiagnosis.Text;
                Image image = pictureBox1.Image;

                ReportGeneration repoGene = new();
                repoGene.GenerateReport(patientName, id.ToString(), diagnosisText, image);
            }
        }
        // compress files and directories
        private void guna2Button17_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "..\\..\\..\\";
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string inputPath = openFileDialog.FileName;

                    // Set the output path, for example, same directory with .mp3 extension
                    string outputPath = Path.ChangeExtension(inputPath, ".zip");

                    // Call the FileAndDirectoryCompression compress method
                    FileAndDirectoryCompression.Compress(inputPath, outputPath);

                    MessageBox.Show("Compression completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
    partial class ReportInputForm:Form
    {
        public ReportInputForm() => InitializeComponent();

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
            this.txtPatientName.Name = "txtPatientName";
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
            this.btnGenerateReport.Name = "btnGenerateReport";
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
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox imgPath;
        public System.Windows.Forms.Button btnGenerateReport;

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {           
            this.DialogResult = DialogResult.OK; 
            this.Close(); 
        }


    }
}