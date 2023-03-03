using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEKESGRAPHICAPPLICATION
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Pen rotatePen = new Pen(Color.HotPink, 3);
        bool rotateLeft = false;

        List<Point> polygon = new List<Point>();
        int polygonStep = 1;
        bool movePolygon = false;
        Point polygonPoint;
        Point polygonNext;

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
            timer.Interval = (50);
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
            movePolygon = false;
        }

        private void rotateFigureLeft()
        {
            Point r = new Point(0, 0);

            for (int i = 0; i < path.Count; ++i)
            {
                r.X += path[i].X;
                r.Y += path[i].Y;
            }

            r.X /= path.Count;
            r.Y /= path.Count;

            int angle = 10;
            double angleRadian = angle * Math.PI / 180;

            for (int i = 0; i < path.Count; ++i)
            {
                float x = (float)((path[i].X - r.X) * Math.Cos(angleRadian) - (path[i].Y - r.Y) * Math.Sin(angleRadian) + r.X);
                float y = (float)((path[i].X - r.X) * Math.Sin(angleRadian) + (path[i].Y - r.Y) * Math.Cos(angleRadian) + r.Y);
                path[i] = new Point((int)x, (int)y);
            }
        }

        private void rotatePolygonLeft()
        {
            Point r = new Point(0, 0);

            for (int i = 0; i < polygon.Count; ++i)
            {
                r.X += polygon[i].X;
                r.Y += polygon[i].Y;
            }

            r.X /= polygon.Count;
            r.Y /= polygon.Count;

            polygonStep = polygonStep > 10 ? 1 : polygonStep + 1;
            int angle = 10 * polygonStep;
            double angleRadian = angle * Math.PI / 180;

            for (int i = 0; i < polygon.Count; ++i)
            {
                float x = (float)((polygon[i].X - r.X) * Math.Cos(angleRadian) - (polygon[i].Y - r.Y) * Math.Sin(angleRadian) + r.X);
                float y = (float)((polygon[i].X - r.X) * Math.Sin(angleRadian) + (polygon[i].Y - r.Y) * Math.Cos(angleRadian) + r.Y);
                polygon[i] = new Point((int)x, (int)y);
            }

        }

        private void movePolygonAlongFigure()
        {
            polygonPoint = MovePointTowards(polygonPoint, polygonNext, 5);
            if (polygonPoint.X >= polygonNext.X && polygonPoint.Y >= polygonNext.Y)
            {
                for (int i = 0; i < path.Count; ++i)
                {
                    if (polygonNext == path[i])
                    {
                        polygonNext = i + 1 < path.Count ? path[i + 1] : path[0];
                        polygonPoint = path[i];
                        break;
                    }
                }
            }

            polygon.Clear();
            polygon.Add(new Point(polygonPoint.X - 20, polygonPoint.Y + 20));
            polygon.Add(new Point(polygonPoint.X + 20, polygonPoint.Y + 20));
            polygon.Add(new Point(polygonPoint.X + 20, polygonPoint.Y - 20));
            polygon.Add(new Point(polygonPoint.X - 20, polygonPoint.Y - 20));

            rotatePolygonLeft();

            graphics.DrawPolygon(new Pen(Color.Blue, 3), polygon.ToArray());
        }
    
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics.Clear(Color.White);

            if (rotateLeft && path.Count != 0)
            {
                rotateFigureLeft();
            }

            if(movePolygon)
            {
                movePolygonAlongFigure();
            }

            if (path.Count > 2)
                graphics.DrawPolygon(rotatePen, path.ToArray());

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

        private Point MovePointTowards(Point a, Point b, double distance)
        {
            var vector = new Point(b.X - a.X, b.Y - a.Y);
            var length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            var unitVector = new PointF((float)(vector.X / length), (float)(vector.Y / length));
            return new Point((int)(a.X + unitVector.X * distance), (int)(a.Y + unitVector.Y * distance));
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

        private void MoveFigure_MouseClick(object sender, MouseEventArgs e)
        {
            rotateLeft = !rotateLeft;
        }

        private void MovePolygon_MouseClick(object sender, MouseEventArgs e)
        {
            movePolygon = !movePolygon;
            polygonPoint = path[0];
            polygonNext = path[1];
        }
    }
}
