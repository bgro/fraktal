using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Numerics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fraktal
{
    public partial class Form1 : Form
    {
        int resolution = 400;
        public Form1()
        {

            InitializeComponent();

            Graphics dc = this.CreateGraphics();
            this.Show();
            int draw_black = 0;
            double leftFrame = -0.8495; // -1.5;
            double rightFrame = -0.85; // 0.5;
            double topFrame = 0.2005; // 0.75;
            double bottomFrame = 0.20; // -0.75;


            Pen BlackPen = new Pen(Color.Black, 1);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen Pen = new Pen(Color.Red, 1);

            string fileName = @"C:\Users\bernd\log.txt";
            FileInfo filetoappend = new FileInfo(fileName);
            using (StreamWriter sw = filetoappend.AppendText())
            {
                // Draw Mandelbrot set
                for (int x = 0; x < resolution; x++)
                {
                    for (int y = 0; y < resolution; y++)
                    {
                        var cX = leftFrame + (rightFrame - leftFrame) * (double)x / (double)resolution;
                        var cY = topFrame - (topFrame - bottomFrame) * (double)y / (double)resolution;

                        var c = new Complex(cX, cY);
                        var z = new Complex(0.0, 0.0);

                        draw_black = 1;
                        int n = 0;
                        while (n < 1000 && (draw_black ==1))
                        {
                            z = z * z + c;
                            if (z.Magnitude > 4.0)
                            {
                                draw_black = 0;
                                break;
                            }
                            n++;
                        }

                        if (draw_black == 1)
                            dc.DrawRectangle(BlackPen, x, y, 1, 1);
                        else
                        {
                            Pen.Color = Color.FromArgb(128- (12 *(n % 10)) , 255- (25*(n % 10)), 255);
                            dc.DrawRectangle(Pen, x, y, 1, 1);
                        }
                        sw.WriteLine($"n: {n} x: {x} y: {y} cX: {cX}, cY: {cY}, |z|: {z.Magnitude} draw_black: {draw_black}");
                    }
                }
                // Draw ticks at frame's edge
                for (int k = 1; k < 10; k++) {
                    int tick = (int)(resolution / 10.0) * k;
                    dc.DrawLine(RedPen, tick, 0, tick, 10);
                    dc.DrawLine(RedPen, tick, resolution, tick, resolution-10);
                    dc.DrawLine(RedPen, 0, tick, 10, tick);
                    dc.DrawLine(RedPen, resolution, tick, resolution - 10, tick);

                }

            }

        }

    }
}
