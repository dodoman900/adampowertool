using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public class SystemMonitor
    {
        private readonly Form1 form;
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
        private SistemVerileri arsivVerileri;

        public SystemMonitor(Form1 form)
        {
            this.form = form;
            guncellemeZamanlayici.Interval = 2000;
            arsivVerileri = YukleArsiv() ?? new SistemVerileri();
        }

        public void GuncellemeleriKur()
        {
            guncellemeZamanlayici.Tick += (object? sender, EventArgs e) => VerileriGuncelle();
            guncellemeZamanlayici.Start();
            VerileriGuncelle();
        }

        private void VerileriGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5)) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
                }

                // Anlık güç ve ortalama güç
                double anlikGuc = sistemVerileri.gucVerileri.Count > 0 ? sistemVerileri.gucVerileri[^1].deger : 0.0;
                double toplamGuc = 0.0;
                foreach (var veri in sistemVerileri.gucVerileri)
                {
                    toplamGuc += veri.deger;
                }
                double ortalamaGuc = sistemVerileri.gucVerileri.Count > 0 ? toplamGuc / sistemVerileri.gucVerileri.Count : 0.0;

                form.GucBilgileriniGuncelle(anlikGuc, ortalamaGuc);

                // Arşive ekle
                arsivVerileri.islemciVerileri.AddRange(sistemVerileri.islemciVerileri);
                arsivVerileri.ramVerileri.AddRange(sistemVerileri.ramVerileri);
                arsivVerileri.diskVerileri.AddRange(sistemVerileri.diskVerileri);
                arsivVerileri.ekranKartiVerileri.AddRange(sistemVerileri.ekranKartiVerileri);
                arsivVerileri.gucVerileri.AddRange(sistemVerileri.gucVerileri);

                // Eski verileri temizle (1 haftadan eski)
                DateTime birHaftaOnce = DateTime.Now.AddDays(-7);
                arsivVerileri.islemciVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.ramVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.diskVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.ekranKartiVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.gucVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
                form.GucBilgileriniGuncelle(0.0, 0.0);
            }
        }

        public SistemVerileri GetArsivVerileri()
        {
            return arsivVerileri;
        }

        public void Kaydet()
        {
            try
            {
                var json = JsonSerializer.Serialize(arsivVerileri, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("arsiv.json", json);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriKaydetmeHatasi);
            }
        }

        private SistemVerileri? YukleArsiv()
        {
            try
            {
                if (File.Exists("arsiv.json"))
                {
                    var json = File.ReadAllText("arsiv.json");
                    return JsonSerializer.Deserialize<SistemVerileri>(json);
                }
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriYuklemeHatasi);
            }
            return null;
        }
    }
}