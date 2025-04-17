using System;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class HataYoneticisi
    {
        public static class HataMesajlari
        {
            public const string VeriAlmaHatasi = "Veri alınırken bir hata oluştu.";
            public const string GrafikCizimHatasi = "Grafik çizimi sırasında bir hata oluştu.";
        }

        public static void HataEleAl(Exception? hata, string mesaj)
        {
            MessageBox.Show($"{mesaj}\nHata: {hata?.Message ?? "Bilinmeyen hata"}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}