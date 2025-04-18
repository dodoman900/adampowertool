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
        private TimeSpan seciliZamanAraligi = TimeSpan.FromMinutes(1); // Görev Yöneticisi gibi 1 dakikalık pencere

        public TimeSpan SeciliZamanAraligi
        {
            get => seciliZamanAraligi;
            set
            {
                seciliZamanAraligi = value > TimeSpan.Zero ? value : TimeSpan.FromMinutes(1);
                Invalidate();
            }
        }

        public SystemGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici.Interval = 1000; // 1 saniyede bir güncelle
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
                    return;

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

            if (islemciVerileri.Count < 2 && ramVerileri.Count < 2)
            {
                using var hataFont = new Font("Montserrat", 10);
                g.DrawString("Veri bekleniyor...", hataFont, Brushes.White, 10, 10);
                return;
            }

            float solBosluk = 10;
            float grafikGenislik = (Width - 30);
            float grafikYukseklik = (Height - 50) / 5; // 5 grafik için böl

            DateTime bitisZamani = DateTime.Now;
            DateTime baslangicZamani = bitisZamani.Subtract(seciliZamanAraligi);
            if (baslangicZamani < DateTime.MinValue.AddDays(1))
                baslangicZamani = DateTime.MinValue.AddDays(1);

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float yOffset, string etiket, double maxDeger)
            {
                // Çerçeve
                g.DrawRectangle(new Pen(Color.Gray, 1), solBosluk, yOffset, grafikGenislik, grafikYukseklik - 10);

                // Veri noktaları
                var noktalar = new List<PointF>();
                double toplamSaniye = seciliZamanAraligi.TotalSeconds;
                if (toplamSaniye <= 0) toplamSaniye = 1;

                foreach (var (zaman, deger) in veriler)
                {
                    if (zaman < baslangicZamani || zaman > bitisZamani || double.IsNaN(deger) || double.IsInfinity(deger))
                        continue;
                    double saniyeFarki = (zaman - baslangicZamani).TotalSeconds;
                    float x = solBosluk + (float)(saniyeFarki / toplamSaniye * grafikGenislik);
                    float y = yOffset + (float)(1 - deger / maxDeger) * (grafikYukseklik - 10);
                    if (float.IsNaN(x) || float.IsNaN(y) || float.IsInfinity(x) || float.IsInfinity(y))
                        continue;
                    noktalar.Add(new PointF(x, y));
                }

                if (noktalar.Count > 1)
                    g.DrawLines(new Pen(renk, 2), noktalar.ToArray());

                // Etiket
                using var etiketFont = new Font("Montserrat", 8);
                g.DrawString(etiket, etiketFont, Brushes.White, solBosluk, yOffset - 10);
            }

            // Grafikleri çiz (Görev Yöneticisi tarzı, alt alta)
            GrafikCiz(islemciVerileri, Color.Green, 10, "CPU Kullanımı (%)", 100);
            GrafikCiz(ramVerileri, Color.Red, 10 + grafikYukseklik, "RAM Kullanımı (%)", 100);
            GrafikCiz(diskVerileri, Color.Yellow, 10 + grafikYukseklik * 2, "Disk Kullanımı (%)", 100);
            GrafikCiz(ekranKartiVerileri, Color.Orange, 10 + grafikYukseklik * 3, "GPU Kullanımı (%)", 100);
            GrafikCiz(gucVerileri, Color.Cyan, 10 + grafikYukseklik * 4, "Güç Kullanımı (Watt)", 300);
        }
    }
}