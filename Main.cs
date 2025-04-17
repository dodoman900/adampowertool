using System;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class HataYoneticisi
    {
        public static class HataMesajlari
        {
            public const string VeriAlmaHatasi = "Veri al�n�rken bir hata olu�tu.";
            public const string GrafikCizimHatasi = "Grafik �izimi s�ras�nda bir hata olu�tu.";
        }

        public static void HataEleAl(Exception? hata, string mesaj)
        {
            MessageBox.Show($"{mesaj}\nHata: {hata?.Message ?? "Bilinmeyen hata"}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}