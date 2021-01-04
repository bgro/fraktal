﻿using System;
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
        public Form1()
        {

            InitializeComponent();

            Graphics dc = this.CreateGraphics();
            this.Show();
            int draw_black = 0;
            double leftFrame = -1.5;
            double rightFrame = 0.5;
            double topFrame = 0.75;
            double bottomFrame = -0.75;


            Pen BlackPen = new Pen(Color.Black, 1);
            Pen Pen = new Pen(Color.Red, 1);

            string fileName = @"C:\Users\bernd\log.txt";
            FileInfo filetoappend = new FileInfo(fileName);
            using (StreamWriter sw = filetoappend.AppendText())
            {
                for (int x = 0; x < 600; x++)
                {
                    for (int y = 0; y < 400; y++)
                    {
                        var cX = leftFrame + (rightFrame - leftFrame) * (double)x / (double)600;
                        var cY = topFrame - (topFrame - bottomFrame) * (double)y / (double)400;

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
            }

        }

    }
}
