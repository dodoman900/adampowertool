using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.IContainer? bilesenler = null;
        private System.Windows.Forms.Button? dugme1;
        private System.Windows.Forms.Button? dugme2;
        private System.Windows.Forms.Button? dugme3;
        private System.Windows.Forms.Button? dugme4;
        private System.Windows.Forms.ComboBox? zamanAraligiKutusu;
        private System.Windows.Forms.DataGridView? sistemBilgiTablosu;
        private MonitorGraph? izlemeGrafik;
        private System.Windows.Forms.DataGridView? kullanimTablosu;
        private System.Windows.Forms.Label? islemciSicaklikEtiketi;
        private System.Windows.Forms.Label? ekranKartiSicaklikEtiketi;
        private System.Windows.Forms.Label? islemciEtiketi;
        private System.Windows.Forms.Label? ekranKartiEtiketi;
        private SystemMonitor? sistemMonitor;

        public Form1()
        {
            BilesenleriBaslat();
            ZamanAraligiKutusunuKur();
            KullanimTablosunuKur();
            sistemMonitor = new SystemMonitor(this);
            sistemMonitor.SicaklikGuncellemeleriniKur();
            izlemeGrafik?.GuncellemeyiBaslat();
            SistemBilgiTablosunuDoldur();
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
            this.bilesenler = new System.ComponentModel.Container();
            this.dugme1 = new System.Windows.Forms.Button();
            this.dugme2 = new System.Windows.Forms.Button();
            this.dugme3 = new System.Windows.Forms.Button();
            this.dugme4 = new System.Windows.Forms.Button();
            this.zamanAraligiKutusu = new System.Windows.Forms.ComboBox();
            this.sistemBilgiTablosu = new System.Windows.Forms.DataGridView();
            this.izlemeGrafik = new AdamPowerTool.MonitorGraph();
            this.kullanimTablosu = new System.Windows.Forms.DataGridView();
            this.islemciSicaklikEtiketi = new System.Windows.Forms.Label();
            this.ekranKartiSicaklikEtiketi = new System.Windows.Forms.Label();
            this.islemciEtiketi = new System.Windows.Forms.Label();
            this.ekranKartiEtiketi = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.sistemBilgiTablosu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kullanimTablosu)).BeginInit();
            this.SuspendLayout();

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

            // sistemBilgiTablosu
            this.sistemBilgiTablosu.Location = new System.Drawing.Point(10, 50);
            this.sistemBilgiTablosu.Name = "sistemBilgiTablosu";
            this.sistemBilgiTablosu.Size = new System.Drawing.Size(760, 600);
            this.sistemBilgiTablosu.TabIndex = 6;
            this.sistemBilgiTablosu.BackgroundColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.sistemBilgiTablosu.ForeColor = System.Drawing.Color.White;
            this.sistemBilgiTablosu.ColumnHeadersVisible = false;
            this.sistemBilgiTablosu.RowHeadersVisible = false;
            this.sistemBilgiTablosu.ReadOnly = true;
            this.sistemBilgiTablosu.AllowUserToAddRows = false;
            this.sistemBilgiTablosu.AllowUserToDeleteRows = false;
            this.sistemBilgiTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.sistemBilgiTablosu.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.sistemBilgiTablosu.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.sistemBilgiTablosu.DefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            this.sistemBilgiTablosu.ScrollBars = ScrollBars.Vertical;

            // izlemeGrafik
            this.izlemeGrafik.Location = new System.Drawing.Point(10, 150);
            this.izlemeGrafik.Name = "izlemeGrafik";
            this.izlemeGrafik.Size = new System.Drawing.Size(740, 440);
            this.izlemeGrafik.TabIndex = 7;

            // kullanimTablosu
            this.kullanimTablosu.Location = new System.Drawing.Point(10, 50);
            this.kullanimTablosu.Name = "kullanimTablosu";
            this.kullanimTablosu.RowHeadersVisible = false;
            this.kullanimTablosu.Size = new System.Drawing.Size(300, 100);
            this.kullanimTablosu.TabIndex = 8;
            this.kullanimTablosu.BackgroundColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.kullanimTablosu.ForeColor = System.Drawing.Color.White;
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.kullanimTablosu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.kullanimTablosu.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.kullanimTablosu.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.kullanimTablosu.DefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);

            // islemciSicaklikEtiketi
            this.islemciSicaklikEtiketi.Location = new System.Drawing.Point(320, 50);
            this.islemciSicaklikEtiketi.Size = new System.Drawing.Size(100, 40);
            this.islemciSicaklikEtiketi.Text = "0.0°C";
            this.islemciSicaklikEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.islemciSicaklikEtiketi.BackColor = System.Drawing.Color.Green;
            this.islemciSicaklikEtiketi.ForeColor = System.Drawing.Color.White;
            this.islemciSicaklikEtiketi.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.islemciSicaklikEtiketi.Name = "islemciSicaklikEtiketi";
            this.islemciSicaklikEtiketi.TabIndex = 9;

            // ekranKartiSicaklikEtiketi
            this.ekranKartiSicaklikEtiketi.Location = new System.Drawing.Point(430, 50);
            this.ekranKartiSicaklikEtiketi.Size = new System.Drawing.Size(100, 40);
            this.ekranKartiSicaklikEtiketi.Text = "0.0°C";
            this.ekranKartiSicaklikEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ekranKartiSicaklikEtiketi.BackColor = System.Drawing.Color.Green;
            this.ekranKartiSicaklikEtiketi.ForeColor = System.Drawing.Color.White;
            this.ekranKartiSicaklikEtiketi.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.ekranKartiSicaklikEtiketi.Name = "ekranKartiSicaklikEtiketi";
            this.ekranKartiSicaklikEtiketi.TabIndex = 10;

            // islemciEtiketi
            this.islemciEtiketi.Location = new System.Drawing.Point(320, 90);
            this.islemciEtiketi.Size = new System.Drawing.Size(100, 20);
            this.islemciEtiketi.Text = "CPU";
            this.islemciEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.islemciEtiketi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.islemciEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.islemciEtiketi.Name = "islemciEtiketi";
            this.islemciEtiketi.TabIndex = 11;

            // ekranKartiEtiketi
            this.ekranKartiEtiketi.Location = new System.Drawing.Point(430, 90);
            this.ekranKartiEtiketi.Size = new System.Drawing.Size(100, 20);
            this.ekranKartiEtiketi.Text = "GPU";
            this.ekranKartiEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ekranKartiEtiketi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.ekranKartiEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.ekranKartiEtiketi.Name = "ekranKartiEtiketi";
            this.ekranKartiEtiketi.TabIndex = 12;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 660);
            this.Controls.Add(this.sistemBilgiTablosu);
            this.Controls.Add(this.izlemeGrafik);
            this.Controls.Add(this.kullanimTablosu);
            this.Controls.Add(this.islemciSicaklikEtiketi);
            this.Controls.Add(this.ekranKartiSicaklikEtiketi);
            this.Controls.Add(this.islemciEtiketi);
            this.Controls.Add(this.ekranKartiEtiketi);
            this.Controls.Add(this.zamanAraligiKutusu);
            this.Controls.Add(this.dugme4);
            this.Controls.Add(this.dugme3);
            this.Controls.Add(this.dugme2);
            this.Controls.Add(this.dugme1);
            this.Name = "Form1";
            this.Text = "Adam'ýn GÜÇ YÖNETÝM ARACI";
            this.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormuKapatirken);
            ((System.ComponentModel.ISupportInitialize)(this.sistemBilgiTablosu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kullanimTablosu)).EndInit();
            this.ResumeLayout(false);
        }

        private void ZamanAraligiKutusunuKur()
        {
            zamanAraligiKutusu?.Items.AddRange(new object[] { "1 Dakika", "5 Dakika", "30 Dakika", "1 Saat", "2 Saat", "4 Saat", "10 Saat", "1 Gün", "1 Hafta" });
            zamanAraligiKutusu!.SelectedIndex = 1;
            zamanAraligiKutusu.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                if (izlemeGrafik != null)
                {
                    izlemeGrafik.SeciliZamanAraligi = zamanAraligiKutusu.SelectedIndex switch
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
                }
            };
        }

        private void KullanimTablosunuKur()
        {
            kullanimTablosu?.Columns.Add("Metrik", "Ölçüm");
            kullanimTablosu?.Columns.Add("Deger", "Deðer");
            kullanimTablosu!.Columns["Metrik"].Width = 200;
            kullanimTablosu.Columns["Deger"].Width = 90;
            sistemMonitor?.KullanimTablosunuGuncelle();
        }

        private void SistemBilgiTablosunuDoldur()
        {
            sistemBilgiTablosu?.Columns.Add("Bilgi", "Sistem Bilgisi");
            sistemBilgiTablosu!.Columns["Bilgi"].Width = 740;

            var bilgi = BilgisayarBilgileri.GetBilgi();
            if (!string.IsNullOrEmpty(bilgi))
            {
                foreach (var satir in bilgi.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (!string.IsNullOrWhiteSpace(satir))
                    {
                        sistemBilgiTablosu.Rows.Add(satir);
                    }
                }
            }
        }

        private void Dugme1_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = true;
            izlemeGrafik!.Visible = false;
            kullanimTablosu!.Visible = false;
            islemciSicaklikEtiketi!.Visible = false;
            ekranKartiSicaklikEtiketi!.Visible = false;
            islemciEtiketi!.Visible = false;
            ekranKartiEtiketi!.Visible = false;
        }

        private void Dugme2_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = true;
            kullanimTablosu!.Visible = true;
            islemciSicaklikEtiketi!.Visible = true;
            ekranKartiSicaklikEtiketi!.Visible = true;
            islemciEtiketi!.Visible = true;
            ekranKartiEtiketi!.Visible = true;
        }

        private void Dugme3_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = false;
            kullanimTablosu!.Visible = false;
            islemciSicaklikEtiketi!.Visible = false;
            ekranKartiSicaklikEtiketi!.Visible = false;
            islemciEtiketi!.Visible = false;
            ekranKartiEtiketi!.Visible = false;
        }

        private void Dugme4_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = false;
            kullanimTablosu!.Visible = false;
            islemciSicaklikEtiketi!.Visible = false;
            ekranKartiSicaklikEtiketi!.Visible = false;
            islemciEtiketi!.Visible = false;
            ekranKartiEtiketi!.Visible = false;
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

        private void FormuKapatirken(object? sender, FormClosingEventArgs e)
        {
            izlemeGrafik?.GuncellemeyiDurdur();
        }

        public void SicaklikGuncelle(double islemciSicakligi, double ekranKartiSicakligi)
        {
            islemciSicaklikEtiketi!.Text = $"{islemciSicakligi:F1}°C";
            ekranKartiSicaklikEtiketi!.Text = $"{ekranKartiSicakligi:F1}°C";
            islemciSicaklikEtiketi.BackColor = SicaklikRengiAl(islemciSicakligi);
            ekranKartiSicaklikEtiketi.BackColor = SicaklikRengiAl(ekranKartiSicakligi);
        }

        public void KullanimTablosunuGuncelle(SistemVerileri sistemVerileri)
        {
            kullanimTablosu!.Rows.Clear();
            kullanimTablosu.Rows.Add("Ýþlemci Kullanýmý (%)", sistemVerileri.islemciVerileri.Count > 0 ? $"{sistemVerileri.islemciVerileri[^1].deger:F1}" : "0.0");
            kullanimTablosu.Rows.Add("RAM Kullanýmý (%)", sistemVerileri.ramVerileri.Count > 0 ? $"{sistemVerileri.ramVerileri[^1].deger:F1}" : "0.0");
            kullanimTablosu.Rows.Add("Disk Aktivitesi (ölçekli)", sistemVerileri.diskVerileri.Count > 0 ? $"{sistemVerileri.diskVerileri[^1].deger:F1}" : "0.0");
            kullanimTablosu.Rows.Add("Ekran Kartý Kullanýmý (%)", sistemVerileri.ekranKartiVerileri.Count > 0 ? $"{sistemVerileri.ekranKartiVerileri[^1].deger:F1}" : "0.0");
            kullanimTablosu.Rows.Add("Güç Kullanýmý (Watt)", sistemVerileri.gucVerileri.Count > 0 ? $"{sistemVerileri.gucVerileri[^1].deger:F1}" : "0.0");
        }

        private Color SicaklikRengiAl(double sicaklik)
        {
            if (sicaklik <= 40) return Color.FromArgb(0, 255, 0);
            if (sicaklik <= 60) return Color.FromArgb(255, 255, 0);
            if (sicaklik <= 80) return Color.FromArgb(255, 165, 0);
            return Color.FromArgb(255, 0, 0);
        }
    }
}