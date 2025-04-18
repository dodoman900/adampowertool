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
                }

                // RAM Bilgisi (Toplam Fiziksel Bellek)
                using var ramArama = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                foreach (var obj in ramArama.Get())
                {
                    double ramBytes = Convert.ToDouble(obj["TotalPhysicalMemory"]);
                    double ramGB = ramBytes / (1024 * 1024 * 1024);
                    sb.AppendLine($"RAM: {ramGB:F1} GB");
                }

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
                }

                // İşletim Sistemi Bilgisi
                using var osArama = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (var obj in osArama.Get())
                {
                    sb.AppendLine($"İşletim Sistemi: {obj["Caption"]} ({obj["OSArchitecture"]})");
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
            var veriler = new SistemVerileri();
            return veriler; // SystemMonitor bu verileri PerformanceCounter ile dolduracak
        }
    }
}