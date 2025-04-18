using System;
using System.IO;
using System.Text.Json;

namespace AdamPowerTool
{
    public class SystemMonitor
    {
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
        private SistemVerileri arsivVerileri;

        public SystemMonitor()
        {
            guncellemeZamanlayici.Interval = 2000;
            arsivVerileri = YukleArsiv() ?? new SistemVerileri();
        }

        public void GuncellemeleriKur()
        {
            guncellemeZamanlayici.Tick += (s, e) => VerileriGuncelle();
            guncellemeZamanlayici.Start();
            VerileriGuncelle();
        }

        private void VerileriGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5));
                if (sistemVerileri == null)
                    return;

                arsivVerileri.islemciVerileri.AddRange(sistemVerileri.islemciVerileri);
                arsivVerileri.ramVerileri.AddRange(sistemVerileri.ramVerileri);
                arsivVerileri.diskVerileri.AddRange(sistemVerileri.diskVerileri);
                arsivVerileri.ekranKartiVerileri.AddRange(sistemVerileri.ekranKartiVerileri);
                arsivVerileri.gucVerileri.AddRange(sistemVerileri.gucVerileri);

                DateTime birHaftaOnce = DateTime.Now.AddDays(-7);
                arsivVerileri.islemciVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.ramVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.diskVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.ekranKartiVerileri.RemoveAll(v => v.zaman < birHaftaOnce);
                arsivVerileri.gucVerileri.RemoveAll(v => v.zaman < birHaftaOnce);

                Kaydet();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
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