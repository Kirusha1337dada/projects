using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
            button1.Click += button1_Click;
        }

        List<MyPoint> _points = new List<MyPoint>();

        class MyPoint
        {
            public bool Connected;
            public Point Point;

        }
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;



            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
            }

            public int GetCountPoints()
            {
                return index;
            }

            public Point[] GetPoints()
            {
                return points;
            }
        }

        private bool isMouse = false;
        private Point _p;
        private int _index = -1;

        private ArrayPoints arrayPoints = new ArrayPoints(2);

        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;

        Pen pen = new Pen(Color.Black, 3f);

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) { return; }

            arrayPoints.SetPoint(e.X, e.Y);

            if (arrayPoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
            graphics.Clear(pictureBox2.BackColor);
            pictureBox2.Image = map;
            _points.Clear();
            pictureBox2.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }



        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                e.Graphics.DrawEllipse(pen, _points[i].Point.X - 4, _points[i].Point.Y - 4, 8, 8);
                _points[i].Connected = false;
            }

            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].Connected == false)
                {
                    double nx = 0;
                    double ny = 0;
                    int p = 0;
                    for (int n = 0; n < _points.Count; n++)
                    {
                        if (_points[n].Connected == false)
                        {
                            double t = Math.Abs(Math.Abs(_points[n].Point.X) - Math.Abs(_points[i].Point.X));
                            if (t > nx)
                            {
                                nx = t;
                                p = n;
                            }
                            double d = Math.Abs(Math.Abs(_points[n].Point.Y) - Math.Abs(_points[i].Point.Y));
                            if (d > nx)
                            {
                                ny = d;
                                p = n;
                            }
                        }
                    }
                    if (p != 0)
                    {
                        _points[i].Connected = true;
                        _points[p].Connected = true;
                        e.Graphics.DrawLine(pen, _points[i].Point.X, _points[i].Point.Y, _points[p].Point.X, _points[p].Point.Y);
                    }
                }
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                isMouse = false;
                _index = -1;
            }
            else
            {
                MyPoint p = new MyPoint();
                p.Point.X = e.X;
                p.Point.Y = e.Y;
                _points.Add(p);
                pictureBox2.Invalidate();
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                Rectangle rect = new Rectangle(_points[i].Point.X - 4, _points[i].Point.Y - 4, 8, 8);
                if (rect.Contains(e.Location))
                {
                    isMouse = true;
                    _p = new Point(e.X - _points[i].Point.X, e.Y - _points[i].Point.Y);
                    _index = i;
                    break;
                }
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouse && _index >= 0 && _index < _points.Count)
            {
                _points[_index].Point = new Point(e.X - _p.X, e.Y - _p.Y);
                pictureBox2.Invalidate();
            }
        }

    }
    
}
