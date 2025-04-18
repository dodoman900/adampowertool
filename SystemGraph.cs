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
            guncellemeZamanlayici.Interval = 1000; // 1 saniyede bir güncelle
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();

            // Zaman ekseni (son 60 saniye)
            for (int i = 0; i < timeValues.Length; i++)
                timeValues[i] = i;

            // Grafik kontrolleri
            cpuPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            ramPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            diskPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            gpuPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };
            gucPlot = new FormsPlot { Dock = DockStyle.Top, Height = 100 };

            GrafikOlustur(cpuPlot, cpuValues, "CPU Kullanımı (%)", 100, System.Drawing.Color.Green);
            GrafikOlustur(ramPlot, ramValues, "RAM Kullanımı (%)", 100, System.Drawing.Color.Red);
            GrafikOlustur(diskPlot, diskValues, "Disk Kullanımı (%)", 100, System.Drawing.Color.Yellow);
            GrafikOlustur(gpuPlot, gpuValues, "GPU Kullanımı (%)", 100, System.Drawing.Color.Orange);
            GrafikOlustur(gucPlot, gucValues, "Güç Kullanımı (Watt)", 300, System.Drawing.Color.Cyan);

            Controls.AddRange(new Control[] { cpuPlot, ramPlot, diskPlot, gpuPlot, gucPlot });
        }

        private void GrafikOlustur(FormsPlot plot, double[] values, string baslik, double maxDeger, System.Drawing.Color renk)
        {
            var plt = plot.Plot;
            plt.AddSignal(values, 1, label: baslik, color: renk);
            plt.Title(baslik);
            plt.XLabel("Zaman (saniye)");
            plt.YLabel("Değer");
            plt.SetAxisLimits(xMin: 0, xMax: 60, yMin: 0, yMax: maxDeger);
            plot.Render();
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

                // Son veriyi al
                double cpuDeger = sistemVerileri.islemciVerileri.Count > 0 ? sistemVerileri.islemciVerileri[^1].deger : 0;
                double ramDeger = sistemVerileri.ramVerileri.Count > 0 ? sistemVerileri.ramVerileri[^1].deger : 0;
                double diskDeger = sistemVerileri.diskVerileri.Count > 0 ? sistemVerileri.diskVerileri[^1].deger : 0;
                double gpuDeger = sistemVerileri.ekranKartiVerileri.Count > 0 ? sistemVerileri.ekranKartiVerileri[^1].deger : 0;
                double gucDeger = sistemVerileri.gucVerileri.Count > 0 ? sistemVerileri.gucVerileri[^1].deger : 0;

                // Verileri kaydır ve yeni değeri ekle
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

                // Grafikleri güncelle
                cpuPlot.Render();
                ramPlot.Render();
                diskPlot.Render();
                gpuPlot.Render();
                gucPlot.Render();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "Grafik güncellenemedi.");
            }
        }
    }
};