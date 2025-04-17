using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.IContainer? bilesenler = null;
        private System.Windows.Forms.TabControl? sekmeKontrolu;
        private System.Windows.Forms.TabPage? sekmeSayfasi1;
        private System.Windows.Forms.TabPage? sekmeSayfasi2;
        private System.Windows.Forms.TabPage? sekmeSayfasi3;
        private System.Windows.Forms.TabPage? sekmeSayfasi4;
        private System.Windows.Forms.Button? dugme1;
        private System.Windows.Forms.Button? dugme2;
        private System.Windows.Forms.Button? dugme3;
        private System.Windows.Forms.Button? dugme4;
        private System.Windows.Forms.ComboBox? zamanAraligiKutusu;
        private System.Windows.Forms.Label? sistemBilgiEtiketi;
        private MonitorGraph? izlemeGrafik;
        private System.Windows.Forms.DataGridView? kullanimTablosu;
        private System.Windows.Forms.Label? islemciSicaklikEtiketi;
        private System.Windows.Forms.Label? ekranKartiSicaklikEtiketi;
        private System.Windows.Forms.Label? islemciEtiketi;
        private System.Windows.Forms.Label? ekranKartiEtiketi;

        public Form1()
        {
            BilesenleriBaslat();
            ZamanAraligiKutusunuKur();
            KullanimTablosunuKur();
            SicaklikGuncellemeleriniKur();
            izlemeGrafik?.GuncellemeyiBaslat();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (bilesenler != null))
            {
                bilesenler.Dispose();
            }
            base.Dispose(disposing);
        }

        private void BilesenleriBaslat()
        {
            this.sekmeKontrolu = new System.Windows.Forms.TabControl();
            this.sekmeSayfasi1 = new System.Windows.Forms.TabPage();
            this.sistemBilgiEtiketi = new System.Windows.Forms.Label();
            this.sekmeSayfasi2 = new System.Windows.Forms.TabPage();
            this.izlemeGrafik = new AdamPowerTool.MonitorGraph();
            this.kullanimTablosu = new System.Windows.Forms.DataGridView();
            this.islemciSicaklikEtiketi = new System.Windows.Forms.Label();
            this.ekranKartiSicaklikEtiketi = new System.Windows.Forms.Label();
            this.islemciEtiketi = new System.Windows.Forms.Label();
            this.ekranKartiEtiketi = new System.Windows.Forms.Label();
            this.sekmeSayfasi3 = new System.Windows.Forms.TabPage();
            this.sekmeSayfasi4 = new System.Windows.Forms.TabPage();
            this.dugme1 = new System.Windows.Forms.Button();
            this.dugme2 = new System.Windows.Forms.Button();
            this.dugme3 = new System.Windows.Forms.Button();
            this.dugme4 = new System.Windows.Forms.Button();
            this.zamanAraligiKutusu = new System.Windows.Forms.ComboBox();

            this.sekmeKontrolu.SuspendLayout();
            this.sekmeSayfasi1.SuspendLayout();
            this.sekmeSayfasi2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kullanimTablosu)).BeginInit();
            this.SuspendLayout();

            // sekmeKontrolu
            this.sekmeKontrolu.Controls.Add(this.sekmeSayfasi1);
            this.sekmeKontrolu.Controls.Add(this.sekmeSayfasi2);
            this.sekmeKontrolu.Controls.Add(this.sekmeSayfasi3);
            this.sekmeKontrolu.Controls.Add(this.sekmeSayfasi4);
            this.sekmeKontrolu.Location = new System.Drawing.Point(10, 50);
            this.sekmeKontrolu.Name = "sekmeKontrolu";
            this.sekmeKontrolu.SelectedIndex = 0;
            this.sekmeKontrolu.Size = new System.Drawing.Size(760, 600);
            this.sekmeKontrolu.TabIndex = 0;
            this.sekmeKontrolu.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.sekmeKontrolu.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.sekmeKontrolu.Appearance = TabAppearance.FlatButtons;
            this.sekmeKontrolu.ItemSize = new System.Drawing.Size(0, 1);
            this.sekmeKontrolu.SizeMode = TabSizeMode.Fixed;

            // sekmeSayfasi1
            this.sekmeSayfasi1.Controls.Add(this.sistemBilgiEtiketi);
            this.sekmeSayfasi1.Location = new System.Drawing.Point(4, 4);
            this.sekmeSayfasi1.Name = "sekmeSayfasi1";
            this.sekmeSayfasi1.Padding = new System.Windows.Forms.Padding(3);
            this.sekmeSayfasi1.Size = new System.Drawing.Size(752, 592);
            this.sekmeSayfasi1.TabIndex = 0;
            this.sekmeSayfasi1.Text = "Sistem Bilgileri";
            this.sekmeSayfasi1.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.sekmeSayfasi1.Enter += new System.EventHandler(this.SekmeSayfasi1_Giris);

            // sistemBilgiEtiketi
            this.sistemBilgiEtiketi.Location = new System.Drawing.Point(6, 6);
            this.sistemBilgiEtiketi.Name = "sistemBilgiEtiketi";
            this.sistemBilgiEtiketi.Size = new System.Drawing.Size(740, 580);
            this.sistemBilgiEtiketi.TabIndex = 0;
            this.sistemBilgiEtiketi.ForeColor = System.Drawing.Color.White;
            this.sistemBilgiEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.sistemBilgiEtiketi.AutoSize = false;
            this.sistemBilgiEtiketi.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // sekmeSayfasi2
            this.sekmeSayfasi2.Controls.Add(this.izlemeGrafik);
            this.sekmeSayfasi2.Controls.Add(this.kullanimTablosu);
            this.sekmeSayfasi2.Controls.Add(this.islemciSicaklikEtiketi);
            this.sekmeSayfasi2.Controls.Add(this.ekranKartiSicaklikEtiketi);
            this.sekmeSayfasi2.Controls.Add(this.islemciEtiketi);
            this.sekmeSayfasi2.Controls.Add(this.ekranKartiEtiketi);
            this.sekmeSayfasi2.Location = new System.Drawing.Point(4, 4);
            this.sekmeSayfasi2.Name = "sekmeSayfasi2";
            this.sekmeSayfasi2.Padding = new System.Windows.Forms.Padding(3);
            this.sekmeSayfasi2.Size = new System.Drawing.Size(752, 592);
            this.sekmeSayfasi2.TabIndex = 1;
            this.sekmeSayfasi2.Text = "Veri Ýzleme";
            this.sekmeSayfasi2.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // izlemeGrafik
            this.izlemeGrafik.Location = new System.Drawing.Point(6, 90);
            this.izlemeGrafik.Name = "izlemeGrafik";
            this.izlemeGrafik.Size = new System.Drawing.Size(740, 500);
            this.izlemeGrafik.TabIndex = 0;

            // kullanimTablosu
            this.kullanimTablosu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kullanimTablosu.Location = new System.Drawing.Point(6, 6);
            this.kullanimTablosu.Name = "kullanimTablosu";
            this.kullanimTablosu.RowHeadersVisible = false;
            this.kullanimTablosu.Size = new System.Drawing.Size(300, 80);
            this.kullanimTablosu.TabIndex = 1;
            this.kullanimTablosu.BackgroundColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.kullanimTablosu.ForeColor = System.Drawing.Color.White;
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.kullanimTablosu.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.kullanimTablosu.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.kullanimTablosu.DefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);

            // islemciSicaklikEtiketi
            this.islemciSicaklikEtiketi.Location = new System.Drawing.Point(316, 6);
            this.islemciSicaklikEtiketi.Size = new System.Drawing.Size(100, 40);
            this.islemciSicaklikEtiketi.Text = "0.0°C";
            this.islemciSicaklikEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.islemciSicaklikEtiketi.BackColor = System.Drawing.Color.Green;
            this.islemciSicaklikEtiketi.ForeColor = System.Drawing.Color.White;
            this.islemciSicaklikEtiketi.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.islemciSicaklikEtiketi.Name = "islemciSicaklikEtiketi";
            this.islemciSicaklikEtiketi.TabIndex = 2;

            // ekranKartiSicaklikEtiketi
            this.ekranKartiSicaklikEtiketi.Location = new System.Drawing.Point(426, 6);
            this.ekranKartiSicaklikEtiketi.Size = new System.Drawing.Size(100, 40);
            this.ekranKartiSicaklikEtiketi.Text = "0.0°C";
            this.ekranKartiSicaklikEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ekranKartiSicaklikEtiketi.BackColor = System.Drawing.Color.Green;
            this.ekranKartiSicaklikEtiketi.ForeColor = System.Drawing.Color.White;
            this.ekranKartiSicaklikEtiketi.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.ekranKartiSicaklikEtiketi.Name = "ekranKartiSicaklikEtiketi";
            this.ekranKartiSicaklikEtiketi.TabIndex = 3;

            // islemciEtiketi
            this.islemciEtiketi.Location = new System.Drawing.Point(316, 46);
            this.islemciEtiketi.Size = new System.Drawing.Size(100, 20);
            this.islemciEtiketi.Text = "CPU";
            this.islemciEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.islemciEtiketi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.islemciEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.islemciEtiketi.Name = "islemciEtiketi";
            this.islemciEtiketi.TabIndex = 4;

            // ekranKartiEtiketi
            this.ekranKartiEtiketi.Location = new System.Drawing.Point(426, 46);
            this.ekranKartiEtiketi.Size = new System.Drawing.Size(100, 20);
            this.ekranKartiEtiketi.Text = "GPU";
            this.ekranKartiEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ekranKartiEtiketi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.ekranKartiEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.ekranKartiEtiketi.Name = "ekranKartiEtiketi";
            this.ekranKartiEtiketi.TabIndex = 5;

            // sekmeSayfasi3
            this.sekmeSayfasi3.Location = new System.Drawing.Point(4, 4);
            this.sekmeSayfasi3.Name = "sekmeSayfasi3";
            this.sekmeSayfasi3.Size = new System.Drawing.Size(752, 592);
            this.sekmeSayfasi3.TabIndex = 2;
            this.sekmeSayfasi3.Text = "Optimizasyon";
            this.sekmeSayfasi3.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // sekmeSayfasi4
            this.sekmeSayfasi4.Location = new System.Drawing.Point(4, 4);
            this.sekmeSayfasi4.Name = "sekmeSayfasi4";
            this.sekmeSayfasi4.Size = new System.Drawing.Size(752, 592);
            this.sekmeSayfasi4.TabIndex = 3;
            this.sekmeSayfasi4.Text = "Oyun";
            this.sekmeSayfasi4.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // dugme1
            this.dugme1.Location = new System.Drawing.Point(10, 10);
            this.dugme1.Name = "dugme1";
            this.dugme1.Size = new System.Drawing.Size(80, 30);
            this.dugme1.TabIndex = 1;
            this.dugme1.Text = "Sistem";
            this.dugme1.UseVisualStyleBackColor = false;
            this.dugme1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.dugme1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme1.FlatStyle = FlatStyle.Flat;
            this.dugme1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dugme1.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.dugme1.Click += new System.EventHandler(this.Dugme1_Tikla);
            this.dugme1.MouseEnter += new System.EventHandler(this.Dugme_UzerineGel);
            this.dugme1.MouseLeave += new System.EventHandler(this.Dugme_Ayril);

            // dugme2
            this.dugme2.Location = new System.Drawing.Point(100, 10);
            this.dugme2.Name = "dugme2";
            this.dugme2.Size = new System.Drawing.Size(80, 30);
            this.dugme2.TabIndex = 2;
            this.dugme2.Text = "Veri Ýzleme";
            this.dugme2.UseVisualStyleBackColor = false;
            this.dugme2.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.dugme2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme2.FlatStyle = FlatStyle.Flat;
            this.dugme2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dugme2.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.dugme2.Click += new System.EventHandler(this.Dugme2_Tikla);
            this.dugme2.MouseEnter += new System.EventHandler(this.Dugme_UzerineGel);
            this.dugme2.MouseLeave += new System.EventHandler(this.Dugme_Ayril);

            // dugme3
            this.dugme3.Location = new System.Drawing.Point(190, 10);
            this.dugme3.Name = "dugme3";
            this.dugme3.Size = new System.Drawing.Size(80, 30);
            this.dugme3.TabIndex = 3;
            this.dugme3.Text = "Optimizasyon";
            this.dugme3.UseVisualStyleBackColor = false;
            this.dugme3.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.dugme3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme3.FlatStyle = FlatStyle.Flat;
            this.dugme3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dugme3.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.dugme3.Click += new System.EventHandler(this.Dugme3_Tikla);
            this.dugme3.MouseEnter += new System.EventHandler(this.Dugme_UzerineGel);
            this.dugme3.MouseLeave += new System.EventHandler(this.Dugme_Ayril);

            // dugme4
            this.dugme4.Location = new System.Drawing.Point(280, 10);
            this.dugme4.Name = "dugme4";
            this.dugme4.Size = new System.Drawing.Size(80, 30);
            this.dugme4.TabIndex = 4;
            this.dugme4.Text = "Oyun";
            this.dugme4.UseVisualStyleBackColor = false;
            this.dugme4.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.dugme4.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme4.FlatStyle = FlatStyle.Flat;
            this.dugme4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.dugme4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dugme4.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.dugme4.Click += new System.EventHandler(this.Dugme4_Tikla);
            this.dugme4.MouseEnter += new System.EventHandler(this.Dugme_UzerineGel);
            this.dugme4.MouseLeave += new System.EventHandler(this.Dugme_Ayril);

            // zamanAraligiKutusu
            this.zamanAraligiKutusu.Location = new System.Drawing.Point(370, 10);
            this.zamanAraligiKutusu.Size = new System.Drawing.Size(120, 30);
            this.zamanAraligiKutusu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zamanAraligiKutusu.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.zamanAraligiKutusu.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.zamanAraligiKutusu.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.zamanAraligiKutusu.Name = "zamanAraligiKutusu";
            this.zamanAraligiKutusu.TabIndex = 5;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 660);
            this.Controls.Add(this.zamanAraligiKutusu);
            this.Controls.Add(this.dugme4);
            this.Controls.Add(this.dugme3);
            this.Controls.Add(this.dugme2);
            this.Controls.Add(this.dugme1);
            this.Controls.Add(this.sekmeKontrolu);
            this.Name = "Form1";
            this.Text = "Adam'ýn GÜÇ YÖNETÝM ARACI";
            this.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormuKapatirken);
            this.sekmeKontrolu.ResumeLayout(false);
            this.sekmeSayfasi1.ResumeLayout(false);
            this.sekmeSayfasi2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kullanimTablosu)).EndInit();
            this.ResumeLayout(false);
        }

        private void ZamanAraligiKutusunuKur()
        {
            zamanAraligiKutusu?.Items.AddRange(new object[] { "1 Dakika", "5 Dakika", "30 Dakika", "1 Saat", "2 Saat", "4 Saat", "10 Saat", "1 Gün", "1 Hafta" });
            zamanAraligiKutusu!.SelectedIndex = 1;
            zamanAraligiKutusu.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                izlemeGrafik!.SeciliZamanAraligi = zamanAraligiKutusu.SelectedIndex switch
                {
                    0 => TimeSpan.FromMinutes(1),
                    1 => TimeSpan.FromMinutes(5),
                    2 => TimeSpan.FromMinutes(30),
                    3 => TimeSpan.FromHours(1),
                    4 => TimeSpan.FromHours(2),
                    5 => TimeSpan.FromHours(4),
                    6 => TimeSpan.FromHours(10),
                    7 => TimeSpan.FromDays(1),
                    8 => TimeSpan.FromDays(7),
                    _ => TimeSpan.FromMinutes(5)
                };
            };
        }

        private void KullanimTablosunuKur()
        {
            kullanimTablosu?.Columns.Add("Metrik", "Ölçüm");
            kullanimTablosu?.Columns.Add("Deger", "Deðer");
            kullanimTablosu!.Columns["Metrik"].Width = 200;
            kullanimTablosu.Columns["Deger"].Width = 90;

            var tabloZamanlayici = new System.Windows.Forms.Timer { Interval = 2000 };
            tabloZamanlayici.Tick += (object? sender, EventArgs e) => KullanimTablosunuGuncelle();
            tabloZamanlayici.Start();

            KullanimTablosunuGuncelle();
        }

        private void SicaklikGuncellemeleriniKur()
        {
            var sicaklikZamanlayici = new System.Windows.Forms.Timer { Interval = 2000 };
            sicaklikZamanlayici.Tick += (object? sender, EventArgs e) => SicakligiGuncelle();
            sicaklikZamanlayici.Start();

            SicakligiGuncelle();
        }

        private void SicakligiGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5)) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alýnamadý.");
                }

                double islemciSicakligi = sistemVerileri.islemciSicakligi ?? 0.0;
                double ekranKartiSicakligi = sistemVerileri.ekranKartiSicakligi ?? 0.0;

                islemciSicaklikEtiketi!.Text = $"{islemciSicakligi:F1}°C";
                ekranKartiSicaklikEtiketi!.Text = $"{ekranKartiSicakligi:F1}°C";

                islemciSicaklikEtiketi.BackColor = SicaklikRengiAl(islemciSicakligi);
                ekranKartiSicaklikEtiketi.BackColor = SicaklikRengiAl(ekranKartiSicakligi);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.DataFetchError);
                islemciSicaklikEtiketi!.Text = "N/A";
                ekranKartiSicaklikEtiketi!.Text = "N/A";
                islemciSicaklikEtiketi.BackColor = Color.Gray;
                ekranKartiSicaklikEtiketi.BackColor = Color.Gray;
            }
        }

        private Color SicaklikRengiAl(double sicaklik)
        {
            if (sicaklik <= 40) return Color.FromArgb(0, 255, 0);
            if (sicaklik <= 60) return Color.FromArgb(255, 255, 0);
            if (sicaklik <= 80) return Color.FromArgb(255, 165, 0);
            return Color.FromArgb(255, 0, 0);
        }

        private void KullanimTablosunuGuncelle()
        {
            try
            {
                var sistemVerileri = BilgisayarBilgileri.GetSystemData(TimeSpan.FromMinutes(5)) as SistemVerileri;
                if (sistemVerileri == null)
                {
                    throw new Exception("Sistem verileri alýnamadý.");
                }

                kullanimTablosu!.Rows.Clear();
                kullanimTablosu.Rows.Add("Ýþlemci Kullanýmý (%)", sistemVerileri.islemciVerileri.Count > 0 ? $"{sistemVerileri.islemciVerileri[^1].deger:F1}" : "0.0");
                kullanimTablosu.Rows.Add("RAM Kullanýmý (%)", sistemVerileri.ramVerileri.Count > 0 ? $"{sistemVerileri.ramVerileri[^1].deger:F1}" : "0.0");
                kullanimTablosu.Rows.Add("Disk Aktivitesi (ölçekli)", sistemVerileri.diskVerileri.Count > 0 ? $"{sistemVerileri.diskVerileri[^1].deger:F1}" : "0.0");
                kullanimTablosu.Rows.Add("Ekran Kartý Kullanýmý (%)", sistemVerileri.ekranKartiVerileri.Count > 0 ? $"{sistemVerileri.ekranKartiVerileri[^1].deger:F1}" : "0.0");
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.DataFetchError);
                kullanimTablosu!.Rows.Clear();
                kullanimTablosu.Rows.Add("Ýþlemci Kullanýmý (%)", "N/A");
                kullanimTablosu.Rows.Add("RAM Kullanýmý (%)", "N/A");
                kullanimTablosu.Rows.Add("Disk Aktivitesi (ölçekli)", "N/A");
                kullanimTablosu.Rows.Add("Ekran Kartý Kullanýmý (%)", "N/A");
            }
        }

        private void Dugme1_Tikla(object? sender, EventArgs e)
        {
            sekmeKontrolu!.SelectedIndex = 0;
        }

        private void Dugme2_Tikla(object? sender, EventArgs e)
        {
            sekmeKontrolu!.SelectedIndex = 1;
        }

        private void Dugme3_Tikla(object? sender, EventArgs e)
        {
            sekmeKontrolu!.SelectedIndex = 2;
        }

        private void Dugme4_Tikla(object? sender, EventArgs e)
        {
            sekmeKontrolu!.SelectedIndex = 3;
        }

        private void Dugme_UzerineGel(object? sender, EventArgs e)
        {
            var dugme = sender as Button;
            if (dugme != null)
            {
                dugme.ForeColor = Color.FromArgb(255, 50, 50);
                dugme.FlatAppearance.BorderColor = Color.FromArgb(255, 50, 50);
            }
        }

        private void Dugme_Ayril(object? sender, EventArgs e)
        {
            var dugme = sender as Button;
            if (dugme != null)
            {
                dugme.ForeColor = Color.FromArgb(255, 0, 0);
                dugme.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            }
        }

        private void SekmeSayfasi1_Giris(object? sender, EventArgs e)
        {
            try
            {
                sistemBilgiEtiketi!.Text = BilgisayarBilgileri.GetBilgi() ?? "Sistem bilgileri alýnamadý.";
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, ErrorHandler.ErrorMessages.DataFetchError);
                sistemBilgiEtiketi!.Text = "Sistem bilgileri alýnamadý.";
            }
        }

        protected override void FormuKapatirken(FormClosingEventArgs e)
        {
            izlemeGrafik?.GuncellemeyiDurdur();
            base.OnFormClosing(e);
        }
    }
}