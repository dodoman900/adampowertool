using System;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class HataYoneticisi
    {
        public enum HataMesajlari
        {
            VeriAlmaHatasi,
            VeriKaydetmeHatasi,
            VeriYuklemeHatasi
        }

        public static void HataEleAl(Exception ex, HataMesajlari mesaj)
        {
            string hataMesaji = mesaj switch
            {
                HataMesajlari.VeriAlmaHatasi => $"Veri alma hatası: {ex.Message}",
                HataMesajlari.VeriKaydetmeHatasi => $"Veri kaydetme hatası: {ex.Message}",
                HataMesajlari.VeriYuklemeHatasi => $"Veri yükleme hatası: {ex.Message}",
                _ => $"Bilinmeyen hata: {ex.Message}"
            };

            MessageBox.Show(hataMesaji, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}