using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graf
{
    public partial class Form1 : Form
    {
        Timer RotateTimer = new Timer();
        Timer RefreshTimer = new Timer();
        List<PointF> Path = new List<PointF>();
        Graphics Gr;
        int ind = -1;
        const int SizeofPoint = 9;
        bool IsLocalasingPoint = false;
        bool IsRotatingPath = false;
        float RotationAngle = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Gr = CreateGraphics();

            RefreshTimer.Interval = (50);
            RefreshTimer.Tick += new EventHandler(RefreshTick);
            RefreshTimer.Start();

            RotateTimer.Interval = (50);//?????
            RotateTimer.Tick += new EventHandler(RotationTick);
        }

        private void RefreshTick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void RotationTick(object sender, EventArgs e)
        {
            Rotate(Path, 5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsLocalasingPoint == false)
            {
                IsLocalasingPoint = true;
                Path.Clear();
                button1.Text = "Localisation";
                return;
            }

            if (IsLocalasingPoint == true)
            {
                IsLocalasingPoint = false;
                Path.Clear();
                button1.Text = "Moving";
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Path.Clear();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Gr.Clear(Color.White);

            if(Path.Count > 2)
                Gr.DrawPolygon(new Pen(Color.DarkCyan, 3), Path.ToArray());
            Path.ForEach(point => Gr.FillRectangle(Brushes.LightSkyBlue, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
            Path.ForEach(point => Gr.DrawRectangle(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsRotatingPath == true) return;
            if (e.Button == MouseButtons.Left)
                if(Path.Count() < 3 || IsLocalasingPoint == false)
                {
                    Path.Add(e.Location);
                }
            

            if (e.Button == MouseButtons.Right)
            {
                ind = -1;
                for (int i = 0; i < Path.Count; i++)
                {
                    if ((e.X > Path[i].X - SizeofPoint - 2) & (e.X < Path[i].X + SizeofPoint + 2) & (e.Y > Path[i].Y - SizeofPoint + 2) & (e.Y < Path[i].Y + SizeofPoint + 2))
                    {
                        ind = i;
                    }
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
             ind = -1;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ind != -1)
            {
                Path[ind] = new PointF(e.X, e.Y);
            }
        }

        private bool Localising(PointF Q)
        {
            PointF A = Path[0];
            PointF B = Path[1];
            PointF C = Path[2];

            bool Determinant = (A.X * B.Y + B.X * C.Y + C.X * A.Y - C.X * B.Y - B.X * A.Y - A.X * C.Y > 0);
            bool MinorA = (Q.X * B.Y + B.X * C.Y + C.X * Q.Y - C.X * B.Y - B.X * Q.Y - Q.X * C.Y > 0);
            if (Determinant != MinorA) return false;
            bool MinorB = (A.X * Q.Y + Q.X * C.Y + C.X * A.Y - C.X * Q.Y - Q.X * A.Y - A.X * C.Y > 0);
            if (Determinant != MinorB) return false;
            bool MinorC = (A.X * B.Y + B.X * Q.Y + Q.X * A.Y - Q.X * B.Y - B.X * A.Y - A.X * Q.Y > 0);
            if (Determinant != MinorC) return false;

            return true;
        }

        private void Rotate(List<PointF> PointsToRotate, Double AngleOfRotation)
        {
            float CenterX = 0;
            float CenterY = 0;

            PointsToRotate.ForEach(point => { CenterX += point.X; CenterY += point.Y; });
            PointF Center = new PointF(CenterX/PointsToRotate.Count, CenterY / PointsToRotate.Count);

            for (int i = 0; i < PointsToRotate.Count; i++)
            {
                PointF point = PointsToRotate[i];
                double CoordinateX = (point.X - Center.X) * Math.Cos(AngleOfRotation * Math.PI / 180) -
                (point.Y - Center.Y) * Math.Sin(AngleOfRotation * Math.PI / 180) + Center.X;
                double CoordinateY = (point.X - Center.X) * Math.Sin(AngleOfRotation * Math.PI / 180) +
                (point.Y - Center.Y) * Math.Cos(AngleOfRotation * Math.PI / 180) + Center.Y;
                PointsToRotate[i] = new PointF((float)CoordinateX, (float)CoordinateY);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsRotatingPath == false)
            {
                IsRotatingPath = true;
                button3.Text = "Stop Rotating";
                RotateTimer.Start();
                return;
            }

            if (IsRotatingPath == true)
            {
                IsRotatingPath = false;
                button3.Text = "Continue Rotating";
                RotateTimer.Stop();
                return;
            }
        }

    }
}
