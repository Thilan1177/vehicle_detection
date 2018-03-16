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

namespace ECV
{
    public partial class CameraCapture : Form
    {
        Image<Bgr, Byte> frame;
        Image<Bgr, Byte> BG;
        Image<Gray, Byte> frameGrayOriginal;
        Image<Gray, Byte> resultImage;
        int count = 0;
        Rectangle r;
        String name;

        public CameraCapture()
        {
            InitializeComponent();
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            OpenFileDialogBG.FileName = "";
            if (OpenFileDialogBG.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image<Bgr, Byte> BGL = new Image<Bgr, Byte>(OpenFileDialogBG.FileName);
                    BG = BGL;
                }

                catch (NullReferenceException excpt)
                {
                    TextBox.AppendText(excpt.Message);
                }
            }
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnBrowsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog.FileName = "";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image<Bgr, Byte> frameL = new Image<Bgr, Byte>(OpenFileDialog.FileName);
                    name = Path.GetFileName(OpenFileDialog.SafeFileName);
                    frame = frameL;
                }
                catch (NullReferenceException excpt)
                {
                    TextBox.AppendText(excpt.Message);
                }
            }
        }


        private void ptnProcess_Click(object sender, EventArgs e)
        {

        }


        }
    }
