using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.ComponentModel;

namespace xrayimageproject
{
    public partial class Form1 : Form
    {
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

            int colorValue = intensity *2;
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
            if (guna2ComboBox1.SelectedItem.ToString() == "Gray-Scale Mode")
            {
                Console.WriteLine("Gray-Scale Mode Selected");
                Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
                Bitmap grayscaleBitmap = ConvertToGrayscale(originalBitmap);
                pictureBox1.Image = grayscaleBitmap;
            }
            if (guna2ComboBox1.SelectedItem.ToString() == "RGB Mode")
            {
                Console.WriteLine("RGB Mode Selected");
                ConvertToRGB();
            }
            if (guna2ComboBox1.SelectedItem.ToString() == "CMY Mode")
            {
                Console.WriteLine("CMY Mode Selected");
                ConvertToCMYK();
            }
        }
        private void ConvertToRGB()
        {
            // Assuming 'pictureBox1' contains the original image
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            // No conversion needed for RGB, but you can ensure the pixel format if necessary
            Bitmap rgbBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(rgbBitmap))
            {
                g.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));
            }
            pictureBox1.Image = rgbBitmap;
        }
        private void ConvertToCMYK()
        {
            // CMYK conversion is complex and not natively supported by GDI+, but you can simulate it
            // Assuming 'pictureBox1' contains the original image
            Bitmap originalBitmap = new Bitmap(pictureBox1.Image);
            Bitmap cmykBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color pixelColor = originalBitmap.GetPixel(x, y);
                    // Simulate CMYK conversion by inverting the colors
                    Color cmykColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    cmykBitmap.SetPixel(x, y, cmykColor);
                }
            }
            pictureBox1.Image = cmykBitmap;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
            MessageBox.Show("Image copied to clipboard. Please paste it into your social media post.");

        }
        // Fourier button
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            float cutoffFrequency = 0.0f;
            Bitmap resultImage = HighAndLowPassFilter.ApplyFilter(pictureBox1.Image, cutoffFrequency, HighAndLowPassFilter.FilterType.High);
            pictureBox3.Image = resultImage;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            float cutoffFrequency = 30.0f;
            Bitmap resultImage = HighAndLowPassFilter.ApplyFilter(pictureBox1.Image, cutoffFrequency, HighAndLowPassFilter.FilterType.Low);
            pictureBox3.Image = resultImage;
        }
    }
    public class HighAndLowPassFilter
    {
        public enum FilterType
        {
            High,
            Low
        }

        public static Bitmap ApplyFilter(System.Drawing.Image image, float cutoffFrequency, FilterType filterType)
        {
            // Load the image
            Bitmap sourceImage = new Bitmap(image);

            // Convert the image to grayscale the parameters are the standard coefficients
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = grayscaleFilter.Apply(sourceImage);

            // Apply FFT to the grayscale image
            ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
            complexImage.ForwardFourierTransform();

            // Create a high-pass filter mask
            int width = grayImage.Width;
            int height = grayImage.Height;
            float[,] hpfMask = CreateFilterMask(width, height, cutoffFrequency, filterType);

            // Apply the high-pass filter mask to the FFT image
            ApplyMaskToComplexImage(complexImage, hpfMask);

            // Perform an inverse Fourier transform
            complexImage.BackwardFourierTransform();

            // Convert the complex image back to bitmap
            Bitmap hpfImage = complexImage.ToBitmap();

            return hpfImage;
        }
        private static float[,] CreateFilterMask(int width, int height, float cutoffFrequency, FilterType filterType)
        {
            float[,] mask = new float[width, height];
            int cx = width / 2;
            int cy = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));
                    if(filterType==FilterType.High)
                    {
                    mask[x, y] = distance > cutoffFrequency ? 1 : 0;
                    }
                    else if(filterType==FilterType.Low)
                    {
                        mask[x, y] = distance <= cutoffFrequency ? 1 : 0;
                    }
                }
            }

            return mask;
        }
        private static void ApplyMaskToComplexImage(ComplexImage complexImage, float[,] mask)
        {
            int width = complexImage.Width;
            int height = complexImage.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    complexImage.Data[x, y].Re *= mask[x, y];
                    complexImage.Data[x, y].Im *= mask[x, y];
                }
            }
        }
    }
}
