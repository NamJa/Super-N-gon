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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (n <= 3)
                n = 3;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            int height = ClientSize.Height;
            int width = ClientSize.Width;

            
            DrawPolygon(g, pen, this.ClientSize.Width / 2, this.Height / 2, 200, 60, 360/n);
            DrawPlygon(g, pen, this.ClientSize.Width / 2, this.Height / 2, 100, 60, 360/n);
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
        public void DrawPlygon(Graphics graphics, Pen pen, int originX, int originY, double radius, double startDegree, double intervalDegree)
        {
            int polygonCount = Convert.ToInt32(360d / intervalDegree);

            List<Point> pointList = new List<Point>();

            for (double i = startDegree; i < startDegree + intervalDegree * polygonCount; i += intervalDegree)
            {
                pointList.Add(GetCirclePoint(originX, originY, radius, i));
            }
            
            graphics.FillPolygon(new SolidBrush(Color.FromArgb(0, 200, 136)),  pointList.ToArray());
            for(int i = 0; i < n; i++)
            {
                MessageBox.Show(pointList[i].ToString());
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