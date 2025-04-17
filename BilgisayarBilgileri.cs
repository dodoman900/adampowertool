using System;
using System.Diagnostics;
using System.Text;

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