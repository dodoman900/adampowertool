using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace AdamPowerTool
{
    public class GraphRenderer
    {
        private List<PointF> points;
        private List<Color> pointColors; // Güç grafiği için renkler
        private Bitmap bitmap;
        private Color color;
        private float graphWidth;
        private float graphHeight;
        private bool isPowerGraph;

        public GraphRenderer(float width, float height, Color graphColor, bool isPowerGraph = false)
        {
            points = new List<PointF>();
            pointColors = new List<Color>();
            graphWidth = width;
            graphHeight = height;
            color = graphColor;
            this.isPowerGraph = isPowerGraph;
            bitmap = new Bitmap((int)width, (int)height);
            using (var g = Graphics.FromImage(bitmap)) g.Clear(Color.Transparent);
        }

        public void UpdatePoints(List<(DateTime time, double value)> data, TimeSpan timeRange, float yOffset, bool isPowerGraph = false)
        {
            if (data.Count < 2) return;

            points.Clear();
            pointColors.Clear();

            DateTime startTime = DateTime.Now - timeRange;
            float xScale = (graphWidth - 40) / (float)timeRange.TotalSeconds;
            float yScale = isPowerGraph ? (graphHeight - 10) / 400f : (graphHeight - 10) / 100f; // Güç için 400 Watt, diğerleri için 100%

            foreach (var d in data)
            {
                if (d.time < startTime) continue;

                float x = 20 + (float)(d.time - startTime).TotalSeconds * xScale;
                float y = yOffset + graphHeight - (float)(Math.Max(0, Math.Min(isPowerGraph ? d.value : Math.Min(d.value, 100), isPowerGraph ? 400 : 100)) * yScale);

                points.Add(new PointF(x, y));
                if (isPowerGraph)
                {
                    pointColors.Add(GetPowerColor(d.value));
                }
            }

            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (points.Count > 1)
                {
                    for (int i = 1; i < points.Count; i++)
                    {
                        var lastPoint = points[i - 1];
                        var newPoint = points[i];

                        using var linePen = isPowerGraph ? new Pen(pointColors[i], 3) : new Pen(color, 3); // Çizgi kalınlığı artırıldı
                        if (Math.Abs(newPoint.X - lastPoint.X) > 10)
                        {
                            using var dashPen = new Pen(isPowerGraph ? pointColors[i] : color, 3) { DashPattern = new float[] { 4, 4 } };
                            g.DrawLine(dashPen, lastPoint.X, lastPoint.Y, newPoint.X, newPoint.Y);
                        }
                        else
                        {
                            g.DrawLine(linePen, lastPoint.X, lastPoint.Y, newPoint.X, newPoint.Y);
                        }
                    }
                }

                points.RemoveAll(p => p.X < 20 || p.X > graphWidth);
            }
        }

        private Color GetPowerColor(double value)
        {
            if (value <= 100) return Color.Green;
            if (value <= 200) return Color.Yellow;
            if (value <= 300) return Color.Orange;
            return Color.Red;
        }

        public void Draw(Graphics g, float yOffset)
        {
            try
            {
                RectangleF graphRect = new RectangleF(15, yOffset - 5, graphWidth + 10, graphHeight + 10);
                using (var shadowPath = new GraphicsPath())
                {
                    shadowPath.AddRectangle(graphRect);
                    using (var shadowBrush = new PathGradientBrush(shadowPath))
                    {
                        shadowBrush.CenterColor = Color.FromArgb(80, 0, 0, 0);
                        shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }

                using (var bgBrush = new SolidBrush(Color.FromArgb(30, 40, 60)))
                {
                    g.FillRectangle(bgBrush, 20, yOffset, graphWidth, graphHeight);
                }

                using var gridPen = new Pen(Color.FromArgb(100, 100, 120), 1);
                for (int i = 0; i <= 5; i++)
                {
                    float y = yOffset + graphHeight - (i * (graphHeight / 5));
                    g.DrawLine(gridPen, 20, y, 20 + graphWidth, y);
                    g.DrawString(isPowerGraph ? (i * 80).ToString() : (i * 20).ToString(), new Font("Segoe UI", 8, FontStyle.Bold), Brushes.White, 0, y - 8);
                }

                g.DrawImage(bitmap, 20, yOffset);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.GraphRenderError);
            }
        }

        public void Clear()
        {
            points.Clear();
            pointColors.Clear();
            using (var g = Graphics.FromImage(bitmap)) g.Clear(Color.Transparent);
        }
    }
}