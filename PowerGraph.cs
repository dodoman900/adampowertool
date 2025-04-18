using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public class PowerGraph : Control
    {
        private readonly List<(DateTime zaman, double deger)> gucVerileri = new();
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

        public PowerGraph()
        {
            DoubleBuffered = true;
            guncellemeZamanlayici = new System.Windows.Forms.Timer { Interval = 2000 };
            guncellemeZamanlayici.Tick += (s, e) => Guncelle();
            Size = new Size(270, 400);
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

                gucVerileri.Clear();
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

            if (gucVerileri.Count == 0) return;

            float genislik = Width - 20;
            float solBosluk = 10;
            float yukseklik = Height - 20;
            DateTime bitisZamani = gucVerileri[^1].zaman;
            DateTime baslangicZamani = bitisZamani - seciliZamanAraligi;

            // Çerçeve
            g.DrawRectangle(new Pen(Color.Gray, 2), solBosluk, 10, genislik, yukseklik);

            var noktalar = new List<PointF>();
            for (int i = 0; i < gucVerileri.Count; i++)
            {
                var (zaman, deger) = gucVerileri[i];
                if (zaman < baslangicZamani) continue;
                float x = solBosluk + (float)(zaman - baslangicZamani).TotalSeconds / (float)seciliZamanAraligi.TotalSeconds * genislik;
                float y = 10 + (float)(1 - deger / 300) * yukseklik;
                noktalar.Add(new PointF(x, y));
            }

            if (noktalar.Count > 1)
            {
                g.DrawLines(new Pen(Color.Cyan, 2), noktalar.ToArray());
            }

            using (var font = new Font("Montserrat", 8))
            {
                g.DrawString("Güç Kullanımı (Watt)", font, Brushes.White, solBosluk, 0);
            }
        }
    }
}