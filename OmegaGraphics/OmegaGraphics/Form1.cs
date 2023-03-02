using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmegaGraphics
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        
        List<Point> path = new List<Point>();
        List<Point> localPoints = new List<Point>();

        int index = -1;
        int POINT_SIZE = 6;
        int HALF_SIZE = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = CreateGraphics();
            
            Timer timer = new Timer();
            timer.Interval = (100);
            timer.Tick += new EventHandler(Tick);
            timer.Start();
        }

        private void Tick(object sender, EventArgs e) 
        {
            // invokes Form1_Paint
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path.Clear();
            localPoints.Clear();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics.Clear(Color.White);

            if (path.Count > 2)
                graphics.DrawPolygon(Pens.HotPink, path.ToArray());

            path.ForEach(point => graphics.FillRectangle(Brushes.DarkGreen,
                point.X - HALF_SIZE,
                point.Y - HALF_SIZE,
                POINT_SIZE,
                POINT_SIZE)
            );


            localPoints.ForEach(point =>
            {
                if (path.Count == 3 && checkPoint(point))
                {
                    graphics.FillRectangle(Brushes.DarkSeaGreen,
                        point.X - HALF_SIZE, point.Y - HALF_SIZE, POINT_SIZE, POINT_SIZE);
                }
                else
                {
                    graphics.FillRectangle(Brushes.Red,
                        point.X - HALF_SIZE, point.Y - HALF_SIZE, POINT_SIZE, POINT_SIZE);
                }
            });
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // add new point
                path.Add(e.Location);
            }

            if (e.Button == MouseButtons.Right)
            {
                index = -1;
                for (int i = 0; i < path.Count; i++)
                {
                    if ((e.X > path[i].X - HALF_SIZE * 3) & (e.X < path[i].X + HALF_SIZE * 3) &
                        (e.Y > path[i].Y - HALF_SIZE * 3) & (e.Y < path[i].Y + HALF_SIZE * 3))
                    {
                        index = i;
                        break;
                    }
                }
                Console.WriteLine("Point number: " + index);
            }

            if (e.Button == MouseButtons.Middle)
            {
                localPoints.Add(e.Location);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            index = -1;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (index != -1) 
            {
                path[index] = new Point(e.X, e.Y);
            }
        }

        private bool checkPoint(Point point) 
        {
            Point p1 = path[0];
            Point p2 = path[1];
            Point p3 = path[2];

            int v1 = (p1.X - point.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (p1.Y - point.Y);
            int v2 = (p2.X - point.X) * (p3.Y - p2.Y) - (p3.X - p2.X) * (p2.Y - point.Y);
            int v3 = (p3.X - point.X) * (p1.Y - p3.Y) - (p1.X - p3.X) * (p3.Y - point.Y);

            if ((v1 >= 0 && v2 >= 0 && v3 >= 0) || (v1 <= 0 && v2 <= 0 && v3 <= 0))
                return true;

            return false;
        }
    }
}
