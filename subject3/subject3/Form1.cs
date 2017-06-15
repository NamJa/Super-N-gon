using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace subject3
{
    public partial class Form1 : Form
    {
        int n = 3;
        int backgroundSize = 1000;
        double randDegree = 0.0f;
        int rightRandRotate = 0;
        float degreeSum = 0;
        int time = 0;
        int rotateDirect = 0;

        Timer scoreTimer, randRotateTimer, playerRotateTimer;
        List<Point> pointList;
        Graphics g;
        Random rand = new Random();
        Pen bigPolygonPen, trianglePen, whitepen;
        PointF[] point;
        PointF center;
        Matrix matrix = new Matrix();

        public Form1()
        {
            InitializeComponent();
            center.X = this.ClientSize.Width / 2.0f;
            center.Y = this.ClientSize.Height / 2.0f;
            point = new PointF[] {new PointF(this.ClientSize.Width/2.0f + 25.0f, this.ClientSize.Height/2.0f + 45.0f),
                                         new PointF(this.ClientSize.Width/2.0f + 35.0f, this.ClientSize.Height/2.0f + 50.0f),
                                         new PointF(this.ClientSize.Width/2.0f + 37.0f, this.ClientSize.Height/2.0f + 38.0f)};
            whitepen = new Pen(Color.White, 30);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int even = 0;
            decimal decTotalPie, decBluePie, decYellowPie, decRedPie;

            if (n <= 3)
                n = 3;
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            bigPolygonPen = new Pen(Color.Red);
            trianglePen = new Pen(Color.Aqua);
            int height = ClientSize.Height;
            int width = ClientSize.Width;

                decBluePie = (decimal)1 / n;
                decYellowPie = (decimal)1 / n;
                decRedPie = (decimal)1 / n;
                decTotalPie = decBluePie + decYellowPie;

                SolidBrush bluePie = new SolidBrush(Color.FromArgb(63, 81, 181));
                SolidBrush yellowPie = new SolidBrush(Color.FromArgb(255, 235, 59));
                SolidBrush redPie = new SolidBrush(Color.FromArgb(244, 67, 54));
                float intEndBook = (float)((decBluePie / decTotalPie * 360) / n);
                float intEndPeriodical = (float)((decYellowPie / decTotalPie * 360) / n);
                float intEndRedPie = (float)((decRedPie / decTotalPie * 360) / n);
                for (int i = 0; i < n ; i++)
                {
                    if (decTotalPie != 0)
                    {
                        if (even % 3 == 0)
                        {
                            g.FillPie(bluePie, (this.ClientSize.Width / 2) - (backgroundSize / 2), (this.ClientSize.Height / 2) - (backgroundSize / 2), backgroundSize, backgroundSize, degreeSum+(float)randDegree, intEndBook * 2);
                            degreeSum += intEndBook * 2;
                            even++;
                        }
                        else if (even % 3 == 1)
                        {
                            g.FillPie(yellowPie, (this.ClientSize.Width / 2) - (backgroundSize / 2), (this.ClientSize.Height / 2) - (backgroundSize / 2), backgroundSize, backgroundSize, degreeSum+(float)randDegree, intEndPeriodical * 2);
                            degreeSum += intEndPeriodical * 2;
                            even++;
                        }
                        else if (even % 3 == 2)
                        {
                            g.FillPie(redPie, (this.ClientSize.Width / 2) - (backgroundSize / 2), (this.ClientSize.Height / 2) - (backgroundSize / 2), backgroundSize, backgroundSize, degreeSum+(float)randDegree, intEndRedPie * 2);
                            degreeSum += intEndRedPie * 2;
                            even++;
                        }
                    }
                }

            DrawPolygon(g, bigPolygonPen, this.ClientSize.Width / 2, this.ClientSize.Height / 2, 40, randDegree, 360 / n);
            //DrawPolygonLine(g, bigPolygonPen, this.ClientSize.Width / 2, this.ClientSize.Height / 2, 100, randDegree, 360 / n);
            DrawTriangle(g, trianglePen);
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    n++;
                    Invalidate();
                    label2.Text = n.ToString();
                    break;

                case Keys.Down:
                    n--;
                    if (n <= 3)
                        n = 3;
                    Invalidate();
                    label2.Text = n.ToString();
                    break;

                case Keys.Left:
                    
                    point[0] = Rotate(point[0], center, -25.05f);
                    point[1] = Rotate(point[1], center, -25.0f);
                    point[2] = Rotate(point[2], center, -25.0f);
                    Invalidate();
                    break;

                case Keys.Right:
                    point[0] = Rotate(point[0], center, 25.05f);
                    point[1] = Rotate(point[1], center, 25.0f);
                    point[2] = Rotate(point[2], center, 25.0f);
                    Invalidate();
                    break;
                
                case Keys.Space:
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    StartGame();
                    break;

                case Keys.Escape:
                    label1.Text = "SUPER";
                    label2.Text = n.ToString();
                    label3.Text = "-GON";
                    label4.Text = "PRESS SPACE TO START";
                    scoreTimer.Stop();
                    randRotateTimer.Stop();
                    playerRotateTimer.Stop();
                    time = 0;
                    label6.Text = time.ToString();
                    break;
            }
        }
        public PointF Rotate(PointF sourcePoint, PointF centerPoint, double rotateAngle)
        {
            PointF targetPoint = new Point();

            double radian = rotateAngle / 180 * Math.PI;
            double x, y;

            x = Math.Cos(radian) * (sourcePoint.X - centerPoint.X) - Math.Sin(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.X;
            y = Math.Sin(radian) * (sourcePoint.X - centerPoint.X) + Math.Cos(radian) * (sourcePoint.Y - centerPoint.Y) + centerPoint.Y;

            targetPoint.X = Convert.ToInt32(x);
            targetPoint.Y = Convert.ToInt32(y);

            return targetPoint;
        }

        public void DrawPolygon(Graphics graphics, Pen pen, int originX, int originY, double radius, double startDegree, double intervalDegree)
        {
            int polygonCount = Convert.ToInt32(360d / intervalDegree);
            pointList = new List<Point>();
            for (double i = startDegree; i < startDegree + intervalDegree * polygonCount; i += intervalDegree)
            {
                pointList.Add(GetCirclePoint(originX, originY, radius, i));
            }

            graphics.FillPolygon(new SolidBrush(Color.FromArgb(156, 39, 176)),  pointList.ToArray());
        }


        public void DrawPolygonLine(Graphics graphics, Pen pen, int originX, int originY, double radius, double startDegree, double intervalDegree)
        {
            int polygonCount = Convert.ToInt32(360d / intervalDegree);
            pointList = new List<Point>();
            for (double i = startDegree; i < startDegree + intervalDegree * polygonCount; i += intervalDegree)
            {
                pointList.Add(GetCirclePoint(originX, originY, radius, i));
            }
            graphics.DrawLines(whitepen, pointList.ToArray());
        }


        public void DrawTriangle(Graphics graphics, Pen pen)
        {
            graphics.FillPolygon(new SolidBrush(Color.Aqua), point);
        }

        public Point GetCirclePoint(int originX, int originY, double radius, double degree)
        {
            double radian = GetRadianValue(degree);

            return new Point
            (
                originX + Convert.ToInt32(Math.Cos(radian) * radius),
                originY + Convert.ToInt32(Math.Sin(radian) * radius)
            );
        }
        public double GetRadianValue(double angle)
        {
            return angle * (Math.PI / 180d);
        }

        private void StartGame()
        {
            scoreTimer = new Timer();
            scoreTimer.Interval = 100;
            scoreTimer.Tick += new EventHandler(IncreaseScore);
            scoreTimer.Start();

            randRotateTimer = new Timer();
            randRotateTimer.Interval = 40;
            randRotateTimer.Tick += new EventHandler(FormRightRotate);
            randRotateTimer.Start();

            playerRotateTimer = new Timer();
            playerRotateTimer.Interval = 40;
            playerRotateTimer.Tick += new EventHandler(PlayerRotate);
            playerRotateTimer.Start();
        }

        private void PlayerRotate(object sender, EventArgs e)
        {
            if (rotateDirect == 0)
            {
                point[0] = Rotate(point[0], center, 5.0f);
                point[1] = Rotate(point[1], center, 5.0f);
                point[2] = Rotate(point[2], center, 5.0f);
                Invalidate();
            }
            else
            {
                point[0] = Rotate(point[0], center, -5.0f);
                point[1] = Rotate(point[1], center, -5.0f);
                point[2] = Rotate(point[2], center, -5.0f);
                Invalidate();
            }
        }

        private void FormRightRotate(object sender, EventArgs e)
        {
            rightRandRotate = rand.Next(200, 600);
            if (rotateDirect == 0)
            {
                randDegree += 5.0f;
                Invalidate();
                if (randDegree >= rightRandRotate)
                    rotateDirect++;
            }
            else
            {
                randDegree -= 5.0f;
                Invalidate();
                if (randDegree <= 0)
                    rotateDirect--;
            }
        }

        private void IncreaseScore(object sender, EventArgs e)
        {
            time++;
            label6.Text = time.ToString();
        }
    }
}