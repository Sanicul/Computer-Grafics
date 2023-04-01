using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierSurname
{
    public partial class Form1 : Form
    {

        Graphics Gr;
        Timer RefreshTimer = new Timer();

        List<PointF> Path = new List<PointF>();
        List<PointF> DrawnBezier = new List<PointF>();

        const int SizeofPoint = 8;
        const int SizeofBezierPoint = 10;
        const int RefreshTime = 100;

        bool IsShowingStruct = true;
        bool IsShowingBezierHelp = true;

        int LastAddedPoint = -1;
        int MovingPointIndex = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Gr = CreateGraphics();

            RefreshTimer.Interval = (RefreshTime);
            RefreshTimer.Tick += new EventHandler(RefreshTick);
            RefreshTimer.Start();
        }

        private void RefreshTick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Path.Add(e.Location);
                if (Path.Count() == 3)
                {
                    DrawnBezier.Add(Path[0]);
                    DrawnBezier.Add(new PointF((float)1 / 3 * Path[0].X + (float)2 / 3 * Path[1].X, (float)1 / 3 * Path[0].Y + (float)2 / 3 * Path[1].Y));
                    DrawnBezier.Add(new PointF((float)1 / 3 * Path[2].X + (float)2 / 3 * Path[1].X, (float)1 / 3 * Path[2].Y + (float)2 / 3 * Path[1].Y));
                    DrawnBezier.Add(Path[2]);
                    LastAddedPoint = 2;
                }
    
                if ((Path.Count() - 1) % 2 == 0 && Path.Count() > 3)
                {
                    for (int i = LastAddedPoint + 1; i < Path.Count(); i++)
                        if (i % 2 == 1)
                            DrawnBezier.Add(new PointF((float)1 / 3 * Path[i - 1].X + ((float)2 / 3) * Path[i].X, ((float)1 / 3) * Path[i - 1].Y + ((float)2 / 3) * Path[i].Y));//?????
                        else
                        {
                            DrawnBezier.Add(new PointF((float)2 / 3 * Path[i - 1].X + (float)1 / 3 * Path[i].X, ((float)2 / 3) * Path[i - 1].Y + ((float)1 / 3) * Path[i].Y));//?????
                            DrawnBezier.Add(Path[i]);
                        }
                    LastAddedPoint = Path.Count() - 1;
                }
                
                /*if (Path.Count() == 4)
                    for (int i = 0; i < 4; i++)
                        DrawnBezier.Add(Path[i]);

                if (DrawnBezier.Count < Path.Count() && (Path.Count() - 1) % 3 == 0 && Path.Count() > 4)
                {
                    for (int i = DrawnBezier.Count(); i < Path.Count(); i++)
                        DrawnBezier.Add(Path[i]);
                }*/
            }

            if (e.Button == MouseButtons.Right && IsShowingStruct)
            {
                MovingPointIndex = -1;
                for (int i = 0; i < Path.Count; i++)
                {
                    if ((e.X > Path[i].X - SizeofPoint - 2) & (e.X < Path[i].X + SizeofPoint + 2) & (e.Y > Path[i].Y - SizeofPoint + 2) & (e.Y < Path[i].Y + SizeofPoint + 2))
                    {
                        MovingPointIndex = i;
                    }
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MovingPointIndex = -1;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && MovingPointIndex != -1)
            {
                Path[MovingPointIndex] = new PointF(e.X, e.Y);

                if(MovingPointIndex % 2 == 0)
                {
                    if (MovingPointIndex == 0)
                    {
                        DrawnBezier[3 * MovingPointIndex / 2] = new PointF(e.X, e.Y);
                        DrawnBezier[3 * MovingPointIndex / 2 + 1] = new PointF((float)1 / 3 * Path[MovingPointIndex].X + (float)2 / 3 * Path[MovingPointIndex + 1].X, ((float)1 / 3) * Path[MovingPointIndex].Y + ((float)2 / 3) * Path[MovingPointIndex + 1].Y);
                    }
                    else 
                    if (3 * MovingPointIndex / 2 == DrawnBezier.Count - 1)
                    {
                        DrawnBezier[3 * MovingPointIndex / 2] = new PointF(e.X, e.Y);
                        DrawnBezier[3 * MovingPointIndex / 2 - 1] = new PointF((float)2 / 3 * Path[MovingPointIndex - 1].X + (float)1 / 3 * Path[MovingPointIndex].X, ((float)2 / 3) * Path[MovingPointIndex - 1].Y + ((float)1 / 3) * Path[MovingPointIndex].Y);
                    }
                    else
                    {
                        DrawnBezier[3 * MovingPointIndex / 2] = new PointF(e.X, e.Y);
                        DrawnBezier[3 * MovingPointIndex / 2 - 1] = new PointF((float)2 / 3 * Path[MovingPointIndex - 1].X + (float)1 / 3 * Path[MovingPointIndex].X, ((float)2 / 3) * Path[MovingPointIndex - 1].Y + ((float)1 / 3) * Path[MovingPointIndex].Y);
                        DrawnBezier[3 * MovingPointIndex / 2 + 1] = new PointF((float)1 / 3 * Path[MovingPointIndex].X + (float)2 / 3 * Path[MovingPointIndex + 1].X, ((float)1 / 3) * Path[MovingPointIndex].Y + ((float)2 / 3) * Path[MovingPointIndex + 1].Y);
                    }
                }
                else
                {
                    if (MovingPointIndex != Path.Count() - 1)
                    {
                        DrawnBezier[(3 * MovingPointIndex - 1) / 2] = new PointF((float)1 / 3 * Path[MovingPointIndex - 1].X + (float)2 / 3 * Path[MovingPointIndex].X, ((float)1 / 3) * Path[MovingPointIndex - 1].Y + ((float)2 / 3) * Path[MovingPointIndex].Y);
                        DrawnBezier[(3 * MovingPointIndex + 1) / 2] = new PointF((float)2 / 3 * Path[MovingPointIndex].X + (float)1 / 3 * Path[MovingPointIndex + 1].X, ((float)2 / 3) * Path[MovingPointIndex].Y + ((float)1 / 3) * Path[MovingPointIndex + 1].Y);
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (DrawnBezier.Count() > 3)
            {
                 e.Graphics.DrawBeziers(new Pen(Color.DarkSlateBlue, 6), DrawnBezier.ToArray());

            } 
            if (IsShowingStruct) 
                for(int i = 0; i < Path.Count() - 1; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Green, 1), Path[i], Path[i + 1]);
                }

            if (IsShowingStruct)
            {
                if (IsShowingBezierHelp)
                {
                    DrawnBezier.ForEach(point => e.Graphics.FillRectangle(Brushes.Blue, point.X - SizeofBezierPoint / 2, point.Y - SizeofBezierPoint / 2, SizeofBezierPoint, SizeofBezierPoint));
                    DrawnBezier.ForEach(point => e.Graphics.DrawRectangle(new Pen(Color.Black, 1), point.X - SizeofBezierPoint / 2, point.Y - SizeofBezierPoint / 2, SizeofBezierPoint, SizeofBezierPoint));
                }
                Path.ForEach(point => e.Graphics.FillEllipse(Brushes.Green, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
                Path.ForEach(point => e.Graphics.DrawEllipse(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
            }
        }

        private void Struct_Click(object sender, EventArgs e)
        {
            if (IsShowingStruct == true)
            {
                IsShowingStruct = false;
                Struct.Text = "Show Structure";
                return;
            }

            if (IsShowingStruct == false)
            {
                IsShowingStruct = true;
                Struct.Text = "Hide Structure";
                return;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Path.Clear();
            DrawnBezier.Clear();
            IsShowingStruct = true;
            Struct.Text = "Hide Structure";
        }

        private void BezierHelp_Click(object sender, EventArgs e)
        {
            if (IsShowingBezierHelp == true)
            {
                IsShowingBezierHelp = false;
                BezierHelp.Text = "Show Bezier Help";
                return;
            }

            if (IsShowingBezierHelp == false)
            {
                IsShowingBezierHelp = true;
                BezierHelp.Text = "Hide Bezier Help";
                return;
            }
        }
    }
}
