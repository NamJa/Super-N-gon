using System;
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
        int circleSize = 1000;
        int even = 0;
        float degreeSum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            decimal decTotalSales, decBookSales, decPeriodicalSales, decRedPie;

            if (n <= 3)
                n = 3;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            int height = ClientSize.Height;
            int width = ClientSize.Width;
            try
            {
                decBookSales = (decimal)1 / n;
                try
                {
                    decPeriodicalSales = (decimal)1 / n;
                    try
                    {
                        decRedPie = (decimal)1 / n;
                        decTotalSales = decBookSales + decPeriodicalSales;

                        SolidBrush bookBrush = new SolidBrush(Color.Blue);
                        SolidBrush periodicalBrush = new SolidBrush(Color.Yellow);
                        SolidBrush redPie = new SolidBrush(Color.Red);
                        float intEndBook = (float)((decBookSales / decTotalSales * 360) / n);
                        float intEndPeriodical = (float)((decPeriodicalSales / decTotalSales * 360) / n);
                        float intEndRedPie = (float)((decRedPie / decTotalSales * 360) / n);
                        for (int i = 0; i < n ; i++)
                        {
                            if (decTotalSales != 0)
                            {
                                if (even % 3 == 0)
                                {
                                    g.FillPie(bookBrush, (this.ClientSize.Width / 2) - (circleSize / 2), (this.ClientSize.Height / 2) - (circleSize / 2), circleSize, circleSize, degreeSum, intEndBook * 2);
                                    degreeSum += intEndBook * 2;
                                    even++;
                                }
                                else if(even % 3 == 1)
                                {
                                    g.FillPie(periodicalBrush, (this.ClientSize.Width / 2) - (circleSize / 2), (this.ClientSize.Height / 2) - (circleSize / 2), circleSize, circleSize, degreeSum, intEndPeriodical * 2);
                                    degreeSum += intEndPeriodical * 2;
                                    even++;
                                }
                                else if (even % 3 == 2)
                                {
                                    g.FillPie(redPie, (this.ClientSize.Width / 2) - (circleSize / 2), (this.ClientSize.Height / 2) - (circleSize / 2), circleSize, circleSize, degreeSum, intEndRedPie * 2);
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
            DrawPolygon(g, pen, this.ClientSize.Width / 2, this.ClientSize.Height / 2, 50, 0, 360 / n);
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    n++;
                    Invalidate();
                    break;

                case Keys.Down:
                    n--;
                    if (n <= 3)
                        n = 3;
                    Invalidate();
                    break;

                case Keys.Left:
                    break;

                case Keys.Right:
                    break;
                
                case Keys.Space:
                    break;

                case Keys.Escape:
                    InitializeComponent();
                    break;
            }
        }

        public void DrawPolygon(Graphics graphics, Pen pen, int originX, int originY, double radius, double startDegree, double intervalDegree)
        {
            int polygonCount = Convert.ToInt32(360d / intervalDegree);

            List<Point> pointList = new List<Point>();

            for (double i = startDegree; i < startDegree + intervalDegree * polygonCount; i += intervalDegree)
            {
                pointList.Add(GetCirclePoint(originX, originY, radius, i));
            }

            graphics.FillPolygon(new SolidBrush(Color.FromArgb(0, 150, 136)),  pointList.ToArray());
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

        }
    }
}