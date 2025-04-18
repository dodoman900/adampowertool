using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace AdamPowerTool
{
    public class SystemGraph : UserControl
    {
        private readonly CartesianChart cpuChart;
        private readonly CartesianChart ramChart;
        private readonly CartesianChart diskChart;
        private readonly CartesianChart gpuChart;
        private readonly CartesianChart gucChart;
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
        private readonly ChartValues<double> cpuValues = new();
        private readonly ChartValues<double> ramValues = new();
        private readonly ChartValues<double> diskValues = new();
        private readonly ChartValues<double> gpuValues = new();
        private readonly ChartValues<double> gucValues = new();

        public SystemGraph()
        {
            guncellemeZamanlayici.Interval = 1000;
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();

            cpuChart = new CartesianChart { Dock = DockStyle.Top, Height = 100 };
            ramChart = new CartesianChart { Dock = DockStyle.Top, Height = 100 };
            diskChart = new CartesianChart { Dock = DockStyle.Top, Height = 100 };
            gpuChart = new CartesianChart { Dock = DockStyle.Top, Height = 100 };
            gucChart = new CartesianChart { Dock = DockStyle.Top, Height = 100 };

            GrafikOlustur(cpuChart, cpuValues, "CPU Kullanımı (%)", 100);
            GrafikOlustur(ramChart, ramValues, "RAM Kullanımı (%)", 100);
            GrafikOlustur(diskChart, diskValues, "Disk Kullanımı (%)", 100);
            GrafikOlustur(gpuChart, gpuValues, "GPU Kullanımı (%)", 100);
            GrafikOlustur(gucChart, gucValues, "Güç Kullanımı (Watt)", 300);

            Controls.AddRange(new Control[] { cpuChart, ramChart, diskChart, gpuChart, gucChart });
        }

        private void GrafikOlustur(CartesianChart chart, ChartValues<double> values, string baslik, double maxDeger)
        {
            chart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = baslik,
                    Values = values,
                    PointGeometry = null,
                    Stroke = System.Windows.Media.Brushes.Green
                }
            };
            chart.AxisX.Add(new Axis { IsMerged = true, ShowLabels = false });
            chart.AxisY.Add(new Axis { MinValue = 0, MaxValue = maxDeger });
        }

        public void GuncellemeyiBaslat()
        {
            guncellemeZamanlayici.Start();
        }

        public void GuncellemeyiDurdur()
        {
            guncellemeZamanlayici.Stop();
        }

        private void Guncelle()
        {
            try
            {
                var sistemVerileri = new SystemMonitor().GetArsivVerileri();
                if (sistemVerileri == null) return;

                DateTime simdi = DateTime.Now;
                DateTime birDakikaOnce = simdi.AddMinutes(-1);

                cpuValues.Clear();
                ramValues.Clear();
                diskValues.Clear();
                gpuValues.Clear();
                gucValues.Clear();

                foreach (var veri in sistemVerileri.islemciVerileri)
                    if (veri.zaman >= birDakikaOnce)
                        cpuValues.Add(veri.deger);

                foreach (var veri in sistemVerileri.ramVerileri)
                    if (veri.zaman >= birDakikaOnce)
                        ramValues.Add(veri.deger);

                foreach (var veri in sistemVerileri.diskVerileri)
                    if (veri.zaman >= birDakikaOnce)
                        diskValues.Add(veri.deger);

                foreach (var veri in sistemVerileri.ekranKartiVerileri)
                    if (veri.zaman >= birDakikaOnce)
                        gpuValues.Add(veri.deger);

                foreach (var veri in sistemVerileri.gucVerileri)
                    if (veri.zaman >= birDakikaOnce)
                        gucValues.Add(veri.deger);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "Grafik güncellenemedi.");
            }
        }
    }
}