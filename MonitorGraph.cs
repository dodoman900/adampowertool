using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace AdamPowerTool
{
    public partial class MonitorGraph : UserControl
    {
        private Panel graphPanel = new Panel();
        private TimeSpan seciliZamanAraligi = TimeSpan.FromMinutes(5);
        private System.Windows.Forms.Timer yenilemeZamanlayicisi = new System.Windows.Forms.Timer();
        private readonly GraphRenderer islemciGrafik;
        private readonly GraphRenderer ramGrafik;
        private readonly GraphRenderer diskGrafik;
        private readonly GraphRenderer ekranKartiGrafik;
        private readonly GraphRenderer gucGrafik;

        public TimeSpan SeciliZamanAraligi
        {
            get => seciliZamanAraligi;
            set
            {
                seciliZamanAraligi = value;
                islemciGrafik.Clear();
                ramGrafik.Clear();
                diskGrafik.Clear();
                ekranKartiGrafik.Clear();
                gucGrafik.Clear();
                graphPanel.Invalidate();
            }
        }

        public MonitorGraph()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            float baslangicGrafikGenisligi = 700;
            float islemciGrafikYuksekligi = 100;
            float diskEkranKartiYuksekligi = 50;
            islemciGrafik = new GraphRenderer(baslangicGrafikGenisligi, islemciGrafikYuksekligi, Color.FromArgb(255, 165, 0)); // Turuncu
            ramGrafik = new GraphRenderer(baslangicGrafikGenisligi, islemciGrafikYuksekligi, Color.FromArgb(0, 255, 0)); // Yeşil
            diskGrafik = new GraphRenderer(baslangicGrafikGenisligi / 2 - 10, diskEkranKartiYuksekligi, Color.FromArgb(255, 165, 0)); // Turuncu
            ekranKartiGrafik = new GraphRenderer(baslangicGrafikGenisligi / 2 - 10, diskEkranKartiYuksekligi, Color.FromArgb(0, 255, 0)); // Yeşil
            gucGrafik = new GraphRenderer(baslangicGrafikGenisligi, islemciGrafikYuksekligi, Color.FromArgb(255, 0, 0)); // Kırmızı neon

            KontrolleriKur();
            yenilemeZamanlayicisi.Interval = 500;
            yenilemeZamanlayicisi.Tick += (s, e) => GrafikleriGuncelle();
        }

        private void KontrolleriKur()
        {
            graphPanel.Location = new Point(0, 0);
            graphPanel.Size = new Size(this.Width, this.Height);
            graphPanel.BorderStyle = BorderStyle.FixedSingle;
            graphPanel.BackColor = Color.FromArgb(46, 46, 46);
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, graphPanel, new object[] { true });
            graphPanel.Paint += GrafikPaneli_Ciz;

            this.Controls.Add(graphPanel);
        }

        private void GrafikleriGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(seciliZamanAraligi) as SistemVerileri;
                if (sistemVerileri == null || sistemVerileri.islemciVerileri == null || sistemVerileri.ramVerileri == null || sistemVerileri.diskVerileri == null || sistemVerileri.ekranKartiVerileri == null || sistemVerileri.gucVerileri == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
                }

                islemciGrafik.UpdatePoints(sistemVerileri.islemciVerileri, seciliZamanAraligi, 0);
                ramGrafik.UpdatePoints(sistemVerileri.ramVerileri, seciliZamanAraligi, 0);
                diskGrafik.UpdatePoints(sistemVerileri.diskVerileri, seciliZamanAraligi, 0);
                ekranKartiGrafik.UpdatePoints(sistemVerileri.ekranKartiVerileri, seciliZamanAraligi, 0);
                gucGrafik.UpdatePoints(sistemVerileri.gucVerileri, seciliZamanAraligi, 0);

                graphPanel.Invalidate();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.GrafikCizimHatasi);
            }
        }

        private void GrafikPaneli_Ciz(object? sender, PaintEventArgs e)
        {
            try
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new LinearGradientBrush(graphPanel.ClientRectangle, Color.FromArgb(27, 27, 27), Color.FromArgb(46, 46, 46), 90F))
                {
                    g.FillRectangle(brush, graphPanel.ClientRectangle);
                }

                float islemciGrafikYuksekligi = (graphPanel.Height - 100) / 4;
                float ramGrafikYuksekligi = islemciGrafikYuksekligi;
                float diskEkranKartiYuksekligi = islemciGrafikYuksekligi / 2;
                float gucGrafikYuksekligi = islemciGrafikYuksekligi;

                GrafikEtiketiCiz(g, "İşlemci Kullanımı (%)", 0);
                islemciGrafik.Draw(g, 0);

                GrafikEtiketiCiz(g, "RAM Kullanımı (%)", islemciGrafikYuksekligi + 20);
                ramGrafik.Draw(g, islemciGrafikYuksekligi + 20);

                GrafikEtiketiCiz(g, "Disk Aktivitesi (ölçekli)", (islemciGrafikYuksekligi + 20) * 2);
                diskGrafik.Draw(g, (islemciGrafikYuksekligi + 20) * 2);

                GrafikEtiketiCiz(g, "Ekran Kartı Kullanımı (%)", (islemciGrafikYuksekligi + 20) * 2 + diskEkranKartiYuksekligi + 10);
                ekranKartiGrafik.Draw(g, (islemciGrafikYuksekligi + 20) * 2 + diskEkranKartiYuksekligi + 10);

                GrafikEtiketiCiz(g, "Güç Kullanımı (Watt)", (islemciGrafikYuksekligi + 20) * 3 + diskEkranKartiYuksekligi + 10);
                gucGrafik.Draw(g, (islemciGrafikYuksekligi + 20) * 3 + diskEkranKartiYuksekligi + 10);

                WattDegerleriniCiz(g, (islemciGrafikYuksekligi + 20) * 3 + diskEkranKartiYuksekligi + 10 + gucGrafikYuksekligi + 10);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.GrafikCizimHatasi);
            }
        }

        private void GrafikEtiketiCiz(Graphics g, string etiket, float yKonumu)
        {
            using (var etiketFontu = new Font("Montserrat", 12, FontStyle.Bold))
            using (var etiketFirca = new SolidBrush(Color.FromArgb(255, 0, 0)))
            {
                g.DrawString(etiket, etiketFontu, etiketFirca, 20, yKonumu - 20);
            }
        }

        private void WattDegerleriniCiz(Graphics g, float yKonumu)
        {
            var sistemVerileri = BilgisayarBilgileri.GetSystemData(seciliZamanAraligi) as SistemVerileri;
            if (sistemVerileri == null || sistemVerileri.gucVerileri == null) return;

            double anlikWatt = sistemVerileri.gucVerileri.Count > 0 ? sistemVerileri.gucVerileri[^1].deger : 0.0;
            double saatlikOrtalama = sistemVerileri.gucVerileri.Count > 0 ? sistemVerileri.gucVerileri.Average(d => d.deger) : 0.0;
            double tasarrufEdilen = 10.0;

            using (var font = new Font("Montserrat", 10, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(255, 0, 0)))
            {
                g.DrawString($"Anlık: {anlikWatt:F1}W", font, brush, 20, yKonumu);
                g.DrawString($"Saatlik Ortalama: {saatlikOrtalama:F1}W", font, brush, 20, yKonumu + 20);
                g.DrawString($"Tasarruf Edilen: {tasarrufEdilen:F1}W", font, brush, 20, yKonumu + 40);
            }
        }

        public void GuncellemeyiBaslat()
        {
            yenilemeZamanlayicisi.Start();
        }

        public void GuncellemeyiDurdur()
        {
            yenilemeZamanlayicisi.Stop();
        }

        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(740, 500);
        }
    }

    internal class GraphRenderer
    {
        private readonly float genislik;
        private readonly float yukseklik;
        private readonly Color grafikRengi;
        private readonly List<PointF> noktalar;

        public GraphRenderer(float genislik, float yukseklik, Color grafikRengi)
        {
            this.genislik = genislik;
            this.yukseklik = yukseklik;
            this.grafikRengi = grafikRengi;
            this.noktalar = new List<PointF>();
        }

        public void UpdatePoints(List<(DateTime zaman, double deger)> veriler, TimeSpan zamanAraligi, float yKonumu)
        {
            noktalar.Clear();
            if (veriler == null || veriler.Count == 0) return;

            DateTime bitisZamani = veriler[^1].zaman;
            DateTime baslangicZamani = bitisZamani - zamanAraligi;

            float xAdimi = genislik / Math.Max(1, veriler.Count - 1);
            for (int i = 0; i < veriler.Count; i++)
            {
                if (veriler[i].zaman >= baslangicZamani)
                {
                    float x = (i * xAdimi);
                    float y = (float)(yukseklik - (veriler[i].deger / 100.0 * yukseklik)) + yKonumu;
                    noktalar.Add(new PointF(x, y));
                }
            }
        }

        public void Draw(Graphics g, float yKonumu)
        {
            if (noktalar.Count < 2) return;

            using (var kalem = new Pen(grafikRengi, 2))
            {
                g.DrawLines(kalem, noktalar.ToArray());

                for (int i = 0; i <= 100; i += 20)
                {
                    float y = yukseklik - (i / 100.0f * yukseklik) + yKonumu;
                    using (var izgaraKalemi = new Pen(Color.FromArgb(50, 255, 255, 255), 1))
                    {
                        g.DrawLine(izgaraKalemi, 0, y, genislik, y);
                    }
                    using (var etiketFontu = new Font("Montserrat", 8, FontStyle.Bold))
                    using (var etiketFirca = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                    {
                        g.DrawString(i.ToString(), etiketFontu, etiketFirca, genislik + 5, y - 5);
                    }
                }
            }
        }

        public void Clear()
        {
            noktalar.Clear();
        }
    }
}