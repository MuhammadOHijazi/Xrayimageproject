using NAudio.Wave;
using System.DirectoryServices;
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

            SearchBox.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            SearchBox.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
            SearchBox.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            SearchBox.Paint += new PaintEventHandler(pictureBox_Paint);
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
                SearchBox.Invalidate();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SearchBox.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker.Value;
            DateSearcher dateSearcher = new(date);
            var results = dateSearcher.search();
            this.showResults(results);


        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.ReadOnly = true;
            this.handleClickingOnSearchResults(e);
        }

        private void handleClickingOnSearchResults(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cellValue = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value;
                if (cellValue != null)
                {
                    string path = cellValue.ToString();
                    this.handleMedia(path);
                }
            }
        }

        private void handleMedia(string path)
        {
            try
            {
                FileChecker checker = new FileChecker(path);
                string fileType = checker.check();

                if (fileType != null)
                {
                    if (fileType is "image")
                    {
                        Bitmap image = new Bitmap(path);
                        pictureBox1.Image = image;
                    }

                    else if (fileType is "audio")
                    {
                        axWindowsMediaPlayer1.Visible = true;
                        axWindowsMediaPlayer1.URL = path;
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(axWindowsMediaPlayer1_PlayStateChange);
                    }


                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File Not Found Exception");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            SizeSearcher sizeSearcher = new SizeSearcher(size);
            var results = sizeSearcher.search();

            this.showResults(results);
        }

        private void showResults(List<FileInfo> results)
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Columns.Clear();

            if (results.Count > 0)
            {
                guna2DataGridView1.Visible = true;
                guna2DataGridView1.Columns.Add("Name", "Name");
                guna2DataGridView1.Columns.Add("Path", "Path");


                foreach (FileInfo result in results)
                {
                    guna2DataGridView1.Rows.Add(result.Name, result.Directory + "\\" + result.Name);
                }

            }

            else
            {
                guna2DataGridView1.Visible = false;
            }
        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void axAcropdf1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1)
            {
                axWindowsMediaPlayer1.Visible = false;
            }
        }
    }
}
