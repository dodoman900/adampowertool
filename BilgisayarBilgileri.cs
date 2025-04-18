using System;
using System.Diagnostics;
using System.Text;
using System.Management;

namespace AdamPowerTool
{
    public static class BilgisayarBilgileri
    {
        private static readonly Random random = new Random();

        public static object? GetSystemData(TimeSpan zamanAraligi)
        {
            try
            {
                var sistemVerileri = new SistemVerileri();
                DateTime bitisZamani = DateTime.Now;
                DateTime baslangicZamani = bitisZamani - zamanAraligi;

                for (DateTime zaman = baslangicZamani; zaman <= bitisZamani; zaman = zaman.AddSeconds(10))
                {
                    sistemVerileri.islemciVerileri.Add((zaman, random.NextDouble() * 100));
                    sistemVerileri.ramVerileri.Add((zaman, random.NextDouble() * 100));
                    sistemVerileri.diskVerileri.Add((zaman, random.NextDouble() * 100));
                    sistemVerileri.ekranKartiVerileri.Add((zaman, random.NextDouble() * 100));
                    sistemVerileri.gucVerileri.Add((zaman, random.NextDouble() * 300));
                }

                sistemVerileri.islemciSicakligi = random.NextDouble() * 80;
                sistemVerileri.ekranKartiSicakligi = random.NextDouble() * 80;

                return sistemVerileri;
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
                return null;
            }
        }

        public static string? GetBilgi()
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Sistem Bilgileri:");
                sb.AppendLine($"İşletim Sistemi: {Environment.OSVersion}");
                sb.AppendLine($"Bilgisayar Adı: {Environment.MachineName}");
                sb.AppendLine($"Kullanıcı Adı: {Environment.UserName}");
                sb.AppendLine($"İşlemci Sayısı: {Environment.ProcessorCount}");

                // İşlemci detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        sb.AppendLine($"İşlemci: {obj["Name"]}");
                        sb.AppendLine($"Çekirdek Sayısı: {obj["NumberOfCores"]}");
                        sb.AppendLine($"Mantıksal İşlemci Sayısı: {obj["NumberOfLogicalProcessors"]}");
                        sb.AppendLine($"Maksimum Saat Hızı: {obj["MaxClockSpeed"]} MHz");
                    }
                }

                // Bellek detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                {
                    long toplamBellek = 0;
                    foreach (var obj in searcher.Get())
                    {
                        toplamBellek += Convert.ToInt64(obj["Capacity"]);
                    }
                    sb.AppendLine($"Toplam RAM: {toplamBellek / (1024 * 1024 * 1024)} GB");
                }

                // Disk detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        sb.AppendLine($"Disk: {obj["Model"]}");
                        sb.AppendLine($"Kapasite: {Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                    }
                }

                // Ekran kartı detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        sb.AppendLine($"Ekran Kartı: {obj["Name"]}");
                        sb.AppendLine($"Bellek: {Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
                    }
                }

                // Anakart detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        sb.AppendLine($"Anakart: {obj["Manufacturer"]} {obj["Product"]}");
                    }
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, HataYoneticisi.HataMesajlari.VeriAlmaHatasi);
                return null;
            }
        }
    }
}