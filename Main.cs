using System;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class HataYoneticisi
    {
        public static class HataMesajlari
        {
            public const string VeriAlmaHatasi = "Veri alýnýrken bir hata oluþtu.";
            public const string GrafikCizimHatasi = "Grafik çizimi sýrasýnda bir hata oluþtu.";
        }

        public static void HataEleAl(Exception? hata, string mesaj)
        {
            MessageBox.Show($"{mesaj}\nHata: {hata?.Message ?? "Bilinmeyen hata"}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}