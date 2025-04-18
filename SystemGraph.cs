using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public class SystemGraph : Control
    {
        private readonly List<(DateTime zaman, double deger)> islemciVerileri = new();
        private readonly List<(DateTime zaman, double deger)> ramVerileri = new();
        private readonly List<(DateTime zaman, double deger)> diskVerileri = new();
        private readonly List<(DateTime zaman, double deger)> ekranKartiVerileri = new();
        private readonly List<(DateTime zaman, double deger)> gucVerileri = new();
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
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

        public SystemGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici.Interval = 2000;
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();
            Size = new Size(760, 500);
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
                var sistemVerileri = new SystemMonitor(null!).GetArsivVerileri();
                if (sistemVerileri == null)
                {
                    throw new Exception("Arşiv verileri alınamadı.");
                }

                islemciVerileri.Clear();
                ramVerileri.Clear();
                diskVerileri.Clear();
                ekranKartiVerileri.Clear();
                gucVerileri.Clear();

                islemciVerileri.AddRange(sistemVerileri.islemciVerileri);
                ramVerileri.AddRange(sistemVerileri.ramVerileri);
                diskVerileri.AddRange(sistemVerileri.diskVerileri);
                ekranKartiVerileri.AddRange(sistemVerileri.ekranKartiVerileri);
                gucVerileri.AddRange(sistemVerileri.gucVerileri);

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

            if (islemciVerileri.Count == 0 || gucVerileri.Count == 0) return;

            float solBosluk = 10;
            float cpuRamGenislik = (Width - 30) * 0.6f;
            float gucGenislik = (Width - 30) * 0.4f;
            float cpuRamYukseklik = Height * 0.5f;
            float diskGpuYukseklik = Height * 0.2f;

            DateTime? sonZaman = null;
            if (islemciVerileri.Count > 0)
                sonZaman = islemciVerileri[^1].zaman;
            else if (gucVerileri.Count > 0)
                sonZaman = gucVerileri[^1].zaman;

            if (!sonZaman.HasValue) return;

            DateTime bitisZamani = sonZaman.Value;
            DateTime baslangicZamani;
            try
            {
                baslangicZamani = bitisZamani.Subtract(seciliZamanAraligi);
            }
            catch (ArgumentOutOfRangeException)
            {
                baslangicZamani = DateTime.Now.Subtract(seciliZamanAraligi);
            }

            // Çerçeveler
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, 10, cpuRamGenislik, cpuRamYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk + cpuRamGenislik + 10, 10, gucGenislik, Height - 20);

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float xOffset, float yOffset, float genislik, float yukseklik, double maxDeger)
            {
                var noktalar = new List<PointF>();
                for (int i = 0; i < veriler.Count; i++)
                {
                    (DateTime zaman, double deger) = veriler[i];
                    if (zaman < baslangicZamani || zaman > bitisZamani) continue;
                    float x = xOffset + (float)(zaman - baslangicZamani).TotalSeconds / (float)seciliZamanAraligi.TotalSeconds * genislik;
                    float y = yOffset + (float)(1 - deger / maxDeger) * (yukseklik - 20);
                    noktalar.Add(new PointF(x, y));
                }

                if (noktalar.Count > 1)
                {
                    g.DrawLines(new Pen(renk, 2), noktalar.ToArray());
                }
            }

            // CPU + RAM (büyük grafik)
            GrafikCiz(islemciVerileri, Color.Green, solBosluk, 10, cpuRamGenislik, cpuRamYukseklik, 100);
            GrafikCiz(ramVerileri, Color.Red, solBosluk, 10, cpuRamGenislik, cpuRamYukseklik, 100);

            // Disk (küçük grafik)
            GrafikCiz(diskVerileri, Color.Yellow, solBosluk, cpuRamYukseklik + 10, cpuRamGenislik, diskGpuYukseklik, 100);

            // GPU (küçük grafik)
            GrafikCiz(ekranKartiVerileri, Color.Orange, solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, cpuRamGenislik, diskGpuYukseklik, 100);

            // Güç (büyük grafik, sağda)
            GrafikCiz(gucVerileri, Color.Cyan, solBosluk + cpuRamGenislik + 10, 10, gucGenislik, Height, 300);

            using var font = new Font("Montserrat", 8);
            g.DrawString("CPU + RAM Kullanımı (%)", font, Brushes.White, solBosluk, 0);
            g.DrawString("Disk Aktivitesi (ölçekli)", font, Brushes.White, solBosluk, cpuRamYukseklik);
            g.DrawString("GPU Kullanımı (%)", font, Brushes.White, solBosluk, cpuRamYukseklik + diskGpuYukseklik);
            g.DrawString("Güç Kullanımı (Watt)", font, Brushes.White, solBosluk + cpuRamGenislik + 10, 0);
        }
    }
}