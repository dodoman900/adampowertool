using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public class MonitorGraph : Control
    {
        private readonly List<(DateTime zaman, double deger)> islemciVerileri = new();
        private readonly List<(DateTime zaman, double deger)> ramVerileri = new();
        private readonly List<(DateTime zaman, double deger)> diskVerileri = new();
        private readonly List<(DateTime zaman, double deger)> ekranKartiVerileri = new();
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici;
        private TimeSpan seciliZamanAraligi = TimeSpan.FromMinutes(5);

        public TimeSpan SeciliZamanAraligi
        {
            get => seciliZamanAraligi;
            set
            {
                seciliZamanAraligi = value;
                Invalidate();
            }
        }

        public MonitorGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici = new System.Windows.Forms.Timer { Interval = 2000 };
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();
            Size = new Size(480, 400);
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
                var sistemVerileri = new SystemMonitor(null).GetArsivVerileri();
                if (sistemVerileri == null)
                {
                    throw new Exception("Arşiv verileri alınamadı.");
                }

                islemciVerileri.Clear();
                ramVerileri.Clear();
                diskVerileri.Clear();
                ekranKartiVerileri.Clear();

                islemciVerileri.AddRange(sistemVerileri.islemciVerileri);
                ramVerileri.AddRange(sistemVerileri.ramVerileri);
                diskVerileri.AddRange(sistemVerileri.diskVerileri);
                ekranKartiVerileri.AddRange(sistemVerileri.ekranKartiVerileri);

                Invalidate();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.Clear(Color.FromArgb(27, 27, 27));

            if (islemciVerileri.Count == 0) return;

            float genislik = Width - 20;
            float solBosluk = 10;
            float cpuRamYukseklik = Height * 0.5f;
            float diskGpuYukseklik = Height * 0.2f;
            DateTime bitisZamani = islemciVerileri[^1].zaman;
            DateTime baslangicZamani = bitisZamani - seciliZamanAraligi;

            // Çerçeveler
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, 10, genislik, cpuRamYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + 10, genislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, genislik, diskGpuYukseklik - 20);

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float yOffset, float yukseklik)
            {
                var noktalar = new List<PointF>();
                for (int i = 0; i < veriler.Count; i++)
                {
                    var (zaman, deger) = veriler[i];
                    if (zaman < baslangicZamani) continue;
                    float x = solBosluk + (float)(zaman - baslangicZamani).TotalSeconds / (float)seciliZamanAraligi.TotalSeconds * genislik;
                    float y = yOffset + (float)(1 - deger / 100) * (yukseklik - 20);
                    noktalar.Add(new PointF(x, y));
                }

                if (noktalar.Count > 1)
                {
                    g.DrawLines(new Pen(renk, 2), noktalar.ToArray());
                }
            }

            // CPU + RAM (büyük grafik)
            GrafikCiz(islemciVerileri, Color.Green, 10, cpuRamYukseklik);
            GrafikCiz(ramVerileri, Color.Red, 10, cpuRamYukseklik);

            // Disk (küçük grafik)
            GrafikCiz(diskVerileri, Color.Yellow, cpuRamYukseklik + 10, diskGpuYukseklik);

            // GPU (küçük grafik)
            GrafikCiz(ekranKartiVerileri, Color.Orange, cpuRamYukseklik + diskGpuYukseklik + 10, diskGpuYukseklik);

            using (var font = new Font("Montserrat", 8))
            {
                g.DrawString("CPU + RAM Kullanımı (%)", font, Brushes.White, solBosluk, 0);
                g.DrawString("Disk Aktivitesi (ölçekli)", font, Brushes.White, solBosluk, cpuRamYukseklik);
                g.DrawString("GPU Kullanımı (%)", font, Brushes.White, solBosluk, cpuRamYukseklik + diskGpuYukseklik);
            }
        }
    }
}