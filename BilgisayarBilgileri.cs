using System;
using System.Text;

namespace AdamPowerTool
{
    public static class BilgisayarBilgileri
    {
        public static string GetBilgi()
        {
            StringBuilder sb = new();
            sb.AppendLine("İşlemci: Intel Core i7-12700H");
            sb.AppendLine("RAM: 16 GB DDR4");
            sb.AppendLine("Depolama: 512 GB NVMe SSD");
            sb.AppendLine("Ekran Kartı: NVIDIA GeForce RTX 3060");
            sb.AppendLine("İşletim Sistemi: Windows 11 Pro");
            return sb.ToString();
        }

        public static SistemVerileri GetSystemData(TimeSpan zamanAraligi)
        {
            var veriler = new SistemVerileri();
            var rastgele = new Random();
            DateTime simdi = DateTime.Now;
            int veriSayisi = (int)(zamanAraligi.TotalSeconds / 2); // Her 2 saniyede veri

            for (int i = 0; i < veriSayisi; i++)
            {
                DateTime zaman = simdi.AddSeconds(-2 * i);
                if (zaman < DateTime.MinValue.AddDays(1)) break;
                veriler.islemciVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 90 + 10))));
                veriler.ramVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 60 + 20))));
                veriler.diskVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 50))));
                veriler.ekranKartiVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 65 + 5))));
                veriler.gucVerileri.Add((zaman, Math.Min(300, Math.Max(50, rastgele.NextDouble() * 200 + 50))));
            }

            veriler.islemciSicakligi = Math.Min(70, Math.Max(40, rastgele.NextDouble() * 30 + 40));
            veriler.ekranKartiSicakligi = Math.Min(80, Math.Max(50, rastgele.NextDouble() * 30 + 50));

            return veriler;
        }
    }
}