using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECV
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Recognition re = new Recognition();
            re.Show();
            this.Hide();
        }

        private void buttonImageProcessing_Click(object sender, EventArgs e)
        {
            CameraCapture cc = new CameraCapture();
            cc.Show();
            this.Hide();
        }

        private void processImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraCapture cc = new CameraCapture();
            cc.Show();
            this.Hide();
        }
    }
}
