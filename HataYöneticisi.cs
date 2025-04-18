using System;
using System.IO;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class HataYoneticisi
    {
        private static string SonHataDosyasi => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SonHataRaporu.txt");

        public static void HataEleAl(Exception ex, string hataMesaji)
        {
            string tamMesaj = $"Hata: {hataMesaji}\nDetay: {ex.Message}\nStack Trace: {ex.StackTrace}\nZaman: {DateTime.Now}";
            MessageBox.Show(tamMesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Hata raporunu dosyaya kaydet
            File.WriteAllText(SonHataDosyasi, tamMesaj);

            // Kullanıcıya e-posta gönderme seçeneği sun
            var sonuc = MessageBox.Show("Hata raporunu geliştiriciye e-posta ile göndermek ister misiniz?", "Hata Raporu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    string mailto = $"mailto:azizcankiri18@hotmail.com?subject=Hata%20Raporu&body={Uri.EscapeDataString(tamMesaj)}";
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(mailto) { UseShellExecute = true });
                }
                catch (Exception mailEx)
                {
                    MessageBox.Show($"E-posta açılamadı: {mailEx.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}