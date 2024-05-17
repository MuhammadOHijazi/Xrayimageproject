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
        }


        private int CalculateBrightnessLevel(Color color)
        {
            float brightness = (float)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (brightness < 0.1)
            {
                return 0;
            }
            else if (brightness < 0.2)
            {
                return 1;
            }
            else if (brightness < 0.3)
            {
                return 2;
            }
            else if (brightness < 0.4)
            {
                return 3;
            }
            else if (brightness < 0.5)
            {
                return 4;
            }
            else if (brightness < 0.6)
            {
                return 5;
            }
            else if (brightness < 0.7)
            {
                return 6;
            }
            else if (brightness < 0.8)
            {
                return 7;
            }
            else if (brightness < 0.9)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }
        // visual spectrum
        private Color[] spectrumColorMap = new Color[] {
        Color.FromArgb(0, 0, 255),     // index0  
        Color.FromArgb(0, 64, 255),    // index1 
    Color.FromArgb(0, 128, 255),   // index2
    Color.FromArgb(0, 192, 255),   // index3  
    Color.FromArgb(0, 255, 255),   // index4  
    Color.FromArgb(0, 255, 192),   // index5  
    Color.FromArgb(0, 255, 128),   // index6  
    Color.FromArgb(0, 255, 64),    // index7  
    Color.FromArgb(0, 255, 0),     // index8  
    Color.FromArgb(128, 255, 0),   // index9  
    Color.FromArgb(192, 255, 0),   // index10  
    Color.FromArgb(255, 255, 0),   // index11 
    Color.FromArgb(255, 192, 0),   // index12 
    Color.FromArgb(255, 128, 0),   // index13 
    Color.FromArgb(255, 64, 0),    // index14 
    Color.FromArgb(255, 0, 0)      // index15
};

        private Color[] elevationColorMap = new Color[] {
    Color.FromArgb(148, 0, 211),   // index0: Deep Violet
    Color.FromArgb(138, 43, 226),  // index1: Blue Violet
    Color.FromArgb(75, 0, 130),    // index2: Indigo
    Color.FromArgb(0, 0, 255),     // index3: Blue
    Color.FromArgb(0, 255, 255),   // index4: Cyan
    Color.FromArgb(0, 255, 0),     // index5: Green
    Color.FromArgb(173, 255, 47),  // index6: Green Yellow
    Color.FromArgb(255, 255, 0),   // index7: Yellow
    Color.FromArgb(255, 215, 0),   // index8: Gold
    Color.FromArgb(255, 165, 0),   // index9: Orange
    Color.FromArgb(255, 140, 0),   // index10: Dark Orange
    Color.FromArgb(255, 69, 0),    // index11: Red Orange
    Color.FromArgb(255, 0, 0),     // index12: Red
    Color.FromArgb(255, 192, 203), // index13: Pink
    Color.FromArgb(255, 20, 147),  // index14: Deep Pink
    Color.FromArgb(199, 21, 133)   // index15: Medium Violet Red   
};
        private Color[] DiagnosticColorMap = new Color[] {
    Color.FromArgb(0, 0, 0),       // index0: Black (Void/No Data)
    Color.FromArgb(105, 105, 105), // index1: Dim Gray (Low Density Tissue)
    Color.FromArgb(160, 82, 45),   // index2: Sienna (Muscle/Fat)
    Color.FromArgb(218, 165, 32),  // index3: Golden Rod (Soft Tissue)
    Color.FromArgb(255, 0, 255),   // index4: Magenta (High Density Tissue)
    Color.FromArgb(65, 105, 225),  // index5: Royal Blue (Veins)
    Color.FromArgb(30, 144, 255),  // index6: Dodger Blue (Arteries)
    Color.FromArgb(34, 139, 34),   // index7: Forest Green (Connective Tissue)
    Color.FromArgb(222, 184, 135), // index8: Burly Wood (Bone/Ligament)
    Color.FromArgb(245, 245, 220), // index9: Beige (Cartilage)
    Color.FromArgb(255, 228, 196), // index10: Bisque (Nerve)
    Color.FromArgb(255, 69, 0),    // index11: Red Orange (Inflammation)
    Color.FromArgb(255, 105, 180), // index12: Hot Pink (Tumor)
    Color.FromArgb(47, 79, 79),    // index13: Dark Slate Gray (Calcification)
    Color.FromArgb(255, 215, 0),   // index14: Gold (Contrast Agent)
    Color.FromArgb(255, 255, 255)  // index15: White (High Intensity/Highlight)
};
        private Color[] medicalColorMap = new Color[] {
    Color.FromArgb(0, 0, 0),       // index0: Black (Void/No Data)
    Color.FromArgb(32, 32, 32),    // index1: Very Dark Gray (Lowest Intensity)
    Color.FromArgb(64, 64, 64),    // index2: Dark Gray (Lower Intensity)
    Color.FromArgb(96, 96, 96),    // index3: Dim Gray (Low Intensity)
    Color.FromArgb(128, 128, 128), // index4: Gray (Medium Low Intensity)
    Color.FromArgb(160, 160, 160), // index5: Light Gray (Neutral Intensity)
    Color.FromArgb(192, 192, 192), // index6: Very Light Gray (Medium High Intensity)
    Color.FromArgb(224, 224, 224), // index7: Near White (Higher Intensity)
    Color.FromArgb(255, 0, 0),     // index8: Red (High Intensity)
    Color.FromArgb(0, 255, 0),     // index9: Green (Normal Tissue)
    Color.FromArgb(0, 0, 255),     // index10: Blue (Fluid/Cooling)
    Color.FromArgb(255, 255, 0),   // index11: Yellow (Cautionary Tissue)
    Color.FromArgb(255, 165, 0),   // index12: Orange (Inflammation)
    Color.FromArgb(255, 105, 180), // index13: Hot Pink (Abnormal Tissue)
    Color.FromArgb(160, 32, 240),  // index14: Purple (High Energy)
    Color.FromArgb(255, 255, 255)  // index15: White (Bone/High Density)
};
        private Color[] redColorMap = new Color[] {
    Color.FromArgb(255, 240, 240), // index0: Very Light Red
    Color.FromArgb(255, 224, 224), // index1: Lighter Red
    Color.FromArgb(255, 208, 208), // index2: Light Red
    Color.FromArgb(255, 192, 192), // index3: Soft Red
    Color.FromArgb(255, 176, 176), // index4: Salmon Red
    Color.FromArgb(255, 160, 160), // index5: Warm Red
    Color.FromArgb(255, 144, 144), // index6: Medium Light Red
    Color.FromArgb(255, 128, 128), // index7: Medium Red
    Color.FromArgb(255, 112, 112), // index8: Rich Red
    Color.FromArgb(255, 96, 96),   // index9: Strong Red
    Color.FromArgb(255, 80, 80),   // index10: Deep Red
    Color.FromArgb(255, 64, 64),   // index11: Darker Red
    Color.FromArgb(255, 48, 48),   // index12: Dark Red
    Color.FromArgb(255, 32, 32),   // index13: Very Dark Red
    Color.FromArgb(255, 16, 16),   // index14: Near Black Red
    Color.FromArgb(255, 0, 0)      // index15: Pure Red
};

        private Color[] blueColorMap = new Color[] {
    Color.FromArgb(255, 240, 240, 255), // index0: Very Light Blue
    Color.FromArgb(255, 224, 224, 255), // index1: Lighter Blue
    Color.FromArgb(255, 208, 208, 255), // index2: Light Blue
    Color.FromArgb(255, 192, 192, 255), // index3: Soft Blue
    Color.FromArgb(255, 176, 176, 255), // index4: Sky Blue
    Color.FromArgb(255, 160, 160, 255), // index5: Warm Blue
    Color.FromArgb(255, 144, 144, 255), // index6: Medium Light Blue
    Color.FromArgb(255, 128, 128, 255), // index7: Medium Blue
    Color.FromArgb(255, 112, 112, 255), // index8: Rich Blue
    Color.FromArgb(255, 96, 96, 255),   // index9: Strong Blue
    Color.FromArgb(255, 80, 80, 255),   // index10: Deep Blue
    Color.FromArgb(255, 64, 64, 255),   // index11: Darker Blue
    Color.FromArgb(255, 48, 48, 255),   // index12: Dark Blue
    Color.FromArgb(255, 32, 32, 255),   // index13: Very Dark Blue
    Color.FromArgb(255, 16, 16, 255),   // index14: Near Black Blue
    Color.FromArgb(255, 0, 0, 255)      // index15: Pure Blue
};
        private Color[] greenColorMap = new Color[] {
    Color.FromArgb(255, 240, 255, 240), // index0: Very Light Green
    Color.FromArgb(255, 224, 255, 224), // index1: Lighter Green
    Color.FromArgb(255, 208, 255, 208), // index2: Light Green
    Color.FromArgb(255, 192, 255, 192), // index3: Soft Green
    Color.FromArgb(255, 176, 255, 176), // index4: Mint Green
    Color.FromArgb(255, 160, 255, 160), // index5: Warm Green
    Color.FromArgb(255, 144, 255, 144), // index6: Medium Light Green
    Color.FromArgb(255, 128, 255, 128), // index7: Medium Green
    Color.FromArgb(255, 112, 255, 112), // index8: Rich Green
    Color.FromArgb(255, 96, 255, 96),   // index9: Strong Green
    Color.FromArgb(255, 80, 255, 80),   // index10: Deep Green
    Color.FromArgb(255, 64, 255, 64),   // index11: Darker Green
    Color.FromArgb(255, 48, 255, 48),   // index12: Dark Green
    Color.FromArgb(255, 32, 255, 32),   // index13: Very Dark Green
    Color.FromArgb(255, 16, 255, 16),   // index14: Near Black Green
    Color.FromArgb(255, 0, 255, 0)      // index15: Pure Green
};

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sf.FileName);
            }
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
            InputForm inputForm = new();
            DialogResult result = inputForm.ShowDialog(); // Show as a modal dialog

            if (result == DialogResult.OK)
            {
                // Getting the input from the previous dialog and the current image
                string patientName = inputForm.txtPatientName.Text;
                id++;
                string diagnosisText = inputForm.txtDiagnosis.Text;
                Image image = pictureBox1.Image;

                ReportGeneration.GenerateReport(patientName, id.ToString(), diagnosisText, image);
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


        private void ColoredSpaceSelected(int ColorMaps)
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
                    int brighteness = CalculateBrightnessLevel(originalColor);
                    if (ColorMaps == 1)
                    {
                        Color mappedColor = spectrumColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 2)
                    {
                        Color mappedColor = elevationColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 3)
                    {
                        Color mappedColor = DiagnosticColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 4)
                    {
                        Color mappedColor = medicalColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 5)
                    {
                        Color mappedColor = redColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 6)
                    {
                        Color mappedColor = blueColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                    else if (ColorMaps == 7)
                    {

                        Color mappedColor = greenColorMap[brighteness];
                        selectedBitmap.SetPixel(x, y, mappedColor);
                    }
                }
            }
            using (Graphics g = Graphics.FromImage(originalImage))
            {

                g.DrawImage(selectedBitmap, selectedArea.Location);
            }

            selectedBitmap.Dispose();

            pictureBox1.Refresh();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // visual spectrum
            // deep violet
            switch (guna2ComboBox2.SelectedItem.ToString())
            {
                case "visual spectrum":
                    ColoredSpaceSelected(1);
                    break;
                case "deep violet":
                    ColoredSpaceSelected(2);
                    break;
                case "Diagnostic":
                    ColoredSpaceSelected(3);
                    break;
                case "Medical":
                    ColoredSpaceSelected(4);
                    break;
                case "Red":
                    ColoredSpaceSelected(5);
                    break;
                case "Blue":
                    ColoredSpaceSelected(6);
                    break;
                case "Green":
                    ColoredSpaceSelected(7);
                    break;

            }
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {

        }

        // Add Caption on the Image
        private void guna2Button18_Click(object sender, EventArgs e)
        {
            CaptionInputForm captionInputForm = new();
            DialogResult result = captionInputForm.ShowDialog(); // Show as a modal dialog

            if (result == DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save Captioned Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string outputPath = saveFileDialog.FileName;
                    // Add caption on the image
                    CaptionOnImage.SetCaption(pictureBox1.Image, outputPath, captionInputForm.caption.Text);
                }
            }
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox3.Image = new Bitmap(openFileDialog.FileName);
                    }
                }
                Bitmap FirstImage = (Bitmap)pictureBox1.Image;
                Bitmap SecondImage = (Bitmap)pictureBox3.Image;
                int minWidth = Math.Min(FirstImage.Width, SecondImage.Width);
                int minHeight = Math.Min(FirstImage.Height, SecondImage.Height);

                int firstPixels = 0;
                int secondPixels = 0;
                for (int x = 0; x < minWidth; x++)
                {
                    for (int y = 0; y < minHeight; y++)
                    {
                        Color FirstImageOriginalPixel = FirstImage.GetPixel(x, y);
                        int FirstBrighteness = CalculateBrightnessLevel(FirstImageOriginalPixel);

                        Color SecondImageOriginalPixel = SecondImage.GetPixel(x, y);
                        int SecondBrighteness = CalculateBrightnessLevel(SecondImageOriginalPixel);

                        if (FirstBrighteness == SecondBrighteness)
                        {
                            continue;
                        }
                        if (FirstBrighteness > SecondBrighteness)
                        {
                            firstPixels++;
                        }
                        if (FirstBrighteness < SecondBrighteness)
                        {
                            secondPixels++;
                        }
                    }
                }
                if (firstPixels > secondPixels)
                {
                    MessageBox.Show("The Treatment Goes Well");
                    return;
                }
                else if (firstPixels < secondPixels)
                {
                    MessageBox.Show("The disease is progressing");
                    return;
                }
                else
                {
                    MessageBox.Show("There is no Change");
                    return;
                }

            }
            else
            {
                MessageBox.Show("You Have to Import First Image before to Compare with it");
                return;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}