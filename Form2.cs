using System;
using System.Windows.Forms;
using System.Drawing;

namespace AdamPowerTool
{
    public partial class Form2 : Form
    {
        private System.Windows.Forms.Timer monitorTimer;
        private Label lblCpu;
        private Label lblRam;
        private Label lblDisk;
        private Panel cpuBar;
        private Panel ramBar;
        private Panel diskBar;

        public Form2()
        {
            InitializeComponent();
            SetupMonitorTimer();
        }

        private void SetupMonitorTimer()
        {
            lblCpu = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            cpuBar = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(0, 20),
                BackColor = Color.Lime
            };

            lblRam = new Label
            {
                Location = new Point(20, 80),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            ramBar = new Panel
            {
                Location = new Point(20, 110),
                Size = new Size(0, 20),
                BackColor = Color.Lime
            };

            lblDisk = new Label
            {
                Location = new Point(20, 140),
                Size = new Size(200, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            diskBar = new Panel
            {
                Location = new Point(20, 170),
                Size = new Size(0, 20),
                BackColor = Color.Lime
            };

            this.Controls.AddRange(new Control[] { lblCpu, cpuBar, lblRam, ramBar, lblDisk, diskBar });

            monitorTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 saniye
            };
            monitorTimer.Tick += (s, e) =>
            {
                string monitorInfo = BilgisayarBilgileri.GetSystemMonitor();
                UpdateMonitor(monitorInfo);
            };
            monitorTimer.Start();
        }

        private void UpdateMonitor(string monitorInfo)
        {
            string[] lines = monitorInfo.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.StartsWith("CPU Kullanımı"))
                {
                    string cpuUsageStr = line.Split(':')[1].Trim().Replace("%", "");
                    if (double.TryParse(cpuUsageStr, out double cpuUsage))
                    {
                        lblCpu.Text = line;
                        cpuBar.Size = new Size((int)(cpuUsage * 3), 20); // 0-100% için 300 piksele ölçekle
                    }
                }
                else if (line.StartsWith("RAM Kullanımı"))
                {
                    lblRam.Text = line;
                    string[] ramParts = line.Split('/');
                    if (ramParts.Length > 1 && double.TryParse(ramParts[0].Replace("RAM Kullanımı:", "").Replace("MB", "").Trim(), out double usedRam) &&
                        double.TryParse(ramParts[1].Replace("MB", "").Trim(), out double totalRam))
                    {
                        double ramUsagePercent = (usedRam / totalRam) * 100;
                        ramBar.Size = new Size((int)(ramUsagePercent * 3), 20);
                    }
                }
                else if (line.StartsWith("Disk Aktivitesi"))
                {
                    lblDisk.Text = line;
                    string diskActivityStr = line.Split(':')[1].Trim().Replace("bytes/sec", "");
                    if (double.TryParse(diskActivityStr, out double diskActivity))
                    {
                        // Disk aktivitesini 0-1M bytes/sec aralığında ölçekleyelim (örnek)
                        double diskUsagePercent = Math.Min(diskActivity / 1000000 * 100, 100);
                        diskBar.Size = new Size((int)(diskUsagePercent * 3), 20);
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            monitorTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}