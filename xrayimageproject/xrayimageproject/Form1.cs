using System.Windows.Forms;

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

            pictureBox1.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            pictureBox1.Paint += new PaintEventHandler(pictureBox_Paint);
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
