using System;
using System.Windows.Forms;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using Emgu.CV.VideoSurveillance;
using System.IO;

namespace vehicle_detection
{
    public partial class Menu : Form
    {

        Image<Bgr, Byte> frame;
        Image<Bgr, Byte> BG;
        Image<Gray, Byte> frameGrayOriginal;
        Image<Gray, Byte> resultImage;
        int count = 0;
        Rectangle r;
        String name;

        public Menu()
        {
            InitializeComponent();
        }

     

        private void imagebutton_Click(object sender, EventArgs e)
        {

        }

        private void backgroundbutton_Click(object sender, EventArgs e)
        {
           
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void openFileDialogBg_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void imagebutton_Click_1(object sender, EventArgs e)
        {
            openFileDialogImage.FileName = "";
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image<Bgr, Byte> frameL = new Image<Bgr, Byte>(openFileDialogImage.FileName);
                    name = Path.GetFileName(openFileDialogImage.SafeFileName);
                    frame = frameL;
                }
                catch (NullReferenceException excpt)
                {
                    TextBox.AppendText(excpt.Message);
                }
            }
        }

        private void processbutton_Click(object sender, EventArgs e)
        {

        }
            
    }
}
