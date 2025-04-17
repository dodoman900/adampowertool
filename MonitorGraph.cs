using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace AdamPowerTool
{
    public partial class MonitorGraph : UserControl
    {
        private Panel graphPanel = new Panel();
        private TimeSpan selectedTimeRange = TimeSpan.FromMinutes(5);
        private System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
        private readonly GraphRenderer cpuGraph;
        private readonly GraphRenderer ramGraph;
        private readonly GraphRenderer diskGraph;
        private readonly GraphRenderer gpuGraph;
        private readonly GraphRenderer powerGraph;

        public TimeSpan SelectedTimeRange
        {
            get => selectedTimeRange;
            set
            {
                selectedTimeRange = value;
                cpuGraph.Clear();
                ramGraph.Clear();
                diskGraph.Clear();
                gpuGraph.Clear();
                powerGraph.Clear();
                graphPanel.Invalidate();
            }
        }

        public MonitorGraph()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            float initialGraphWidth = 700;
            float cpuGraphHeight = 100;
            float diskGpuHeight = 50;
            cpuGraph = new GraphRenderer(initialGraphWidth, cpuGraphHeight, Color.FromArgb(255, 165, 0)); // Turuncu
            ramGraph = new GraphRenderer(initialGraphWidth, cpuGraphHeight, Color.FromArgb(0, 255, 0)); // Yeşil
            diskGraph = new GraphRenderer(initialGraphWidth / 2 - 10, diskGpuHeight, Color.FromArgb(255, 165, 0)); // Turuncu
            gpuGraph = new GraphRenderer(initialGraphWidth / 2 - 10, diskGpuHeight, Color.FromArgb(0, 255, 0)); // Yeşil
            powerGraph = new GraphRenderer(initialGraphWidth, cpuGraphHeight, Color.FromArgb(255, 0, 0)); // Kırmızı neon

            SetupControls();
            refreshTimer.Interval = 500;
            refreshTimer.Tick += (s, e) => UpdateGraphs();
        }

        private void SetupControls()
        {
            graphPanel.Location = new Point(0, 0);
            graphPanel.Size = new Size(this.Width, this.Height);
            graphPanel.BorderStyle = BorderStyle.FixedSingle;
            graphPanel.BackColor = Color.FromArgb(46, 46, 46); // Düzeltildi: #2E2E2E
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, graphPanel, new object[] { true });
            graphPanel.Paint += GraphPanel_Paint;

            this.Controls.Add(graphPanel);
        }

        private void UpdateGraphs()
        {
            try
            {
                var systemData = BilgisayarBilgileri.GetSystemData(selectedTimeRange) as SystemData;
                if (systemData == null || systemData.cpuData == null || systemData.ramData == null || systemData.diskData == null || systemData.gpuData == null || systemData.powerData == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
                }

                cpuGraph.UpdatePoints(systemData.cpuData, selectedTimeRange, 0);
                ramGraph.UpdatePoints(systemData.ramData, selectedTimeRange, 0);
                diskGraph.UpdatePoints(systemData.diskData, selectedTimeRange, 0);
                gpuGraph.UpdatePoints(systemData.gpuData, selectedTimeRange, 0);
                powerGraph.UpdatePoints(systemData.powerData, selectedTimeRange, 0);

                graphPanel.Invalidate();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.GraphRenderError);
            }
        }

        private void GraphPanel_Paint(object? sender, PaintEventArgs e)
        {
            try
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new LinearGradientBrush(graphPanel.ClientRectangle, Color.FromArgb(27, 27, 27), Color.FromArgb(46, 46, 46), 90F))
                {
                    g.FillRectangle(brush, graphPanel.ClientRectangle);
                }

                float cpuGraphHeight = (graphPanel.Height - 100) / 4;
                float ramGraphHeight = cpuGraphHeight;
                float diskGpuHeight = cpuGraphHeight / 2;
                float powerGraphHeight = cpuGraphHeight;

                DrawGraphLabel(g, "CPU Kullanımı (%)", 0);
                cpuGraph.Draw(g, 0);

                DrawGraphLabel(g, "RAM Kullanımı (%)", cpuGraphHeight + 20);
                ramGraph.Draw(g, cpuGraphHeight + 20);

                DrawGraphLabel(g, "Disk Aktivitesi (ölçekli)", (cpuGraphHeight + 20) * 2);
                diskGraph.Draw(g, (cpuGraphHeight + 20) * 2);

                DrawGraphLabel(g, "GPU Kullanımı (%)", (cpuGraphHeight + 20) * 2 + diskGpuHeight + 10);
                gpuGraph.Draw(g, (cpuGraphHeight + 20) * 2 + diskGpuHeight + 10);

                DrawGraphLabel(g, "Güç Kullanımı (Watt)", (cpuGraphHeight + 20) * 3 + diskGpuHeight + 10);
                powerGraph.Draw(g, (cpuGraphHeight + 20) * 3 + diskGpuHeight + 10);

                DrawWattValues(g, (cpuGraphHeight + 20) * 3 + diskGpuHeight + 10 + powerGraphHeight + 10);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.GraphRenderError);
            }
        }

        private void DrawGraphLabel(Graphics g, string label, float yOffset)
        {
            using (var labelFont = new Font("Montserrat", 12, FontStyle.Bold))
            using (var labelBrush = new SolidBrush(Color.FromArgb(255, 0, 0))) // Kırmızı neon
            {
                g.DrawString(label, labelFont, labelBrush, 20, yOffset - 20);
            }
        }

        private void DrawWattValues(Graphics g, float yOffset)
        {
            var systemData = BilgisayarBilgileri.GetSystemData(selectedTimeRange) as SystemData;
            if (systemData == null || systemData.powerData == null) return;

            double anlikWatt = systemData.powerData.Count > 0 ? systemData.powerData[^1].value : 0.0;
            double saatlikOrtalama = systemData.powerData.Count > 0 ? systemData.powerData.Average(d => d.value) : 0.0;
            double tasarrufEdilen = 10.0; // Tahmini bir değer

            using (var font = new Font("Montserrat", 10, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(255, 0, 0))) // Kırmızı neon
            {
                g.DrawString($"Anlık: {anlikWatt:F1}W", font, brush, 20, yOffset);
                g.DrawString($"Saatlik Ortalama: {saatlikOrtalama:F1}W", font, brush, 20, yOffset + 20);
                g.DrawString($"Tasarruf Edilen: {tasarrufEdilen:F1}W", font, brush, 20, yOffset + 40);
            }
        }

        public void StartUpdating()
        {
            refreshTimer.Start();
        }

        public void StopUpdating()
        {
            refreshTimer.Stop();
        }

        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(740, 500);
        }
    }

    internal class GraphRenderer
    {
        private readonly float width;
        private readonly float height;
        private readonly Color graphColor;
        private readonly List<PointF> points = new List<PointF>();

        public GraphRenderer(float width, float height, Color graphColor)
        {
            this.width = width;
            this.height = height;
            this.graphColor = graphColor;
        }

        public void UpdatePoints(List<(DateTime time, double value)> data, TimeSpan timeRange, float yOffset)
        {
            points.Clear();
            if (data == null || data.Count == 0) return;

            DateTime endTime = data[^1].time;
            DateTime startTime = endTime - timeRange;

            float xStep = width / Math.Max(1, data.Count - 1);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].time >= startTime)
                {
                    float x = (i * xStep);
                    float y = (float)(height - (data[i].value / 100.0 * height)) + yOffset;
                    points.Add(new PointF(x, y));
                }
            }
        }

        public void Draw(Graphics g, float yOffset)
        {
            if (points.Count < 2) return;

            using (var pen = new Pen(graphColor, 2))
            {
                g.DrawLines(pen, points.ToArray());

                for (int i = 0; i <= 100; i += 20)
                {
                    float y = height - (i / 100.0f * height) + yOffset;
                    using (var gridPen = new Pen(Color.FromArgb(50, 255, 255, 255), 1))
                    {
                        g.DrawLine(gridPen, 0, y, width, y);
                    }
                    using (var labelFont = new Font("Montserrat", 8, FontStyle.Bold))
                    using (var labelBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                    {
                        g.DrawString(i.ToString(), labelFont, labelBrush, width + 5, y - 5);
                    }
                }
            }
        }

        public void Clear()
        {
            points.Clear();
        }
    }
}