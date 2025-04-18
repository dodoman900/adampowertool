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

                // Daha gerçekçi sıcaklık değerleri (CPU: 30-80°C, GPU: 30-75°C)
                sistemVerileri.islemciSicakligi = 30 + random.NextDouble() * 50; // 30-80°C
                sistemVerileri.ekranKartiSicakligi = 30 + random.NextDouble() * 45; // 30-75°C

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
                sb.AppendLine($"Sistem Başlangıç Süresi: {DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount)}");
                sb.AppendLine($"64-Bit İşletim Sistemi: {Environment.Is64BitOperatingSystem}");

                // İşlemci detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        sb.AppendLine($"İşlemci: {obj["Name"]}");
                        sb.AppendLine($"Üretici: {obj["Manufacturer"]}");
                        sb.AppendLine($"Çekirdek Sayısı: {obj["NumberOfCores"]}");
                        sb.AppendLine($"Mantıksal İşlemci Sayısı: {obj["NumberOfLogicalProcessors"]}");
                        sb.AppendLine($"Maksimum Saat Hızı: {obj["MaxClockSpeed"]} MHz");
                        sb.AppendLine($"L2 Önbellek: {Convert.ToInt64(obj["L2CacheSize"]) / 1024} MB");
                        sb.AppendLine($"L3 Önbellek: {Convert.ToInt64(obj["L3CacheSize"]) / 1024} MB");
                    }
                }

                // Bellek detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                {
                    long toplamBellek = 0;
                    int modulSayisi = 0;
                    foreach (var obj in searcher.Get())
                    {
                        toplamBellek += Convert.ToInt64(obj["Capacity"]);
                        modulSayisi++;
                        sb.AppendLine($"RAM Modülü #{modulSayisi}:");
                        sb.AppendLine($"  Kapasite: {Convert.ToInt64(obj["Capacity"]) / (1024 * 1024 * 1024)} GB");
                        sb.AppendLine($"  Hız: {obj["Speed"]} MHz");
                        sb.AppendLine($"  Üretici: {obj["Manufacturer"]}");
                    }
                    sb.AppendLine($"Toplam RAM: {toplamBellek / (1024 * 1024 * 1024)} GB");
                }

                // Disk detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
                {
                    int diskSayisi = 0;
                    foreach (var obj in searcher.Get())
                    {
                        diskSayisi++;
                        sb.AppendLine($"Disk #{diskSayisi}:");
                        sb.AppendLine($"  Model: {obj["Model"]}");
                        sb.AppendLine($"  Kapasite: {Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                        sb.AppendLine($"  Tür: {obj["MediaType"]}");
                    }
                }

                // Ekran kartı detayları
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
                {
                    int gpuSayisi = 0;
                    foreach (var obj in searcher.Get())
                    {
                        gpuSayisi++;
                        sb.AppendLine($"Ekran Kartı #{gpuSayisi}:");
                        sb.AppendLine($"  Model: {obj["Name"]}");
                        sb.AppendLine($"  Bellek: {Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
                        sb.AppendLine($"  Sürücü Sürümü: {obj["DriverVersion"]}");
                        sb.AppendLine($"  Çözünürlük: {obj["CurrentHorizontalResolution"]}x{tegrated or discrete?");
                        sb.AppendLine($"  Tür: {(obj["AdapterDACType"].ToString().Contains("Integrated") ? "Tümleşik" : "Ayrık")}");
                    }
                }

                    // Anakart detayları
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
                    {
                        foreach (var obj in searcher.Get())
                        {
                            sb.AppendLine($"Anakart: {obj["Manufacturer"]} {obj["Product"]}");
                            sb.AppendLine($"Seri Numarası: {obj["SerialNumber"]}");
                        }
                    }

                    // BIOS detayları
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
                    {
                        foreach (var obj in searcher.Get())
                        {
                            sb.AppendLine($"BIOS: {obj["Manufacturer"]} {obj["SMBIOSBIOSVersion"]}");
                            sb.AppendLine($"Yayın Tarihi: {obj["ReleaseDate"]}");
                        }
                    }

                    // Ağ bağdaştırıcıları
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE PhysicalAdapter = True"))
                    {
                        int agSayisi = 0;
                        foreach (var obj in searcher.Get())
                        {
                            agSayisi++;
                            sb.AppendLine($"Ağ Bağdaştırıcısı #{agSayisi}:");
                            sb.AppendLine($"  Ad: {obj["Name"]}");
                            sb.AppendLine($"  Tür: {obj["AdapterType"]}");
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