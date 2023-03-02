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
        List<Point> Path = new List<Point>();
        Graphics Gr;
        int ind = -1;
        const int SizeofPoint = 9;
        bool IsLocalasingPoint = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Gr = CreateGraphics();

            Timer MyTimer = new Timer();
            MyTimer.Interval = (100); 
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsLocalasingPoint == false)
            {
                IsLocalasingPoint = true;
                Path.Clear();
            }

            if (IsLocalasingPoint == true)
            {
                IsLocalasingPoint = false;
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
            if (e.Button == MouseButtons.Left)
                Path.Add(e.Location);

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
                Path[ind] = new Point(e.X, e.Y);
            }
        }
    }
}
