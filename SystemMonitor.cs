using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public class SystemMonitor
    {
        private readonly Form1 form;
        private readonly Timer sicaklikZamanlayici;
        private readonly Timer tabloZamanlayici;

        public SystemMonitor(Form1 form)
        {
            this.form = form;
            sicaklikZamanlayici = new Timer { Interval = 2000 };
            tabloZamanlayici = new Timer { Interval = 2000 };
        }

        public void SicaklikGuncellemeleriniKur()
        {
            sicaklikZamanlayici.Tick += (object? sender, EventArgs e) => SicakligiGuncelle();
            sicaklikZamanlayici.Start();
            tabloZamanlayici.Tick += (object? sender, EventArgs e) => KullanimTablosunuGuncelle();
            tabloZamanlayici.Start();
            SicakligiGuncelle();
        }

        private void SicakligiGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5)) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
                }

                double islemciSicakligi = sistemVerileri.islemciSicakligi ?? 0.0;
                double ekranKartiSicakligi = sistemVerileri.ekranKartiSicakligi ?? 0.0;
                form.SicaklikGuncelle(islemciSicakligi, ekranKartiSicakligi);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
                form.SicaklikGuncelle(0.0, 0.0);
            }
        }

        public void KullanimTablosunuGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5)) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alınamadı.");
                }

                form.KullanimTablosunuGuncelle(sistemVerileri);
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
            }
        }
    }
}