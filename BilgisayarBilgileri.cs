using System;
using System.Collections.Generic;
using System.Text;

namespace AdamPowerTool
{
    public static class BilgisayarBilgileri
    {
        public static string GetBilgi()
        {
            StringBuilder sb = new();
            sb.AppendLine("Model: SDHC SCSI Disk Device");
            sb.AppendLine("Kapasite: 7 GB");
            return sb.ToString();
        }

        public static SistemVerileri GetSystemData(TimeSpan zamanAraligi)
        {
            var veriler = new SistemVerileri();
            var rastgele = new Random();
            DateTime simdi = DateTime.Now;

            // Son 5 dakikalık test verileri üret (zaman damgaları düzenli)
            int veriSayisi = (int)(zamanAraligi.TotalSeconds / 2); // Her 2 saniyede bir veri
            for (int i = 0; i < veriSayisi; i++)
            {
                DateTime zaman = simdi.AddSeconds(-2 * i);
                if (zaman < DateTime.MinValue.AddDays(1)) break; // Tarih sınırı kontrolü
                veriler.islemciVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 90 + 10)))); // %10-90
                veriler.ramVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 60 + 20)))); // %20-80
                veriler.diskVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 50)))); // %0-50
                veriler.ekranKartiVerileri.Add((zaman, Math.Min(100, Math.Max(0, rastgele.NextDouble() * 65 + 5)))); // %5-70
                veriler.gucVerileri.Add((zaman, Math.Min(300, Math.Max(50, rastgele.NextDouble() * 200 + 50)))); // 50-250 Watt
            }

            veriler.islemciSicakligi = Math.Min(70, Math.Max(40, rastgele.NextDouble() * 30 + 40)); // 40-70 °C
            veriler.ekranKartiSicakligi = Math.Min(80, Math.Max(50, rastgele.NextDouble() * 30 + 50)); // 50-80 °C

            return veriler;
        }
    }
}