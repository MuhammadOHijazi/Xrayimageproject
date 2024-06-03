using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace xrayimageproject
{   
    public partial class LayerItem : UserControl
    {
        public LayerItem()
        {
            InitializeComponent();
        }
            public LayerItem(Bitmap image, bool isVisible=true ,int id = 0 )
        {
            InitializeComponent();
            _title = "Layer"+(id+1).ToString();
            _isVisible = isVisible;
            _image = image;
            _id = id;
            label1.Text = "Layer " + (id+1).ToString();
            editingPictureBox.Image = image;
           
        }
       // static X_Ray x = new X_Ray();
        private String _title;
        private bool _isVisible;
        private Bitmap _image;
        private int _id;

        public int ID
        {       
            get { return _id; }
            set { _id = value; }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; } 
        }
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }
        public Bitmap Image
        {
            get { return _image; }
             set { _image = value; }
        }
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            Form1.changeVisiblityToLayerNumber(ID);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }   
}
