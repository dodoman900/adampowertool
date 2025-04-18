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
        private readonly PerformanceCounter gpuCounter;

        public SystemMonitor()
        {
            guncellemeZamanlayici.Interval = 1000; // 1 saniyede bir güncelle
            arsivVerileri = YukleArsiv() ?? new SistemVerileri();

            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

                // GPU kullanımı için Performance Counter (NVIDIA için örnek)
                gpuCounter = new PerformanceCounter("GPU Engine", "Utilization Percentage", "pid_0_luid_0x00000000_0x0000XXXX"); // GPU için dinamik olarak ayarlanmalı
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

                // CPU Kullanımı
                float cpuKullanimi = cpuCounter.NextValue();
                arsivVerileri.islemciVerileri.Add((zaman, cpuKullanimi));

                // RAM Kullanımı
                float ramKullanimi = ramCounter.NextValue();
                arsivVerileri.ramVerileri.Add((zaman, ramKullanimi));

                // Disk Kullanımı
                float diskKullanimi = diskCounter.NextValue();
                arsivVerileri.diskVerileri.Add((zaman, diskKullanimi));

                // GPU Kullanımı (Not: GPU counter dinamik olarak ayarlanmalı)
                float gpuKullanimi = 0; // gpuCounter.NextValue(); // Şu an için sıfır, dinamik ayar gerekecek
                arsivVerileri.ekranKartiVerileri.Add((zaman, gpuKullanimi));

                // Güç Kullanımı (Simüle, çünkü direkt ölçüm için donanım erişimi gerek)
                double gucKullanimi = new Random().Next(50, 300); // Gerçek ölçüm için ek kütüphane gerekir
                arsivVerileri.gucVerileri.Add((zaman, gucKullanimi));

                // Eski verileri temizle (1 haftadan eski)
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