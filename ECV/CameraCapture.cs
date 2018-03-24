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
            var filePath = @"data.csv";

            using (var wr = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                var sb = new System.Text.StringBuilder();

                sb.Append(name);
                sb.Append(",");

                frameGrayOriginal = frame.Convert<Gray, Byte>().PyrDown().PyrUp();
                Image<Gray, Byte> frameGray = frame.Convert<Gray, Byte>().PyrDown().PyrUp();
                Image<Gray, Byte> BGGray = BG.Convert<Gray, Byte>().PyrDown().PyrUp();

                CvInvoke.cvAbsDiff(BGGray, frameGray, frameGray);

                OriginalImageBox.Image = frameGray;

                CvInvoke.cvSmooth(frameGray, frameGray, SMOOTH_TYPE.CV_GAUSSIAN, 15, 15, 3, 1);

                frameGray = frameGray.ThresholdBinary(new Gray(60), new Gray(255));

                //OriginalImageBox.Image = frameGrayOriginal;


                StructuringElementEx rect_6 = new StructuringElementEx(8, 8, 5, 5, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);

                //dilating the source image using the specified structuring element
                CvInvoke.cvDilate(frameGray, frameGray, rect_6, 6);

                StructuringElementEx rect_12 = new StructuringElementEx(4, 4, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);

                //Eroding the source image using the specified structuring element
                CvInvoke.cvErode(frameGray, frameGray, rect_12, 4);
                resultImage = frameGray.And(frameGrayOriginal);

                FindLargestObject();

                TextBox.AppendText("Height : " + r.Height.ToString() + "\n");
                sb.Append(r.Height);
                sb.Append(",");
                TextBox.AppendText("Width : " + r.Width.ToString() + "\n");
                sb.Append(r.Width);
                sb.Append(",");
                TextBox.AppendText("Ratio : " + ((float)r.Width / (float)r.Height).ToString() + "\n");
                sb.Append((float)r.Width / (float)r.Height);
                sb.Append(",");



            }
        }

        public void FindLargestObject()
        {
            Contour<Point> largestContour = null;
            double largestarea = 0;

            using (MemStorage store = new MemStorage())
                for (var contours = resultImage.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, RETR_TYPE.CV_RETR_EXTERNAL); contours != null; contours = contours.HNext)
                {
                    if (contours.Area > largestarea)
                    {
                        largestarea = contours.Area;
                        largestContour = contours;

                    }
                }

            r = CvInvoke.cvBoundingRect(largestContour, 1);
            //frameGrayOriginal.Draw(r, new Gray(255), 5);

            Rectangle temp = r;

            resultImage.ROI = r;
            frame.ROI = r;

            temp.Location = new Point(temp.Left, (temp.Top + temp.Height / 2));
            temp.Height = temp.Height / 2;

            frameGrayOriginal.ROI = temp;

            //ProcessImageBox.Image = resultImage;
            //OriginalImageBox.Image = mask;

        }

        private void OriginalImageBox_Click(object sender, EventArgs e)
        {

        }
    }
    }
