using System;
using System.Management;
using System.Text;

namespace AdamPowerTool
{
    public static class BilgisayarBilgileri
    {
        public static string GetBilgi()
        {
            StringBuilder sb = new();
            try
            {
                // İşlemci Bilgisi
                using var cpuArama = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (var obj in cpuArama.Get())
                {
                    sb.AppendLine($"İşlemci: {obj["Name"]}");
                    sb.AppendLine($"Çekirdek Sayısı: {obj["NumberOfCores"]}");
                    sb.AppendLine($"Maksimum Hız: {obj["MaxClockSpeed"]} MHz");
                }

                // RAM Bilgisi
                using var ramArama = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                long toplamRam = 0;
                foreach (var obj in ramArama.Get())
                {
                    toplamRam += Convert.ToInt64(obj["Capacity"]);
                    sb.AppendLine($"RAM Modülü: {obj["Manufacturer"]} {obj["Speed"]} MHz");
                }
                double ramGB = toplamRam / (1024.0 * 1024 * 1024);
                sb.AppendLine($"Toplam RAM: {ramGB:F1} GB");

                // Depolama Bilgisi
                using var diskArama = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (var obj in diskArama.Get())
                {
                    double diskBytes = Convert.ToDouble(obj["Size"]);
                    double diskGB = diskBytes / (1024 * 1024 * 1024);
                    sb.AppendLine($"Depolama: {obj["Model"]} ({diskGB:F1} GB)");
                }

                // Ekran Kartı Bilgisi
                using var gpuArama = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (var obj in gpuArama.Get())
                {
                    sb.AppendLine($"Ekran Kartı: {obj["Name"]}");
                    sb.AppendLine($"VRAM: {Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
                }

                // İşletim Sistemi Bilgisi
                using var osArama = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (var obj in osArama.Get())
                {
                    sb.AppendLine($"İşletim Sistemi: {obj["Caption"]} ({obj["OSArchitecture"]})");
                    sb.AppendLine($"Sürüm: {obj["Version"]}");
                }

                // Anakart Bilgisi
                using var anakartArama = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (var obj in anakartArama.Get())
                {
                    sb.AppendLine($"Anakart: {obj["Manufacturer"]} {obj["Product"]}");
                }

                // BIOS Bilgisi
                using var biosArama = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (var obj in biosArama.Get())
                {
                    sb.AppendLine($"BIOS: {obj["Manufacturer"]} {obj["SMBIOSBIOSVersion"]}");
                }
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "Sistem bilgileri alınamadı.");
                sb.AppendLine("Sistem bilgileri alınamadı.");
            }

            return sb.ToString();
        }

        public static SistemVerileri GetSystemData(TimeSpan zamanAraligi)
        {
            return new SistemVerileri();
        }
    }
}