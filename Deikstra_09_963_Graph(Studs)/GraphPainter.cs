using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deikstra_09_963;
using System.Diagnostics;

namespace Deikstra_09_963_Graph_Studs_
{
    class GraphPainter
    {
        private Graph gr;
        private readonly int vLineWidth = 5;
        private int vertexSize = 30;

        public GraphPainter(Graph graph)
        {
            gr = graph;
        }

        public void Paint(Graphics g)
        {
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawEdges(g);
            DrawVerticies(g);
        }

        private void DrawVerticies(Graphics g)
        {
            var sr = GetSquareRect(g);
            var radius = sr.Width / 2;
            var center = new PointF(sr.X + radius, sr.Y + radius);
            g.TranslateTransform(center.X, center.Y);
            var vc = gr.VertexCount;
            var vfColor = Color.BlueViolet;
            var vbColor = Color.White;

            for (int i = 1; i <= vc; i++)
            {
                var vp = new Pen(vfColor, vLineWidth);
                var vb = new SolidBrush(vbColor);
                var fnt = new Font("Calibri", 14);
                var brush = new SolidBrush(vfColor);
                var sm = g.MeasureString(i.ToString(), fnt);
                var pt = new PointF(-sm.Width / 2, -radius + (vertexSize - sm.Height)/2);
                g.FillEllipse(vb, -vertexSize/2, -radius, vertexSize, vertexSize);
                g.DrawEllipse(vp, -vertexSize/2, -radius, vertexSize, vertexSize);
                g.DrawString(i.ToString(), fnt, brush, pt);
                g.RotateTransform(360F / vc);
            }
            g.ResetTransform();
        }

        private RectangleF GetSquareRect(Graphics g)
        {
            var r = g.VisibleClipBounds;
            var minSz = Math.Min(r.Width, r.Height) - 2 * vLineWidth;
            var dx = (r.Width - minSz) / 2;
            var dy = (r.Height - minSz) / 2;
            return new RectangleF(dx, dy, minSz, minSz);
        }

        private void DrawEdges(Graphics g)
        {
            RectangleF r = GetSquareRect(g);
            var radius = (r.Width - vertexSize) / 2F;
            var center = new PointF(r.X + r.Width / 2F, r.Y = r.Height / 2F);
            var angle = 2F * Math.PI / gr.VertexCount;
            for (int i = 0; i < gr.Matrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < gr.Matrix.GetLength(1); j++)
                {
                    if (gr.Matrix[i, j] > Double.Epsilon)
                    {
                        g.TranslateTransform(center.X, center.Y);
                        g.RotateTransform(-90);
                        var x1 = (float) (radius * Math.Cos(i * angle));
                        var y1 = (float) (radius * Math.Sin(i * angle));
                        var x2 = (float) (radius * Math.Cos(j * angle));
                        var y2 = (float) (radius * Math.Sin(j * angle));

                        var ec = Color.DarkGreen;
                        var b = new SolidBrush(ec);
                        var p = new Pen(ec, 2);
                        var fnt = new Font("Calibri", 12, FontStyle.Bold | FontStyle.Underline);
                        var weight = gr.Matrix[i, j].ToString();
                        var sm = g.MeasureString(weight, fnt);
                        float ang = (float)(90 - Math.Atan((y2 - y1) / (x2 - x1)) * 180 / Math.PI);
                        if (Math.Abs(ang) < 90) ang += 180F;
                        g.DrawLine(p, x1, y1, x2, y2);
                        g.TranslateTransform((x2 + x1) / 2, (y2 + y1) / 2);
                        g.RotateTransform(-90 - ang);
                        g.DrawString(
                            weight,
                            fnt,
                            b, -sm.Width / 2, 0);
                        g.ResetTransform();
                    }

                    
                }
            }
        }

        public void ShowPath(List<Vertex> vertices, Graphics g)
        {
            RectangleF r = GetSquareRect(g);
            var radius = (r.Width - vertexSize) / 2F;
            var center = new PointF(r.X + r.Width / 2F, r.Y = r.Height / 2F);
            var angle = 2F * Math.PI / gr.VertexCount;


            for (int i = 1; i < vertices.Count ; i++)
            {
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(-90);
                var x1 = (float)(radius * Math.Cos((vertices[i].Previous.Num - 1) * angle));
                var y1 = (float)(radius * Math.Sin((vertices[i].Previous.Num - 1) * angle));
                var x2 = (float)(radius * Math.Cos((vertices[i].Num - 1) * angle));
                var y2 = (float)(radius * Math.Sin((vertices[i].Num - 1) * angle));

                var ec = Color.Red;
                var p = new Pen(ec, 4);
                float ang = (float)(90 - Math.Atan((y2 - y1) / (x2 - x1)) * 180 / Math.PI);
                if (Math.Abs(ang) < 90) ang += 180F;
                g.DrawLine(p, x1, y1, x2, y2);
                g.TranslateTransform((x2 + x1) / 2, (y2 + y1) / 2);
                g.RotateTransform(-90 - ang);
                g.ResetTransform();
            }

            DrawVerticies(g);
        }
    }
}
