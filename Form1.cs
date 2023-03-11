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
        const int RefreshTime = 100;

        bool IsShowingStruct = true;

        int IndexOfStartingPoint = 0;
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
                if (Path.Count() == 4)
                    for (int i = 0; i < 4; i++)
                        DrawnBezier.Add(Path[i]);

                if (DrawnBezier.Count < Path.Count() && (Path.Count() - 1) % 3 == 0 && Path.Count() > 4)
                {
                    for (int i = DrawnBezier.Count(); i < Path.Count(); i++)
                        DrawnBezier.Add(Path[i]);
                }
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
                DrawnBezier[MovingPointIndex] = new PointF(e.X, e.Y);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Gr.Clear(Color.White);
            if (DrawnBezier.Count() > 3)
            {
                 Gr.DrawBeziers(new Pen(Color.DarkSlateBlue, 4), DrawnBezier.ToArray());

            } 
            if (IsShowingStruct) 
                for(int i = 0; i < Path.Count() - 1; i++)
                {
                    Gr.DrawLine(new Pen(Color.Red, 1), Path[i], Path[i + 1]);
                }

            if (IsShowingStruct)
            {
                Path.ForEach(point => Gr.FillEllipse(Brushes.Red, point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
                Path.ForEach(point => Gr.DrawEllipse(new Pen(Color.Black, 1), point.X - SizeofPoint / 2, point.Y - SizeofPoint / 2, SizeofPoint, SizeofPoint));
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
    }
}
