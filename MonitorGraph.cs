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
        private readonly List<(DateTime zaman, double deger)> gucVerileri = new();
        private readonly Timer guncellemeZamanlayici;
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
            guncellemeZamanlayici = new Timer { Interval = 2000 };
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();
            Size = new Size(740, 440);
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
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(seciliZamanAraligi) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
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

            if (islemciVerileri.Count == 0) return;

            float genislik = Width;
            float yukseklik = Height / 5f;
            DateTime bitisZamani = islemciVerileri[^1].zaman;
            DateTime baslangicZamani = bitisZamani - seciliZamanAraligi;

            void GrafikCiz(List<(DateTime zaman, double deger)> veriler, Color renk, float yOffset)
            {
                var noktalar = new List<PointF>();
                for (int i = 0; i < veriler.Count; i++)
                {
                    var (zaman, deger) = veriler[i];
                    if (zaman < baslangicZamani) continue;
                    float x = (float)(zaman - baslangicZamani).TotalSeconds / (float)seciliZamanAraligi.TotalSeconds * genislik;
                    float y = yOffset + (float)(1 - deger / 100) * yukseklik;
                    noktalar.Add(new PointF(x, y));
                }

                if (noktalar.Count > 1)
                {
                    g.DrawLines(new Pen(renk, 2), noktalar.ToArray());
                }
            }

            GrafikCiz(islemciVerileri, Color.Green, 0);
            GrafikCiz(ramVerileri, Color.Red, yukseklik);
            GrafikCiz(diskVerileri, Color.Yellow, yukseklik * 2);
            GrafikCiz(ekranKartiVerileri, Color.Orange, yukseklik * 3);
            GrafikCiz(gucVerileri, Color.Cyan, yukseklik * 4);

            using (var font = new Font("Montserrat", 8))
            {
                g.DrawString("İşlemci Kullanımı (%)", font, Brushes.Green, 0, 0);
                g.DrawString("RAM Kullanımı (%)", font, Brushes.Red, 0, yukseklik);
                g.DrawString("Disk Aktivitesi (ölçekli)", font, Brushes.Yellow, 0, yukseklik * 2);
                g.DrawString("Ekran Kartı Kullanımı (%)", font, Brushes.Orange, 0, yukseklik * 3);
                g.DrawString("Güç Kullanımı (Watt)", font, Brushes.Cyan, 0, yukseklik * 4);
            }
        }
    }
}