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
        Timer RefreshTimer = new Timer();
        List<PointF> Path = new List<PointF>();
        Graphics Gr;
        int ind = -1;
        const int SizeofPoint = 9;
        bool IsLocalasingPoint = false;
        bool IsRotatingPath = false;
        bool IsRotatingSquare = false;
        bool IsMovingSquare = false;
        bool ShowSquare = false;
        float RotationAngleOfSquare = 10;
        float RotationAngleOfPath = 10;
        List<PointF> Square = new List<PointF>();
        List<PointF> LocalisationPoints = new List<PointF>();
        float SideOfSquare = 40;
        float t = 0;
        int i = 0;
        float MovingSpeed = 10;


        public Form1()
        {
            InitializeComponent();
        }

        private void SetSquare(float CoordX, float CoordY)
        {
            //if (Square.Count != 0) return;
            Square.Add(new PointF(CoordX - SideOfSquare / 2, CoordY - SideOfSquare / 2));
            Square.Add(new PointF(CoordX - SideOfSquare / 2, CoordY + SideOfSquare / 2));
            Square.Add(new PointF(CoordX + SideOfSquare / 2, CoordY + SideOfSquare / 2));
            Square.Add(new PointF(CoordX + SideOfSquare / 2, CoordY - SideOfSquare / 2));
        }

        private void DrawSquare()
        {
            Gr.DrawPolygon(new Pen(Color.DarkMagenta, 4), Square.ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Gr = CreateGraphics();

            RefreshTimer.Interval = (100);
            RefreshTimer.Tick += new EventHandler(RefreshTick);
            RefreshTimer.Start();
        }

        private void RefreshTick(object sender, EventArgs e)
        {
            Refresh();
            if (IsRotatingPath)
            {
                Rotate(Path, RotationAngleOfPath);
                MoveSquare(Square);
            }
            if(IsMovingSquare) MoveSquare(Square);
            if(IsRotatingSquare) Rotate(Square, RotationAngleOfSquare);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsLocalasingPoint == false)
            {
                IsLocalasingPoint = true;
                Path.Clear();
                ShowSquare = false;
                button4.Text = "Show Square";
                Square.Clear();
                button1.Text = "Localisation";
                return;
            }

            if (IsLocalasingPoint == true)
            {
                IsLocalasingPoint = false;
                Path.Clear();
                ShowSquare = false;
                button4.Text = "Show Square";
                Square.Clear();
                button1.Text = "Moving";
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Path.Clear();
            LocalisationPoints.Clear();
            ShowSquare = false;
            Square.Clear();
        }
        private bool Localising(PointF Q)
        {
            if (Path.Count < 3) return false;
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Gr.Clear(Color.White);
            if (ShowSquare == true) DrawSquare();
            if(Path.Count > 2)
                Gr.DrawPolygon(new Pen(Color.DarkCyan, 3), Path.ToArray());
            Path.ForEach(point => Gr.FillRectangle(Brushes.LightSkyBlue, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
            Path.ForEach(point => Gr.DrawRectangle(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
            
            if(IsLocalasingPoint)
            {
                for (int i = 0; i < LocalisationPoints.Count; i++)
                {
                    PointF point = LocalisationPoints[i];
                    if (Localising(LocalisationPoints[i]))
                    {
                        Gr.FillRectangle(Brushes.Green, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint);
                        Gr.DrawRectangle(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint);
                    }
                    else
                    {
                        Gr.FillRectangle(Brushes.Red, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint);
                        Gr.DrawRectangle(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint);
                    }
                }
            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsRotatingPath == true) return;
            if (e.Button == MouseButtons.Left)
            {
                if (Path.Count() < 3 || IsLocalasingPoint == false)
                {
                    Path.Add(e.Location);
                } 
                else
                {
                    LocalisationPoints.Add(e.Location);
                }

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



        private void Rotate(List<PointF> PointsToRotate, Double AngleOfRotation)
        {
            PointF Center = FindCenter(PointsToRotate);

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
                return;
            }

            if(IsRotatingPath == true)
            {
                IsRotatingPath = false;
                button3.Text = "Continue Rotating";
                return;
            }
        }

        private void MoveSquare(List<PointF> PointsToMove)
        {
            if (Path.Count() == 0) return;
            PointF Vector = new PointF(Path[(i + 1) % Path.Count()].X - Path[i].X, Path[(i + 1) % Path.Count()].Y - Path[i].Y);
            float Distance = (float)Math.Sqrt(Vector.X * Vector.X + Vector.Y * Vector.Y);
            PointF newP = new PointF(Path[i].X * (1 - t) + Path[(i + 1) % Path.Count()].X * t, Path[i].Y * (1 - t) + Path[(i + 1) % Path.Count()].Y * t);
            
            if(IsMovingSquare)
                t += 1 * MovingSpeed / Distance;

            if (t >= 1)
            {
                i++;
                if (i >= Path.Count()) i = 0;
                t = 0;
            }

            PointF Center = FindCenter(PointsToMove);

            for (int i = 0; i < Square.Count; i++)
            {
                Square[i] = new PointF((Square[i].X - Center.X) + newP.X, (Square[i].Y - Center.Y) + newP.Y);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ShowSquare == false)
            {
                ShowSquare = true;
                button4.Text = "Show Square";
                if(Square.Count() == 0)
                if (Path.Count > 0) SetSquare(Path[0].X, Path[0].Y);//&&&&&&
                else SetSquare(800, 300);
                return;
            }

            if (ShowSquare == true)
            {
                ShowSquare = false;
                button4.Text = "Hide Square";
                Square.Clear();
                return;
            }
        }

        private PointF FindCenter(List<PointF> Polygon)
        {
            float CenterX = 0;
            float CenterY = 0;
            Polygon.ForEach(point => { CenterX += point.X; CenterY += point.Y; });
            PointF Center = new PointF(CenterX / Polygon.Count, CenterY / Polygon.Count);
            return Center;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (IsMovingSquare == false)
            {
                IsMovingSquare = true;
                button5.Text = "Stop Movement";
                return;
            }

            if (IsMovingSquare == true)
            {
                IsMovingSquare = false;
                button5.Text = "Continue Movement";
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IsRotatingSquare == false)
            {
                IsRotatingSquare = true;
                button6.Text = "Stop Rotating";
                return;
            }

            if (IsRotatingSquare == true)
            {
                IsRotatingSquare = false;
                button6.Text = "Continue Rotating";
                return;
            }
        }
    }
}
