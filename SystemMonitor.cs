using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace AdamPowerTool
{
    public class SystemMonitor
    {
        private readonly System.Windows.Forms.Timer guncellemeZamanlayici = new();
        private SistemVerileri arsivVerileri;
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter ramCounter;
        private readonly PerformanceCounter diskCounter;

        public SystemMonitor()
        {
            guncellemeZamanlayici.Interval = 1000;
            arsivVerileri = YukleArsiv() ?? new SistemVerileri();

            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "PerformanceCounter başlatılamadı.");
                throw;
            }
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
                DateTime zaman = DateTime.Now;

                float cpuKullanimi = cpuCounter.NextValue();
                arsivVerileri.islemciVerileri.Add((zaman, cpuKullanimi));

                float ramKullanimi = ramCounter.NextValue();
                arsivVerileri.ramVerileri.Add((zaman, ramKullanimi));

                float diskKullanimi = diskCounter.NextValue();
                arsivVerileri.diskVerileri.Add((zaman, diskKullanimi));

                // GPU ve Güç simüle (gerçek ölçüm için ek kütüphane gerek)
                arsivVerileri.ekranKartiVerileri.Add((zaman, new Random().Next(0, 100)));
                arsivVerileri.gucVerileri.Add((zaman, new Random().Next(50, 300)));

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
                HataYoneticisi.HataEleAl(ex, "Veri alınamadı.");
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
                HataYoneticisi.HataEleAl(ex, "Veri kaydedilemedi.");
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
                HataYoneticisi.HataEleAl(ex, "Veri yüklenemedi.");
            }
            return null;
        }
    }
}