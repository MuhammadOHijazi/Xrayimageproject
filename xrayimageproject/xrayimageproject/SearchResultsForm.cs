using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xrayimageproject
{
    public partial class SearchResultsForm : Form
    {
        public SearchResultsForm(List<string> files)
        {
            InitializeComponent();

            // Add files to the ListBox
            foreach (var file in files)
            {
                //listBox1.Items.Add(file);
            }
        }

        private void SearchResults_Load(object sender, EventArgs e)
        {

        }
    } 
}
