using System;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace AdamPowerTool
{
    public class SystemGraph : Control
    {
        private readonly FormsPlot cpuPlot;
        private readonly FormsPlot ramPlot;
        private readonly FormsPlot diskPlot;
        private readonly FormsPlot gpuPlot;
        private readonly FormsPlot gucPlot;
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
        private readonly double[] cpuValues = new double[60];
        private readonly double[] ramValues = new double[60];
        private readonly double[] diskValues = new double[60];
        private readonly double[] gpuValues = new double[60];
        private readonly double[] gucValues = new double[60];
        private readonly double[] timeValues = new double[60];
        private int veriIndex = 0;

        public SystemGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici.Interval = 1000;
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();

            for (int i = 0; i < timeValues.Length; i++)
                timeValues[i] = i;

            cpuPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            ramPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            diskPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            gpuPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            gucPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };

            GrafikOlustur(cpuPlot, cpuValues, "CPU Kullanımı (%)", 100, ScottPlot.Color.FromHex("#00FF00"));
            GrafikOlustur(ramPlot, ramValues, "RAM Kullanımı (%)", 100, ScottPlot.Color.FromHex("#FF0000"));
            GrafikOlustur(diskPlot, diskValues, "Disk Kullanımı (%)", 100, ScottPlot.Color.FromHex("#FFFF00"));
            GrafikOlustur(gpuPlot, gpuValues, "GPU Kullanımı (%)", 100, ScottPlot.Color.FromHex("#FFA500"));
            GrafikOlustur(gucPlot, gucValues, "Güç Kullanımı (Watt)", 300, ScottPlot.Color.FromHex("#00FFFF"));

            Controls.AddRange(new Control[] { cpuPlot, ramPlot, diskPlot, gpuPlot, gucPlot });
        }

        private void GrafikOlustur(FormsPlot plot, double[] values, string baslik, double maxDeger, ScottPlot.Color renk)
        {
            var signal = plot.Plot.Add.Signal(values);
            signal.Color = renk;
            signal.Label = baslik;
            plot.Plot.Title(baslik);
            plot.Plot.Axes.SetLimits(0, 60, 0, maxDeger);
            plot.Plot.XLabel("Zaman (saniye)");
            plot.Plot.YLabel("Değer");
            plot.Refresh();
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
                if (sistemVerileri == null)
                {
                    HataYoneticisi.HataEleAl(new Exception("Sistem verileri null"), "Grafik güncellenemedi.");
                    return;
                }

                double cpuDeger = sistemVerileri.islemciVerileri.Count > 0 ? sistemVerileri.islemciVerileri[^1].deger : 0;
                double ramDeger = sistemVerileri.ramVerileri.Count > 0 ? sistemVerileri.ramVerileri[^1].deger : 0;
                double diskDeger = sistemVerileri.diskVerileri.Count > 0 ? sistemVerileri.diskVerileri[^1].deger : 0;
                double gpuDeger = sistemVerileri.ekranKartiVerileri.Count > 0 ? sistemVerileri.ekranKartiVerileri[^1].deger : 0;
                double gucDeger = sistemVerileri.gucVerileri.Count > 0 ? sistemVerileri.gucVerileri[^1].deger : 0;

                if (veriIndex >= cpuValues.Length)
                {
                    Array.Copy(cpuValues, 1, cpuValues, 0, cpuValues.Length - 1);
                    Array.Copy(ramValues, 1, ramValues, 0, ramValues.Length - 1);
                    Array.Copy(diskValues, 1, diskValues, 0, diskValues.Length - 1);
                    Array.Copy(gpuValues, 1, gpuValues, 0, gpuValues.Length - 1);
                    Array.Copy(gucValues, 1, gucValues, 0, gucValues.Length - 1);
                    veriIndex = cpuValues.Length - 1;
                }

                cpuValues[veriIndex] = cpuDeger;
                ramValues[veriIndex] = ramDeger;
                diskValues[veriIndex] = diskDeger;
                gpuValues[veriIndex] = gpuDeger;
                gucValues[veriIndex] = gucDeger;

                veriIndex++;

                cpuPlot.Refresh();
                ramPlot.Refresh();
                diskPlot.Refresh();
                gpuPlot.Refresh();
                gucPlot.Refresh();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "Grafik güncellenemedi.");
            }
        }
    }
}