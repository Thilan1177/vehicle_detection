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
        String vehicleType;

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

            vehicleType = textBoxvehicleType.Text;
            if (vehicleType == "")
            {
                MessageBox.Show("Please Enter Vehicle Type");
            }
            else
            {


                var filePath = @"data.csv";

                using (var wr = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
                {
                    var sb = new System.Text.StringBuilder();

                    sb.Append(vehicleType);
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


                    Image<Gray, Byte> cannyEdges = resultImage.Canny(150, 80);



                    StructuringElementEx rect_2 = new StructuringElementEx(4, 4, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
                    CvInvoke.cvDilate(cannyEdges, cannyEdges, rect_2, 1);

                    //OriginalImageBox.Image = cannyEdges;

                    CvInvoke.cvErode(cannyEdges, cannyEdges, rect_2, 1);

                    //OriginalImageBox.Image = cannyEdges;

                    Image<Gray, float> cornerStrength = new Image<Gray, float>(cannyEdges.Size);
                    CvInvoke.cvCornerHarris(
                        cannyEdges,       //source image
                        cornerStrength, //result image
                        3,           //neighborhood size
                        3,           //aperture size
                        0.2);       //Harris parameter
                                    //threshold the corner strengths
                    cornerStrength._ThresholdBinary(new Gray(0.01), new Gray(255));

                    int contCorners = 0;
                    for (int x = 0; x < cornerStrength.Width; x++)
                    {
                        for (int y = 0; y < cornerStrength.Height; y++)
                        {
                            Gray imagenP = cornerStrength[y, x];
                            if (imagenP.Intensity == 255)
                            {
                                contCorners++;
                            }
                        }
                    }

                    sb.Append(contCorners);
                    sb.Append(",");

                    // ProcessImageBox.Image = cornerStrength;

                    LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
                         3, //Distance resolution in pixel-related units
                         Math.PI / 2, //Angle resolution measured in radians.
                         150, //threshold
                         80, //min Line width
                         10 //gap between lines
                         )[0]; //Get the lines from the first channel

                    foreach (LineSegment2D line in lines)
                    {
                        frame.Draw(line, new Bgr(Color.Red), 2);
                        count++;
                    }

                    //TextBox.AppendText("Horizontal Lines : " + count.ToString() + "\n");

                    sb.Append(count);
                    sb.Append(",");

                    count = 0;

                    lines = cannyEdges.HoughLinesBinary(
                         1, //Distance resolution in pixel-related units
                         Math.PI, //Angle resolution measured in radians.
                         80, //threshold
                         70, //min Line width
                         5 //gap between lines
                         )[0]; //Get the lines from the first channel

                    foreach (LineSegment2D line in lines)
                    {
                        frame.Draw(line, new Bgr(Color.White), 2);
                        count++;
                    }
                    // TextBox.AppendText("vertical Lines : " + count.ToString() + "\n");

                    sb.Append(count);
                    sb.Append(",");

                    count = 0;

                    lines = cannyEdges.HoughLinesBinary(
                        3, //Distance resolution in pixel-related units
                        Math.PI / 4, //Angle resolution measured in radians.
                        150, //threshold
                        100, //min Line width
                        8 //gap between lines
                        )[0]; //Get the lines from the first channel

                    foreach (LineSegment2D line in lines)
                    {
                        frame.Draw(line, new Bgr(Color.LightPink), 2);
                        count++;
                    }
                    // TextBox.AppendText("30 Lines : " + count.ToString() + "\n");

                    sb.Append(count);
                    sb.Append(",");

                    count = 0;

                    lines = cannyEdges.HoughLinesBinary(
                        3, //Distance resolution in pixel-related units
                        Math.PI / 3, //Angle resolution measured in radians.
                        50, //threshold
                        100, //min Line width
                        8 //gap between lines
                        )[0]; //Get the lines from the first channel

                    foreach (LineSegment2D line in lines)
                    {
                        frame.Draw(line, new Bgr(Color.LightPink), 2);
                        count++;
                    }
                    // TextBox.AppendText("60 Lines : " + count.ToString() + "\n");

                    sb.Append(count);
                    sb.Append(",");

                    count = 0;

                    lines = cannyEdges.HoughLinesBinary(
                        3, //Distance resolution in pixel-related units
                        Math.PI / 6, //Angle resolution measured in radians.
                        50, //threshold
                        100, //min Line width
                        8 //gap between lines
                        )[0]; //Get the lines from the first channel

                    foreach (LineSegment2D line in lines)
                    {
                        frame.Draw(line, new Bgr(Color.LightPink), 2);
                        count++;
                    }
                    // TextBox.AppendText("45 Lines : " + count.ToString() + "\n");

                    sb.Append(count);
                    sb.Append(",");

                    List<MCvBox2D> boxList1 = new List<MCvBox2D>();
                    List<MCvBox2D> boxList2 = new List<MCvBox2D>();
                    List<MCvBox2D> boxList3 = new List<MCvBox2D>();
                    List<MCvBox2D> boxList4 = new List<MCvBox2D>();
                    List<MCvBox2D> boxList5 = new List<MCvBox2D>();
                    List<MCvBox2D> boxList6 = new List<MCvBox2D>();

                    List<Triangle2DF> triangleList1 = new List<Triangle2DF>();
                    List<Triangle2DF> triangleList2 = new List<Triangle2DF>();
                    List<Triangle2DF> triangleList3 = new List<Triangle2DF>();
                    List<Triangle2DF> triangleList4 = new List<Triangle2DF>();
                    List<Triangle2DF> triangleList5 = new List<Triangle2DF>();

                    List<Contour<Point>> pentagonList1 = new List<Contour<Point>>();
                    List<Contour<Point>> pentagonList2 = new List<Contour<Point>>();
                    List<Contour<Point>> pentagonList3 = new List<Contour<Point>>();
                    List<Contour<Point>> pentagonList4 = new List<Contour<Point>>();
                    List<Contour<Point>> pentagonList5 = new List<Contour<Point>>();
                    List<Contour<Point>> pentagonList6 = new List<Contour<Point>>();

                    using (MemStorage storage = new MemStorage())
                        for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                        {
                            Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                            if (currentContour.Total == 3) //The contour has 3 vertices, it is a triangle
                            {
                                if (contours.Area > 30 && contours.Area <= 100)
                                {
                                    Point[] pts_1 = currentContour.ToArray();
                                    triangleList1.Add(new Triangle2DF(pts_1[0], pts_1[1], pts_1[2]));
                                }
                                else if (contours.Area <= 200)
                                {
                                    Point[] pts_2 = currentContour.ToArray();
                                    triangleList2.Add(new Triangle2DF(pts_2[0], pts_2[1], pts_2[2]));
                                }
                                else if (contours.Area <= 300)
                                {
                                    Point[] pts_3 = currentContour.ToArray();
                                    triangleList3.Add(new Triangle2DF(pts_3[0], pts_3[1], pts_3[2]));
                                }
                                else if (contours.Area <= 400)
                                {
                                    Point[] pts_4 = currentContour.ToArray();
                                    triangleList4.Add(new Triangle2DF(pts_4[0], pts_4[1], pts_4[2]));
                                }
                                else
                                {
                                    Point[] pts_5 = currentContour.ToArray();
                                    triangleList5.Add(new Triangle2DF(pts_5[0], pts_5[1], pts_5[2]));
                                }
                            }
                            else if (currentContour.Total == 4)
                            {
                                if (contours.Area > 30 && contours.Area < 50)
                                {
                                    bool isRectangle = true;
                                    Point[] pts1 = currentContour.ToArray();
                                    LineSegment2D[] edges1 = PointCollection.PolyLine(pts1, true);
                                    for (int i = 0; i < edges1.Length; i++)
                                    {
                                        double angle = Math.Abs(edges1[(i + 1) % edges1.Length].GetExteriorAngleDegree(edges1[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList1.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                                else if (contours.Area <= 100)
                                {
                                    bool isRectangle = true;
                                    Point[] pts2 = currentContour.ToArray();
                                    LineSegment2D[] edges2 = PointCollection.PolyLine(pts2, true);
                                    for (int i = 0; i < edges2.Length; i++)
                                    {
                                        double angle = Math.Abs(edges2[(i + 1) % edges2.Length].GetExteriorAngleDegree(edges2[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList2.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                                else if (contours.Area <= 200)
                                {
                                    bool isRectangle = true;
                                    Point[] pts3 = currentContour.ToArray();
                                    LineSegment2D[] edges3 = PointCollection.PolyLine(pts3, true);
                                    //using edges i found coordinates.
                                    for (int i = 0; i < edges3.Length; i++)
                                    {
                                        double angle = Math.Abs(edges3[(i + 1) % edges3.Length].GetExteriorAngleDegree(edges3[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList3.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                                else if (contours.Area <= 300)
                                {
                                    bool isRectangle = true;
                                    Point[] pts4 = currentContour.ToArray();
                                    LineSegment2D[] edges4 = PointCollection.PolyLine(pts4, true);
                                    //using edges i found coordinates.
                                    for (int i = 0; i < edges4.Length; i++)
                                    {
                                        double angle = Math.Abs(edges4[(i + 1) % edges4.Length].GetExteriorAngleDegree(edges4[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList4.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                                else if (contours.Area <= 400)
                                {
                                    bool isRectangle = true;
                                    Point[] pts5 = currentContour.ToArray();
                                    LineSegment2D[] edges5 = PointCollection.PolyLine(pts5, true);
                                    //using edges i found coordinates.
                                    for (int i = 0; i < edges5.Length; i++)
                                    {
                                        double angle = Math.Abs(edges5[(i + 1) % edges5.Length].GetExteriorAngleDegree(edges5[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList5.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                                else
                                {
                                    bool isRectangle = true;
                                    Point[] pts6 = currentContour.ToArray();
                                    LineSegment2D[] edges6 = PointCollection.PolyLine(pts6, true);
                                    //using edges i found coordinates.
                                    for (int i = 0; i < edges6.Length; i++)
                                    {
                                        double angle = Math.Abs(edges6[(i + 1) % edges6.Length].GetExteriorAngleDegree(edges6[i]));

                                        if (angle < 88 || angle > 92)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                        if (isRectangle)
                                            boxList6.Add(currentContour.GetMinAreaRect());
                                    }
                                }
                            }

                            else if (currentContour.Total == 5)
                            {
                                if (contours.Area > 20 && contours.Area <= 50)
                                {
                                    pentagonList1.Add(currentContour); // Add to list of pentagons

                                    Point[] ptPoints1 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints1[0].X + ptPoints1[1].X + ptPoints1[2].X + ptPoints1[3].X + ptPoints1[4].X) / 5;
                                    int dotY = (ptPoints1[0].Y + ptPoints1[1].Y + ptPoints1[2].Y + ptPoints1[3].Y + ptPoints1[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);

                                }
                                else if (contours.Area <= 100)
                                {
                                    pentagonList2.Add(currentContour);

                                    Point[] ptPoints2 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints2[0].X + ptPoints2[1].X + ptPoints2[2].X + ptPoints2[3].X + ptPoints2[4].X) / 5;
                                    int dotY = (ptPoints2[0].Y + ptPoints2[1].Y + ptPoints2[2].Y + ptPoints2[3].Y + ptPoints2[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);
                                }
                                else if (contours.Area <= 200)
                                {
                                    pentagonList3.Add(currentContour);

                                    Point[] ptPoints3 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints3[0].X + ptPoints3[1].X + ptPoints3[2].X + ptPoints3[3].X + ptPoints3[4].X) / 5;
                                    int dotY = (ptPoints3[0].Y + ptPoints3[1].Y + ptPoints3[2].Y + ptPoints3[3].Y + ptPoints3[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);
                                }
                                else if (contours.Area <= 300)
                                {
                                    pentagonList4.Add(currentContour);

                                    Point[] ptPoints4 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints4[0].X + ptPoints4[1].X + ptPoints4[2].X + ptPoints4[3].X + ptPoints4[4].X) / 5;
                                    int dotY = (ptPoints4[0].Y + ptPoints4[1].Y + ptPoints4[2].Y + ptPoints4[3].Y + ptPoints4[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);
                                }
                                else if (contours.Area <= 400)
                                {
                                    pentagonList5.Add(currentContour);

                                    Point[] ptPoints5 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints5[0].X + ptPoints5[1].X + ptPoints5[2].X + ptPoints5[3].X + ptPoints5[4].X) / 5;
                                    int dotY = (ptPoints5[0].Y + ptPoints5[1].Y + ptPoints5[2].Y + ptPoints5[3].Y + ptPoints5[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);
                                }
                                else
                                {
                                    pentagonList6.Add(currentContour);

                                    Point[] ptPoints6 = contours.ToArray(); // Get contour points
                                    frame.Draw(contours, new Bgr(Color.Purple), 2);
                                    int dotX = (ptPoints6[0].X + ptPoints6[1].X + ptPoints6[2].X + ptPoints6[3].X + ptPoints6[4].X) / 5;
                                    int dotY = (ptPoints6[0].Y + ptPoints6[1].Y + ptPoints6[2].Y + ptPoints6[3].Y + ptPoints6[4].Y) / 5;
                                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                    frame.Draw(dot, new Bgr(Color.Purple), -1);
                                }
                            }
                        }

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList1)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList2)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList3)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList4)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList5)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Contour<Point> pentagon in pentagonList6)
                    {
                        frame.Draw(pentagon, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (MCvBox2D box in boxList1)
                    {
                        frame.Draw(box, new Bgr(Color.Purple), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (MCvBox2D box in boxList2)
                    {
                        frame.Draw(box, new Bgr(Color.LightBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (MCvBox2D box in boxList3)
                    {
                        frame.Draw(box, new Bgr(Color.LightBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (MCvBox2D box in boxList4)
                    {
                        frame.Draw(box, new Bgr(Color.LightBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (MCvBox2D box in boxList5)
                    {
                        frame.Draw(box, new Bgr(Color.LightBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Triangle2DF triangle in triangleList1)
                    {
                        frame.Draw(triangle, new Bgr(Color.DarkBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Triangle2DF triangle in triangleList2)
                    {
                        frame.Draw(triangle, new Bgr(Color.DarkBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Triangle2DF triangle in triangleList3)
                    {
                        frame.Draw(triangle, new Bgr(Color.DarkBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Triangle2DF triangle in triangleList4)
                    {
                        frame.Draw(triangle, new Bgr(Color.DarkBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;
                    foreach (Triangle2DF triangle in triangleList5)
                    {
                        frame.Draw(triangle, new Bgr(Color.DarkBlue), 2);
                        count++;
                    }
                    sb.Append(count);
                    sb.Append(",");

                    count = 0;

                    Gray cannyThreshold = new Gray(100);
                    Gray circleAccumulatorThreshold = new Gray(70);

                    CircleF[] circles = frameGrayOriginal.HoughCircles(
                                            cannyThreshold,
                                            circleAccumulatorThreshold,
                                            2.0, //Resolution of the accumulator used to detect centers of the circles
                                            160.0, //min distance 
                                            20, //min radius
                                            50 //max radius
                                            )[0]; //Get the circles from the first channel

                    float rad = 0;
                    foreach (CircleF circle in circles)
                    {
                        //frame.Draw(circle, new Bgr(Color.Brown), 2);
                        TextBox.AppendText("Radius : " + circle.Radius.ToString() + "\n");
                        rad = rad + circle.Radius;
                    }

                    sb.Append(circles[0].Radius);
                    sb.Append(",");
                    sb.Append(circles[1].Radius);
                    sb.Append(",");
                    double dist = Math.Sqrt(Math.Pow(circles[0].Center.X - circles[1].Center.X, 2) + Math.Pow(circles[0].Center.Y - circles[1].Center.Y, 2));
                    sb.Append(dist);
                    sb.Append(",");
                    //TextBox.AppendText("circles : " + circles.Length.ToString() + "\n");
                    sb.Append(circles.Length);
                    sb.Append(",");
                    //TextBox.AppendText("Avg radius : " + (rad / circles.Length).ToString() + "\n");
                    sb.Append(rad / circles.Length);

                    ProcessImageBox.Image = frame;

                    wr.WriteLine(sb.ToString());

                }
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
