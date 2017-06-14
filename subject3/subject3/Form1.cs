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
        int even = 0;
        double randDegree = 0;
        float degreeSum = 0;
        int time = 0;

        Timer timer;
        List<Point> pointList;
        Graphics g;
        Pen pen;
        PointF[] pointf = new PointF[1];
        Matrix matrix = new Matrix();

        public Form1()
        {
            InitializeComponent();
            pointf[0] = new PointF(this.ClientSize.Width/2, this.ClientSize.Height/2);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            decimal decTotalPie, decBluePie, decYellowPie, decRedPie;

            if (n <= 3)
                n = 3;
             g = e.Graphics;
             pen = new Pen(Color.Red);
            int height = ClientSize.Height;
            int width = ClientSize.Width;
            try
            {
                decBluePie = (decimal)1 / n;
                try
                {
                    decYellowPie = (decimal)1 / n;
                    try
                    {
                        decRedPie = (decimal)1 / n;
                        decTotalPie = decBluePie + decYellowPie;

                        SolidBrush bluePie = new SolidBrush(Color.Blue);
                        SolidBrush yellowPie = new SolidBrush(Color.Yellow);
                        SolidBrush redPie = new SolidBrush(Color.Red);
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
                    }
                    catch
                    {

                    }
                }
                catch
                {

                }
            }
            catch
            {

            }
            DrawPolygon(g, pen, this.ClientSize.Width / 2, this.ClientSize.Height / 2, 50, randDegree, 360 / n);
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
                    randDegree += 30.0f;
                    Invalidate();
                    break;

                case Keys.Right:
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
                    timer.Stop();
                    break;
            }
        }

        public void DrawPolygon(Graphics graphics, Pen pen, int originX, int originY, double radius, double startDegree, double intervalDegree)
        {
            int polygonCount = Convert.ToInt32(360d / intervalDegree);
            pointList = new List<Point>();
            PointF pointf = new PointF();
            for (double i = startDegree; i < startDegree + intervalDegree * polygonCount; i += intervalDegree)
            {
                pointList.Add(GetCirclePoint(originX, originY, radius, i));
            }

            graphics.FillPolygon(new SolidBrush(Color.FromArgb(156, 39, 176)),  pointList.ToArray());
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

        public PointF rotatePoint(PointF point, PointF centroid, double angle)
        {
            float x = (centroid.X + (int)((point.X - centroid.X) * Math.Cos(angle) - (point.Y - centroid.Y) * Math.Sin(angle)));
            float y = (centroid.Y + (int)((point.X - centroid.X) * Math.Sin(angle) + (point.Y - centroid.Y) * Math.Cos(angle)));
            return new PointF(x, y);
        }

        private void StartGame()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            time++;
            label6.Text = time.ToString();
        }
    }
}