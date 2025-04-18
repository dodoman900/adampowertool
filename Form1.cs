using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace AdamPowerTool
{
    public partial class Form1 : Form
    {
        private readonly System.ComponentModel.IContainer bilesenler = new System.ComponentModel.Container();
        private readonly Button dugmeSistem = new();
        private readonly Button dugmeVeriIzleme = new();
        private readonly Button dugmeOptimizasyon = new();
        private readonly Button dugmeAyarlar = new();
        private readonly Button dugmeYardim = new();
        private readonly ToolTip aciklamaAraci = new();
        private readonly DataGridView sistemBilgiTablosu = new();
        private readonly SystemGraph sistemGrafik = new();
        private readonly SystemMonitor sistemMonitor;
        private readonly Panel optimizasyonPaneli = new();
        private readonly Panel ayarlarPaneli = new();
        private readonly CheckBox otomatikAcilisKutusu = new();
        private readonly CheckBox arkaplandaCalisKutusu = new();
        private readonly Button uykuModuButonu = new();
        private readonly Button optimizeEtButonu = new();
        private readonly Button verimlilikModuButonu = new();
        private readonly Button maksTasarrufButonu = new();
        private readonly Button maksPerformansButonu = new();
        private readonly Button dengeliModButonu = new();
        private readonly Label optimizasyonDurumEtiketi = new();
        private string seciliMod = "Dengeli";
        private bool arkaplandaCalisiyor = false;
        private readonly NotifyIcon bildirimSimge = new();
        private readonly System.Windows.Forms.Timer verimlilikZamanlayici = new();

        public Form1()
        {
            sistemMonitor = new SystemMonitor();
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

            // Butonlar
            dugmeSistem.Location = new Point(10, 10);
            dugmeSistem.Size = new Size(80, 30);
            dugmeSistem.Text = "Sistem";
            dugmeSistem.BackColor = Color.FromArgb(46, 46, 46);
            dugmeSistem.ForeColor = Color.FromArgb(255, 0, 0);
            dugmeSistem.FlatStyle = FlatStyle.Flat;
            dugmeSistem.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugmeSistem.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugmeSistem.Click += DugmeSistem_Tikla;
            dugmeSistem.MouseEnter += Button_MouseEnter;
            dugmeSistem.MouseLeave += Button_MouseLeave;
            aciklamaAraci.SetToolTip(dugmeSistem, "Bilgisayarýnýzýn donaným bilgilerini gösterir.");

            dugmeVeriIzleme.Location = new Point(100, 10);
            dugmeVeriIzleme.Size = new Size(80, 30);
            dugmeVeriIzleme.Text = "Veri Ýzleme";
            dugmeVeriIzleme.BackColor = Color.FromArgb(46, 46, 46);
            dugmeVeriIzleme.ForeColor = Color.FromArgb(255, 0, 0);
            dugmeVeriIzleme.FlatStyle = FlatStyle.Flat;
            dugmeVeriIzleme.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugmeVeriIzleme.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugmeVeriIzleme.Click += DugmeVeriIzleme_Tikla;
            dugmeVeriIzleme.MouseEnter += Button_MouseEnter;
            dugmeVeriIzleme.MouseLeave += Button_MouseLeave;
            aciklamaAraci.SetToolTip(dugmeVeriIzleme, "CPU, RAM, Disk, GPU ve güç kullanýmýný grafiklerle gösterir.");

            dugmeOptimizasyon.Location = new Point(190, 10);
            dugmeOptimizasyon.Size = new Size(80, 30);
            dugmeOptimizasyon.Text = "Optimizasyon";
            dugmeOptimizasyon.BackColor = Color.FromArgb(46, 46, 46);
            dugmeOptimizasyon.ForeColor = Color.FromArgb(255, 0, 0);
            dugmeOptimizasyon.FlatStyle = FlatStyle.Flat;
            dugmeOptimizasyon.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugmeOptimizasyon.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugmeOptimizasyon.Click += DugmeOptimizasyon_Tikla;
            dugmeOptimizasyon.MouseEnter += Button_MouseEnter;
            dugmeOptimizasyon.MouseLeave += Button_MouseLeave;
            aciklamaAraci.SetToolTip(dugmeOptimizasyon, "Sistem performansýný optimize eder ve güç tasarrufu saðlar.");

            dugmeAyarlar.Location = new Point(280, 10);
            dugmeAyarlar.Size = new Size(80, 30);
            dugmeAyarlar.Text = "Ayarlar";
            dugmeAyarlar.BackColor = Color.FromArgb(46, 46, 46);
            dugmeAyarlar.ForeColor = Color.FromArgb(255, 0, 0);
            dugmeAyarlar.FlatStyle = FlatStyle.Flat;
            dugmeAyarlar.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugmeAyarlar.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugmeAyarlar.Click += DugmeAyarlar_Tikla;
            dugmeAyarlar.MouseEnter += Button_MouseEnter;
            dugmeAyarlar.MouseLeave += Button_MouseLeave;
            aciklamaAraci.SetToolTip(dugmeAyarlar, "Program ayarlarýný yapýlandýrýr.");

            dugmeYardim.Location = new Point(650, 10);
            dugmeYardim.Size = new Size(80, 30);
            dugmeYardim.Text = "Yardým";
            dugmeYardim.BackColor = Color.FromArgb(46, 46, 46);
            dugmeYardim.ForeColor = Color.FromArgb(255, 0, 0);
            dugmeYardim.FlatStyle = FlatStyle.Flat;
            dugmeYardim.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugmeYardim.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugmeYardim.Click += DugmeYardim_Tikla;
            dugmeYardim.MouseEnter += Button_MouseEnter;
            dugmeYardim.MouseLeave += Button_MouseLeave;
            aciklamaAraci.SetToolTip(dugmeYardim, "Programýn kullanýmý hakkýnda bilgi verir.");

            // Sistem Bilgi Tablosu
            sistemBilgiTablosu.Location = new Point(10, 50);
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
            sistemBilgiTablosu.DefaultCellStyle.Font = new Font("Montserrat", 10F, FontStyle.Regular);
            sistemBilgiTablosu.ScrollBars = ScrollBars.Vertical;

            // Sistem Grafik
            sistemGrafik.Location = new Point(10, 50);
            sistemGrafik.Size = new Size(760, 500);

            // Optimizasyon Paneli
            optimizasyonPaneli.Location = new Point(10, 50);
            optimizasyonPaneli.Size = new Size(760, 600);
            optimizasyonPaneli.BackColor = Color.FromArgb(27, 27, 27);

            uykuModuButonu.Location = new Point(10, 10);
            uykuModuButonu.Size = new Size(100, 30);
            uykuModuButonu.Text = "Uyku Modu";
            uykuModuButonu.BackColor = Color.FromArgb(46, 46, 46);
            uykuModuButonu.ForeColor = Color.White;
            uykuModuButonu.FlatStyle = FlatStyle.Flat;
            uykuModuButonu.Click += UykuModuButonu_Tikla;
            aciklamaAraci.SetToolTip(uykuModuButonu, "Bilgisayarý uyku moduna geçirir.");

            optimizeEtButonu.Location = new Point(10, 50);
            optimizeEtButonu.Size = new Size(100, 30);
            optimizeEtButonu.Text = "Optimize Et";
            optimizeEtButonu.BackColor = Color.FromArgb(46, 46, 46);
            optimizeEtButonu.ForeColor = Color.White;
            optimizeEtButonu.FlatStyle = FlatStyle.Flat;
            optimizeEtButonu.Click += OptimizeEtButonu_Tikla;
            aciklamaAraci.SetToolTip(optimizeEtButonu, "Sistemi seçili moda göre optimize eder.");

            maksTasarrufButonu.Location = new Point(120, 50);
            maksTasarrufButonu.Size = new Size(100, 30);
            maksTasarrufButonu.Text = "Maks Tasarruf";
            maksTasarrufButonu.BackColor = Color.FromArgb(46, 46, 46);
            maksTasarrufButonu.ForeColor = Color.White;
            maksTasarrufButonu.FlatStyle = FlatStyle.Flat;
            maksTasarrufButonu.Click += (s, e) => ModSec("MaksTasarruf");
            aciklamaAraci.SetToolTip(maksTasarrufButonu, "Maksimum güç tasarrufu saðlar.");

            maksPerformansButonu.Location = new Point(230, 50);
            maksPerformansButonu.Size = new Size(100, 30);
            maksPerformansButonu.Text = "Maks Performans";
            maksPerformansButonu.BackColor = Color.FromArgb(46, 46, 46);
            maksPerformansButonu.ForeColor = Color.White;
            maksPerformansButonu.FlatStyle = FlatStyle.Flat;
            maksPerformansButonu.Click += (s, e) => ModSec("MaksPerformans");
            aciklamaAraci.SetToolTip(maksPerformansButonu, "Maksimum performans için ayar yapar.");

            dengeliModButonu.Location = new Point(340, 50);
            dengeliModButonu.Size = new Size(100, 30);
            dengeliModButonu.Text = "Dengeli";
            dengeliModButonu.BackColor = Color.FromArgb(46, 46, 46);
            dengeliModButonu.ForeColor = Color.White;
            dengeliModButonu.FlatStyle = FlatStyle.Flat;
            dengeliModButonu.Click += (s, e) => ModSec("Dengeli");
            aciklamaAraci.SetToolTip(dengeliModButonu, "Performans ve tasarruf arasýnda denge kurar.");
            ModSec("Dengeli"); // Varsayýlan mod

            verimlilikModuButonu.Location = new Point(10, 90);
            verimlilikModuButonu.Size = new Size(100, 30);
            verimlilikModuButonu.Text = "Verimlilik Modu";
            verimlilikModuButonu.BackColor = Color.FromArgb(46, 46, 46);
            verimlilikModuButonu.ForeColor = Color.White;
            verimlilikModuButonu.FlatStyle = FlatStyle.Flat;
            verimlilikModuButonu.Click += VerimlilikModuButonu_Tikla;
            aciklamaAraci.SetToolTip(verimlilikModuButonu, "30 dakika kullanýlmayan uygulamalarý kapatýr.");

            optimizasyonDurumEtiketi.Location = new Point(120, 90);
            optimizasyonDurumEtiketi.Size = new Size(200, 30);
            optimizasyonDurumEtiketi.ForeColor = Color.White;
            optimizasyonDurumEtiketi.Text = "Durum: Beklemede";

            optimizasyonPaneli.Controls.AddRange(new Control[] { uykuModuButonu, optimizeEtButonu, maksTasarrufButonu, maksPerformansButonu, dengeliModButonu, verimlilikModuButonu, optimizasyonDurumEtiketi });

            // Ayarlar Paneli
            ayarlarPaneli.Location = new Point(10, 50);
            ayarlarPaneli.Size = new Size(760, 600);
            ayarlarPaneli.BackColor = Color.FromArgb(27, 27, 27);

            otomatikAcilisKutusu.Location = new Point(10, 10);
            otomatikAcilisKutusu.Size = new Size(200, 30);
            otomatikAcilisKutusu.Text = "Baþlangýçta Otomatik Aç";
            otomatikAcilisKutusu.ForeColor = Color.White;
            otomatikAcilisKutusu.CheckedChanged += OtomatikAcilisKutusu_CheckedChanged;

            arkaplandaCalisKutusu.Location = new Point(10, 50);
            arkaplandaCalisKutusu.Size = new Size(200, 30);
            arkaplandaCalisKutusu.Text = "Arkaplanda Çalýþmaya Ýzin Ver";
            arkaplandaCalisKutusu.ForeColor = Color.White;
            arkaplandaCalisKutusu.CheckedChanged += (s, e) => arkaplandaCalisiyor = arkaplandaCalisKutusu.Checked;

            ayarlarPaneli.Controls.AddRange(new Control[] { otomatikAcilisKutusu, arkaplandaCalisKutusu });

            // Form
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 660);
            Controls.AddRange(new Control[] { sistemBilgiTablosu, sistemGrafik, optimizasyonPaneli, ayarlarPaneli, dugmeSistem, dugmeVeriIzleme, dugmeOptimizasyon, dugmeAyarlar, dugmeYardim });
            Text = "Adam'ýn GÜÇ YÖNETÝM ARACI";
            BackColor = Color.FromArgb(27, 27, 27);
            FormClosing += FormuKapatirken;
            ResumeLayout(false);
        }

        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(60, 60, 60);
            }
        }

        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(46, 46, 46);
            }
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
            string yardimMetni = "Adam'ýn Güç Yönetim Aracý Kullaným Kýlavuzu\n\n" +
                                "1. Sistem Sekmesi: Bilgisayarýnýzýn donaným bilgilerini detaylý olarak gösterir.\n" +
                                "2. Veri Ýzleme Sekmesi: CPU, RAM, Disk, GPU ve güç kullanýmýný grafiklerle izlemenizi saðlar.\n" +
                                "3. Optimizasyon Sekmesi:\n" +
                                "   - Uyku Modu: Bilgisayarý uyku moduna geçirir.\n" +
                                "   - Optimize Et: Sisteminizi seçili moda göre optimize eder.\n" +
                                "     - Maks Tasarruf: Güç tüketimini en aza indirir (ekran parlaklýðý %30, arkaplan hizmetleri kapatýlýr).\n" +
                                "     - Maks Performans: Tüm kaynaklarý performansa yönlendirir (yüksek CPU önceliði, hizmetler açýk).\n" +
                                "     - Dengeli: Performans ve tasarruf arasýnda denge kurar.\n" +
                                "   - Verimlilik Modu: 30 dakika kullanýlmayan uygulamalarý kapatýr.\n" +
                                "4. Ayarlar Sekmesi:\n" +
                                "   - Baþlangýçta Otomatik Aç: Programýn Windows açýlýþýnda çalýþmasýný saðlar.\n" +
                                "   - Arkaplanda Çalýþmaya Ýzin Ver: Program kapatýldýðýnda arkaplanda çalýþmaya devam eder.";
            MessageBox.Show(yardimMetni, "Yardým ve Hakkýnda", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Process.Start("powercfg", "/setactive 381b4222-f694-41f0-9685-ff5bb260df2e"); // Windows Güç Tasarruf Planý
                    Process.Start("cmd", "/c taskkill /F /IM * /FI \"STATUS eq RUNNING\" /FI \"IMAGENAME ne explorer.exe\" /FI \"IMAGENAME ne svchost.exe\"");
                    break;
                case "MaksPerformans":
                    Process.Start("powercfg", "/setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c"); // Windows Yüksek Performans Planý
                    break;
                case "Dengeli":
                    Process.Start("powercfg", "/setactive a1841308-3541-4fab-bc81-f71556f20b4a"); // Windows Dengeli Planý
                    Process.Start("cmd", "/c taskkill /F /IM * /FI \"STATUS eq RUNNING\" /FI \"IMAGENAME ne explorer.exe\" /FI \"IMAGENAME ne svchost.exe\"");
                    break;
            }
            optimizasyonDurumEtiketi.Text = "Durum: Optimizasyon Tamamlandý";
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
                HataYoneticisi.HataEleAl(ex, "Verimlilik kontrolü baþarýsýz.");
            }
        }

        private void ModSec(string mod)
        {
            seciliMod = mod;
            maksTasarrufButonu.FlatAppearance.BorderSize = mod == "MaksTasarruf" ? 2 : 0;
            maksPerformansButonu.FlatAppearance.BorderSize = mod == "MaksPerformans" ? 2 : 0;
            dengeliModButonu.FlatAppearance.BorderSize = mod == "Dengeli" ? 2 : 0;
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
                bildirimSimge.ShowBalloonTip(3000, "Bilgi", "Program arkaplanda çalýþýyor.", ToolTipIcon.Info);
            }
            else
            {
                sistemGrafik.GuncellemeyiDurdur();
                sistemMonitor.Kaydet();
            }
        }
    }
}