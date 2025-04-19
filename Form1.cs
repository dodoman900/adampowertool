using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace AdamPowerTool
{
    public partial class Form1 : MetroForm
    {
        private readonly MetroButton dugmeSistem = new();
        private readonly MetroButton dugmeVeriIzleme = new();
        private readonly MetroButton dugmeOptimizasyon = new();
        private readonly MetroButton dugmeAyarlar = new();
        private readonly MetroButton dugmeYardim = new();
        private readonly ToolTip aciklamaAraci = new();
        private readonly DataGridView sistemBilgiTablosu = new();
        private readonly SystemGraph sistemGrafik = new();
        private readonly SystemMonitor sistemMonitor;
        private readonly MetroPanel optimizasyonPaneli = new();
        private readonly MetroPanel ayarlarPaneli = new();
        private readonly MetroCheckBox otomatikAcilisKutusu = new();
        private readonly MetroCheckBox arkaplandaCalisKutusu = new();
        private readonly MetroButton uykuModuButonu = new();
        private readonly MetroButton optimizeEtButonu = new();
        private readonly MetroButton verimlilikModuButonu = new();
        private readonly MetroButton maksTasarrufButonu = new();
        private readonly MetroButton maksPerformansButonu = new();
        private readonly MetroButton dengeliModButonu = new();
        private readonly MetroLabel optimizasyonDurumEtiketi = new();
        private string seciliMod = "Dengeli";
        private bool arkaplandaCalisiyor = false;
        private readonly NotifyIcon bildirimSimge = new();
        private readonly System.Windows.Forms.Timer verimlilikZamanlayici = new();

        public Form1()
        {
            sistemMonitor = new SystemMonitor();
            StyleManager = new MetroFramework.Components.MetroStyleManager
            {
                Theme = MetroFramework.MetroThemeStyle.Dark
            };
            BilesenleriBaslat();
            SistemBilgiTablosunuDoldur();
            sistemMonitor.GuncellemeleriKur();
            DugmeSistem_Tikla(null, EventArgs.Empty);
            bildirimSimge.Icon = SystemIcons.Application;
            bildirimSimge.Visible = true;
            bildirimSimge.DoubleClick += (s, e) => { Show(); WindowState = FormWindowState.Normal; };
            verimlilikZamanlayici.Interval = 30000; // 30 saniyede bir kontrol
            verimlilikZamanlayici.Tick += VerimlilikKontrol;
        }

        private void BilesenleriBaslat()
        {
            SuspendLayout();

            // Form Ayarlar�
            Text = "Adam'�n G�� Y�netim Arac�";
            Size = new Size(800, 700);
            StyleManager.Style = MetroFramework.MetroColorStyle.Red;

            // Butonlar
            dugmeSistem.Location = new Point(10, 10);
            dugmeSistem.Size = new Size(120, 40);
            dugmeSistem.Text = "Sistem";
            dugmeSistem.Click += DugmeSistem_Tikla;
            dugmeSistem.MouseHover += (s, e) => aciklamaAraci.Show("Bilgisayar�n�z�n donan�m bilgilerini g�sterir.", dugmeSistem);
            dugmeSistem.MouseLeave += (s, e) => aciklamaAraci.Hide(dugmeSistem);

            dugmeVeriIzleme.Location = new Point(140, 10);
            dugmeVeriIzleme.Size = new Size(120, 40);
            dugmeVeriIzleme.Text = "Veri �zleme";
            dugmeVeriIzleme.Click += DugmeVeriIzleme_Tikla;
            dugmeVeriIzleme.MouseHover += (s, e) => aciklamaAraci.Show("CPU, RAM, Disk, GPU ve g�� kullan�m�n� grafiklerle g�sterir.", dugmeVeriIzleme);
            dugmeVeriIzleme.MouseLeave += (s, e) => aciklamaAraci.Hide(dugmeVeriIzleme);

            dugmeOptimizasyon.Location = new Point(270, 10);
            dugmeOptimizasyon.Size = new Size(120, 40);
            dugmeOptimizasyon.Text = "Optimizasyon";
            dugmeOptimizasyon.Click += DugmeOptimizasyon_Tikla;
            dugmeOptimizasyon.MouseHover += (s, e) => aciklamaAraci.Show("Sistem performans�n� optimize eder ve g�� tasarrufu sa�lar.", dugmeOptimizasyon);
            dugmeOptimizasyon.MouseLeave += (s, e) => aciklamaAraci.Hide(dugmeOptimizasyon);

            dugmeAyarlar.Location = new Point(400, 10);
            dugmeAyarlar.Size = new Size(120, 40);
            dugmeAyarlar.Text = "Ayarlar";
            dugmeAyarlar.Click += DugmeAyarlar_Tikla;
            dugmeAyarlar.MouseHover += (s, e) => aciklamaAraci.Show("Program ayarlar�n� yap�land�r�r.", dugmeAyarlar);
            dugmeAyarlar.MouseLeave += (s, e) => aciklamaAraci.Hide(dugmeAyarlar);

            dugmeYardim.Location = new Point(530, 10);
            dugmeYardim.Size = new Size(120, 40);
            dugmeYardim.Text = "Yard�m";
            dugmeYardim.Click += DugmeYardim_Tikla;
            dugmeYardim.MouseHover += (s, e) => aciklamaAraci.Show("Program�n kullan�m� hakk�nda bilgi verir.", dugmeYardim);
            dugmeYardim.MouseLeave += (s, e) => aciklamaAraci.Hide(dugmeYardim);

            // Sistem Bilgi Tablosu
            sistemBilgiTablosu.Location = new Point(10, 60);
            sistemBilgiTablosu.Size = new Size(760, 600);
            sistemBilgiTablosu.BackgroundColor = Color.FromArgb(27, 27, 27);
            sistemBilgiTablosu.ForeColor = Color.White;
            sistemBilgiTablosu.ColumnHeadersVisible = false;
            sistemBilgiTablosu.RowHeadersVisible = false;
            sistemBilgiTablosu.ReadOnly = true;
            sistemBilgiTablosu.AllowUserToAddRows = false;
            sistemBilgiTablosu.AllowUserToDeleteRows = false;
            sistemBilgiTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sistemBilgiTablosu.DefaultCellStyle.BackColor = Color.FromArgb(27, 27, 27);
            sistemBilgiTablosu.DefaultCellStyle.ForeColor = Color.White;
            sistemBilgiTablosu.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            sistemBilgiTablosu.ScrollBars = ScrollBars.Vertical;

            // Sistem Grafik
            sistemGrafik.Location = new Point(10, 60);
            sistemGrafik.Size = new Size(760, 500);

            // Optimizasyon Paneli
            optimizasyonPaneli.Location = new Point(10, 60);
            optimizasyonPaneli.Size = new Size(760, 600);

            uykuModuButonu.Location = new Point(10, 10);
            uykuModuButonu.Size = new Size(120, 40);
            uykuModuButonu.Text = "Uyku Modu";
            uykuModuButonu.Click += UykuModuButonu_Tikla;
            uykuModuButonu.MouseHover += (s, e) => aciklamaAraci.Show("Bilgisayar� uyku moduna ge�irir.", uykuModuButonu);
            uykuModuButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(uykuModuButonu);

            optimizeEtButonu.Location = new Point(10, 60);
            optimizeEtButonu.Size = new Size(120, 40);
            optimizeEtButonu.Text = "Optimize Et";
            optimizeEtButonu.Click += OptimizeEtButonu_Tikla;
            optimizeEtButonu.MouseHover += (s, e) => aciklamaAraci.Show("Sistemi se�ili moda g�re optimize eder.", optimizeEtButonu);
            optimizeEtButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(optimizeEtButonu);

            maksTasarrufButonu.Location = new Point(140, 60);
            maksTasarrufButonu.Size = new Size(120, 40);
            maksTasarrufButonu.Text = "Maks Tasarruf";
            maksTasarrufButonu.Click += (s, e) => ModSec("MaksTasarruf");
            maksTasarrufButonu.MouseHover += (s, e) => aciklamaAraci.Show("Maksimum g�� tasarrufu sa�lar.", maksTasarrufButonu);
            maksTasarrufButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(maksTasarrufButonu);

            maksPerformansButonu.Location = new Point(270, 60);
            maksPerformansButonu.Size = new Size(120, 40);
            maksPerformansButonu.Text = "Maks Performans";
            maksPerformansButonu.Click += (s, e) => ModSec("MaksPerformans");
            maksPerformansButonu.MouseHover += (s, e) => aciklamaAraci.Show("Maksimum performans i�in ayar yapar.", maksPerformansButonu);
            maksPerformansButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(maksPerformansButonu);

            dengeliModButonu.Location = new Point(400, 60);
            dengeliModButonu.Size = new Size(120, 40);
            dengeliModButonu.Text = "Dengeli";
            dengeliModButonu.Click += (s, e) => ModSec("Dengeli");
            dengeliModButonu.MouseHover += (s, e) => aciklamaAraci.Show("Performans ve tasarruf aras�nda denge kurar.", dengeliModButonu);
            dengeliModButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(dengeliModButonu);
            ModSec("Dengeli"); // Varsay�lan mod

            verimlilikModuButonu.Location = new Point(10, 110);
            verimlilikModuButonu.Size = new Size(120, 40);
            verimlilikModuButonu.Text = "Verimlilik Modu";
            verimlilikModuButonu.Click += VerimlilikModuButonu_Tikla;
            verimlilikModuButonu.MouseHover += (s, e) => aciklamaAraci.Show("30 dakika kullan�lmayan uygulamalar� kapat�r.", verimlilikModuButonu);
            verimlilikModuButonu.MouseLeave += (s, e) => aciklamaAraci.Hide(verimlilikModuButonu);

            optimizasyonDurumEtiketi.Location = new Point(140, 110);
            optimizasyonDurumEtiketi.Size = new Size(200, 30);
            optimizasyonDurumEtiketi.Text = "Durum: Beklemede";

            optimizasyonPaneli.Controls.AddRange(new Control[] { uykuModuButonu, optimizeEtButonu, maksTasarrufButonu, maksPerformansButonu, dengeliModButonu, verimlilikModuButonu, optimizasyonDurumEtiketi });

            // Ayarlar Paneli
            ayarlarPaneli.Location = new Point(10, 60);
            ayarlarPaneli.Size = new Size(760, 600);

            otomatikAcilisKutusu.Location = new Point(10, 10);
            otomatikAcilisKutusu.Size = new Size(200, 30);
            otomatikAcilisKutusu.Text = "Ba�lang��ta Otomatik A�";
            otomatikAcilisKutusu.CheckedChanged += OtomatikAcilisKutusu_CheckedChanged;

            arkaplandaCalisKutusu.Location = new Point(10, 50);
            arkaplandaCalisKutusu.Size = new Size(200, 30);
            arkaplandaCalisKutusu.Text = "Arkaplanda �al��maya �zin Ver";
            arkaplandaCalisKutusu.CheckedChanged += (s, e) => arkaplandaCalisiyor = arkaplandaCalisKutusu.Checked;

            ayarlarPaneli.Controls.AddRange(new Control[] { otomatikAcilisKutusu, arkaplandaCalisKutusu });

            // Form'a Kontrolleri Ekle
            Controls.AddRange(new Control[] { sistemBilgiTablosu, sistemGrafik, optimizasyonPaneli, ayarlarPaneli, dugmeSistem, dugmeVeriIzleme, dugmeOptimizasyon, dugmeAyarlar, dugmeYardim });
            FormClosing += FormuKapatirken;
            ResumeLayout(false);
        }

        private void SistemBilgiTablosunuDoldur()
        {
            sistemBilgiTablosu.Columns.Add("Bilgi", "Sistem Bilgisi");
            sistemBilgiTablosu.Columns["Bilgi"].Width = 740;

            var bilgi = BilgisayarBilgileri.GetBilgi();
            if (!string.IsNullOrEmpty(bilgi))
            {
                foreach (var satir in bilgi.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (!string.IsNullOrWhiteSpace(satir))
                        sistemBilgiTablosu.Rows.Add(satir);
                }
            }
        }

        private void DugmeSistem_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = true;
            sistemGrafik.Visible = false;
            optimizasyonPaneli.Visible = false;
            ayarlarPaneli.Visible = false;
            sistemGrafik.GuncellemeyiDurdur();
        }

        private void DugmeVeriIzleme_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = true;
            optimizasyonPaneli.Visible = false;
            ayarlarPaneli.Visible = false;
            sistemGrafik.GuncellemeyiBaslat();
        }

        private void DugmeOptimizasyon_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = false;
            optimizasyonPaneli.Visible = true;
            ayarlarPaneli.Visible = false;
            sistemGrafik.GuncellemeyiDurdur();
        }

        private void DugmeAyarlar_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = false;
            optimizasyonPaneli.Visible = false;
            ayarlarPaneli.Visible = true;
            sistemGrafik.GuncellemeyiDurdur();
        }

        private void DugmeYardim_Tikla(object? sender, EventArgs e)
        {
            string yardimMetni = "Adam'�n G�� Y�netim Arac� Kullan�m K�lavuzu\n\n" +
                                "1. Sistem Sekmesi: Bilgisayar�n�z�n donan�m bilgilerini detayl� olarak g�sterir.\n" +
                                "2. Veri �zleme Sekmesi: CPU, RAM, Disk, GPU ve g�� kullan�m�n� grafiklerle izlemenizi sa�lar.\n" +
                                "3. Optimizasyon Sekmesi:\n" +
                                "   - Uyku Modu: Bilgisayar� uyku moduna ge�irir.\n" +
                                "   - Optimize Et: Sisteminizi se�ili moda g�re optimize eder.\n" +
                                "     - Maks Tasarruf: G�� t�ketimini en aza indirir.\n" +
                                "     - Maks Performans: T�m kaynaklar� performansa y�nlendirir.\n" +
                                "     - Dengeli: Performans ve tasarruf aras�nda denge kurar.\n" +
                                "   - Verimlilik Modu: 30 dakika kullan�lmayan uygulamalar� kapat�r.\n" +
                                "4. Ayarlar Sekmesi:\n" +
                                "   - Ba�lang��ta Otomatik A�: Program�n Windows a��l���nda �al��mas�n� sa�lar.\n" +
                                "   - Arkaplanda �al��maya �zin Ver: Program kapat�ld���nda arkaplanda �al��maya devam eder.";
            MessageBox.Show(yardimMetni, "Yard�m ve Hakk�nda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UykuModuButonu_Tikla(object? sender, EventArgs e)
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        private void OptimizeEtButonu_Tikla(object? sender, EventArgs e)
        {
            optimizasyonDurumEtiketi.Text = "Durum: Optimize Ediliyor...";
            switch (seciliMod)
            {
                case "MaksTasarruf":
                    Process.Start("powercfg", "/setactive 381b4222-f694-41f0-9685-ff5bb260df2e");
                    Process.Start("cmd", "/c taskkill /F /IM * /FI \"STATUS eq RUNNING\" /FI \"IMAGENAME ne explorer.exe\" /FI \"IMAGENAME ne svchost.exe\"");
                    break;
                case "MaksPerformans":
                    Process.Start("powercfg", "/setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
                    break;
                case "Dengeli":
                    Process.Start("powercfg", "/setactive a1841308-3541-4fab-bc81-f71556f20b4a");
                    Process.Start("cmd", "/c taskkill /F /IM * /FI \"STATUS eq RUNNING\" /FI \"IMAGENAME ne explorer.exe\" /FI \"IMAGENAME ne svchost.exe\"");
                    break;
            }
            optimizasyonDurumEtiketi.Text = "Durum: Optimizasyon Tamamland�";
        }

        private void VerimlilikModuButonu_Tikla(object? sender, EventArgs e)
        {
            optimizasyonDurumEtiketi.Text = "Durum: Verimlilik Modu Aktif";
            verimlilikZamanlayici.Start();
        }

        private void VerimlilikKontrol(object? sender, EventArgs e)
        {
            try
            {
                Process.Start("cmd", "/c taskkill /F /IM * /FI \"STATUS eq SUSPENDED\" /FI \"IMAGENAME ne explorer.exe\" /FI \"IMAGENAME ne svchost.exe\"");
            }
            catch (Exception ex)
            {
                HataYoneticisi.HataEleAl(ex, "Verimlilik kontrol� ba�ar�s�z.");
            }
        }

        private void ModSec(string mod)
        {
            seciliMod = mod;
            maksTasarrufButonu.Style = mod == "MaksTasarruf" ? MetroFramework.MetroColorStyle.Green : MetroFramework.MetroColorStyle.Default;
            maksPerformansButonu.Style = mod == "MaksPerformans" ? MetroFramework.MetroColorStyle.Green : MetroFramework.MetroColorStyle.Default;
            dengeliModButonu.Style = mod == "Dengeli" ? MetroFramework.MetroColorStyle.Green : MetroFramework.MetroColorStyle.Default;
        }

        private void OtomatikAcilisKutusu_CheckedChanged(object? sender, EventArgs e)
        {
            string appPath = Application.ExecutablePath;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (otomatikAcilisKutusu.Checked)
                rk.SetValue("AdamPowerTool", appPath);
            else
                rk.DeleteValue("AdamPowerTool", false);
        }

        private void FormuKapatirken(object? sender, FormClosingEventArgs e)
        {
            if (arkaplandaCalisiyor)
            {
                e.Cancel = true;
                Hide();
                bildirimSimge.ShowBalloonTip(3000, "Bilgi", "Program arkaplanda �al���yor.", ToolTipIcon.Info);
            }
            else
            {
                sistemGrafik.GuncellemeyiDurdur();
                sistemMonitor.Kaydet();
            }
        }
    }
}