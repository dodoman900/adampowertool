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
                var sistemVerileri = new SystemMonitor().GetArsivVerileri();
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

            // Veri kontrolü
            if (islemciVerileri.Count < 2 && ramVerileri.Count < 2 && gucVerileri.Count < 2)
            {
                using var hataFont = new Font("Montserrat", 10);
                g.DrawString("Veri bekleniyor...", hataFont, Brushes.White, 10, 10);
                return;
            }

            float solBosluk = 10;
            float cpuRamGenislik = (Width - 30) * 0.6f;
            float gucGenislik = (Width - 30) * 0.4f;
            float cpuRamYukseklik = Height * 0.5f;
            float diskGpuYukseklik = Height * 0.2f;

            // Son zamanı belirle
            DateTime bitisZamani = DateTime.Now;
            var tumZamanlar = new List<DateTime>();
            tumZamanlar.AddRange(islemciVerileri.ConvertAll(v => v.zaman));
            tumZamanlar.AddRange(ramVerileri.ConvertAll(v => v.zaman));
            tumZamanlar.AddRange(gucVerileri.ConvertAll(v => v.zaman));
            if (tumZamanlar.Count > 0)
            {
                bitisZamani = tumZamanlar.Max();
            }

            // Başlangıç zamanını güvenli hesapla
            DateTime baslangicZamani;
            try
            {
                baslangicZamani = bitisZamani.Subtract(seciliZamanAraligi);
                if (baslangicZamani < DateTime.MinValue.AddDays(1))
                    baslangicZamani = DateTime.MinValue.AddDays(1);
                if (baslangicZamani > bitisZamani)
                    baslangicZamani = bitisZamani.AddMinutes(-5);
            }
            catch
            {
                baslangicZamani = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
            }

            // Çerçeveler
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, 10, cpuRamGenislik, cpuRamYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk + cpuRamGenislik + 10, 10, gucGenislik, Height - 20);

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float xOffset, float yOffset, float genislik, float yukseklik, double maxDeger)
            {
                var noktalar = new List<PointF>();
                foreach (var (zaman, deger) in veriler)
                {
                    if (zaman < baslangicZamani || zaman > bitisZamani || double.IsNaN(deger) || double.IsInfinity(deger))
                        continue;
                    double saniyeFarki = (zaman - baslangicZamani).TotalSeconds;
                    double toplamSaniye = seciliZamanAraligi.TotalSeconds;
                    if (toplamSaniye <= 0) toplamSaniye = 1;
                    float x = xOffset + (float)(saniyeFarki / toplamSaniye * genislik);
                    float y = yOffset + (float)(1 - deger / maxDeger) * (yukseklik - 20);
                    if (float.IsNaN(x) || float.IsNaN(y) || float.IsInfinity(x) || float.IsInfinity(y))
                        continue;
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

            // Etiketler
            using var etiketFont = new Font("Montserrat", 8);
            g.DrawString("CPU + RAM Kullanımı (%)", etiketFont, Brushes.White, solBosluk, 0);
            g.DrawString("Disk Aktivitesi (ölçekli)", etiketFont, Brushes.White, solBosluk, cpuRamYukseklik);
            g.DrawString("GPU Kullanımı (%)", etiketFont, Brushes.White, solBosluk, cpuRamYukseklik + diskGpuYukseklik);
            g.DrawString("Güç Kullanımı (Watt)", etiketFont, Brushes.White, solBosluk + cpuRamGenislik + 10, 0);
        }
    }
}