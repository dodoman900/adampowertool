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
        private double anlikGuc = 0.0;
        private double ortalamaGuc = 0.0;

        public TimeSpan SeciliZamanAraligi
        {
            get => seciliZamanAraligi;
            set
            {
                seciliZamanAraligi = value > TimeSpan.Zero ? value : TimeSpan.FromMinutes(5);
                Invalidate();
            }
        }

        public SystemGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici.Interval = 2000;
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();
            Size = new Size(760, 500);
            VerileriSimuleEt(); // İlk açılışta test verileriyle doldur
        }

        public void GuncellemeyiBaslat()
        {
            guncellemeZamanlayici.Start();
        }

        public void GuncellemeyiDurdur()
        {
            guncellemeZamanlayici.Stop();
        }

        private void VerileriSimuleEt()
        {
            var rastgele = new Random();
            DateTime simdi = DateTime.Now;
            int veriSayisi = (int)(seciliZamanAraligi.TotalSeconds / 2); // Her 2 saniyede veri
            for (int i = 0; i < veriSayisi; i++)
            {
                DateTime zaman = simdi.AddSeconds(-2 * i);
                islemciVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 90 + 10))));
                ramVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 60 + 20))));
                diskVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 50))));
                ekranKartiVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 65 + 5))));
                gucVerileri.Add((zaman, Math.Min(300, Math.Max(50, rastgele.NextDouble() * 200 + 50))));
            }
        }

        private void Guncelle()
        {
            try
            {
                var sistemVerileri = new SystemMonitor().GetArsivVerileri();
                if (sistemVerileri == null)
                {
                    VerileriSimuleEt(); // Arşiv yoksa simüle et
                }
                else
                {
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
                }

                // Anlık ve ortalama güç hesapla
                anlikGuc = gucVerileri.Count > 0 ? gucVerileri[^1].deger : 0.0;
                double toplamGuc = 0.0;
                foreach (var veri in gucVerileri)
                    toplamGuc += veri.deger;
                ortalamaGuc = gucVerileri.Count > 0 ? toplamGuc / gucVerileri.Count : 0.0;

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

            // Zaman aralığını güvenli belirle
            DateTime bitisZamani = DateTime.Now;
            DateTime baslangicZamani = bitisZamani.Subtract(seciliZamanAraligi);
            if (baslangicZamani < DateTime.MinValue.AddDays(1))
                baslangicZamani = DateTime.MinValue.AddDays(1);

            // Çerçeveler
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, 10, cpuRamGenislik, cpuRamYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, cpuRamGenislik, diskGpuYukseklik - 20);
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk + cpuRamGenislik + 10, 10, gucGenislik, Height - 20);

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float xOffset, float yOffset, float genislik, float yukseklik, double maxDeger)
            {
                var noktalar = new List<PointF>();
                double toplamSaniye = seciliZamanAraligi.TotalSeconds;
                if (toplamSaniye <= 0) toplamSaniye = 1;

                foreach (var (zaman, deger) in veriler)
                {
                    if (zaman < baslangicZamani || zaman > bitisZamani || double.IsNaN(deger) || double.IsInfinity(deger))
                        continue;
                    double saniyeFarki = (zaman - baslangicZamani).TotalSeconds;
                    float x = xOffset + (float)(saniyeFarki / toplamSaniye * genislik);
                    float y = yOffset + (float)(1 - deger / maxDeger) * (yukseklik - 20);
                    if (float.IsNaN(x) || float.IsNaN(y) || float.IsInfinity(x) || float.IsInfinity(y))
                        continue;
                    noktalar.Add(new PointF(x, y));
                }

                if (noktalar.Count > 1)
                    g.DrawLines(new Pen(renk, 2), noktalar.ToArray());
            }

            // Grafikleri çiz
            GrafikCiz(islemciVerileri, Color.Green, solBosluk, 10, cpuRamGenislik, cpuRamYukseklik, 100);
            GrafikCiz(ramVerileri, Color.Red, solBosluk, 10, cpuRamGenislik, cpuRamYukseklik, 100);
            GrafikCiz(diskVerileri, Color.Yellow, solBosluk, cpuRamYukseklik + 10, cpuRamGenislik, diskGpuYukseklik, 100);
            GrafikCiz(ekranKartiVerileri, Color.Orange, solBosluk, cpuRamYukseklik + diskGpuYukseklik + 10, cpuRamGenislik, diskGpuYukseklik, 100);
            GrafikCiz(gucVerileri, Color.Cyan, solBosluk + cpuRamGenislik + 10, 10, gucGenislik, Height, 300);

            // Etiketler
            using var etiketFont = new Font("Montserrat", 8);
            g.DrawString("CPU + RAM Kullanımı (%)", etiketFont, Brushes.White, solBosluk, 0);
            g.DrawString("Disk Aktivitesi (ölçekli)", etiketFont, Brushes.White, solBosluk, cpuRamYukseklik);
            g.DrawString("GPU Kullanımı (%)", etiketFont, Brushes.White, solBosluk, cpuRamYukseklik + diskGpuYukseklik);
            g.DrawString("Güç Kullanımı (Watt)", etiketFont, Brushes.White, solBosluk + cpuRamGenislik + 10, 0);

            // Anlık ve ortalama güç
            using var gucFont = new Font("Montserrat", 10, FontStyle.Bold);
            g.DrawString($"Anlık: {anlikGuc:F1} W", gucFont, Brushes.Cyan, solBosluk + cpuRamGenislik + 10, Height - 60);
            g.DrawString($"Ortalama: {ortalamaGuc:F1} W", gucFont, Brushes.Cyan, solBosluk + cpuRamGenislik + 10, Height - 30);
        }
    }
}