using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace xrayimageproject
{
    public partial class Form1 : Form
    {
        // Declare variables to track the selection
        bool isSelecting = false;
        Point selectionStart, selectionEnd;

        public Form1()
        {
            InitializeComponent();
            this.panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            this.panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            this.panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);

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
                // to do later here, handle the selected part of the image

            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isSelecting)
            {
                // Draw a rectangle to represent the selected area
                Rectangle rect = GetRectangle(selectionStart, selectionEnd);
                e.Graphics.DrawRectangle(Pens.White, rect);
            }
        }

        // Helper method to get a rectangle from two points, the first location will be the smalleset two points
        // and the second location will be the absolute distance 
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
            // Create a blank bitmap with the same dimensions as the original
            Bitmap grayscaleBitmap = new Bitmap(original.Width, original.Height);

            // Create a graphics object for the new bitmap
            using (Graphics g = Graphics.FromImage(grayscaleBitmap))
            {
                // Define the color matrix
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
                    });

                // Create image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    // Set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    // Draw the original image on the new image using the grayscale color matrix
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
                    Bitmap originalBitmap = new Bitmap(openFileDialog.FileName);
                    Bitmap grayscaleBitmap = ConvertToGrayscale(originalBitmap);
                    pictureBox1.Image = grayscaleBitmap;
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
    }
}
