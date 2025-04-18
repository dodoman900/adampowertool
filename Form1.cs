using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public partial class Form1 : Form
    {
        private readonly System.ComponentModel.IContainer bilesenler = new System.ComponentModel.Container();
        private readonly System.Windows.Forms.Button dugme1 = new();
        private readonly System.Windows.Forms.Button dugme2 = new();
        private readonly System.Windows.Forms.Button dugme3 = new();
        private readonly System.Windows.Forms.Button dugme4 = new();
        private readonly System.Windows.Forms.ComboBox zamanAraligiKutusu = new();
        private readonly System.Windows.Forms.DataGridView sistemBilgiTablosu = new();
        private readonly MonitorGraph izlemeGrafik = new();
        private readonly PowerGraph gucGrafik = new();
        private readonly System.Windows.Forms.Label anlikGucEtiketi = new();
        private readonly System.Windows.Forms.Label ortalamaGucEtiketi = new();
        private readonly SystemMonitor sistemMonitor;

        public Form1()
        {
            BilesenleriBaslat();
            ZamanAraligiKutusunuKur();
            sistemMonitor = new SystemMonitor(this);
            sistemMonitor.GuncellemeleriKur();
            izlemeGrafik.GuncellemeyiBaslat();
            gucGrafik.GuncellemeyiBaslat();
            SistemBilgiTablosunuDoldur();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bilesenler.Dispose();
            }
            base.Dispose(disposing);
        }

        private void BilesenleriBaslat()
        {
            ((System.ComponentModel.ISupportInitialize)sistemBilgiTablosu).BeginInit();
            SuspendLayout();

            // dugme1
            dugme1.Location = new System.Drawing.Point(10, 10);
            dugme1.Name = "dugme1";
            dugme1.Size = new System.Drawing.Size(80, 30);
            dugme1.TabIndex = 1;
            dugme1.Text = "Sistem";
            dugme1.UseVisualStyleBackColor = false;
            dugme1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            dugme1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme1.FlatStyle = FlatStyle.Flat;
            dugme1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dugme1.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            dugme1.Click += Dugme1_Tikla;
            dugme1.MouseEnter += Dugme_UzerineGel;
            dugme1.MouseLeave += Dugme_Ayril;

            // dugme2
            dugme2.Location = new System.Drawing.Point(100, 10);
            dugme2.Name = "dugme2";
            dugme2.Size = new System.Drawing.Size(80, 30);
            dugme2.TabIndex = 2;
            dugme2.Text = "Veri Ýzleme";
            dugme2.UseVisualStyleBackColor = false;
            dugme2.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            dugme2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme2.FlatStyle = FlatStyle.Flat;
            dugme2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dugme2.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            dugme2.Click += Dugme2_Tikla;
            dugme2.MouseEnter += Dugme_UzerineGel;
            dugme2.MouseLeave += Dugme_Ayril;

            // dugme3
            dugme3.Location = new System.Drawing.Point(190, 10);
            dugme3.Name = "dugme3";
            dugme3.Size = new System.Drawing.Size(80, 30);
            dugme3.TabIndex = 3;
            dugme3.Text = "Optimizasyon";
            dugme3.UseVisualStyleBackColor = false;
            dugme3.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            dugme3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme3.FlatStyle = FlatStyle.Flat;
            dugme3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dugme3.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            dugme3.Click += Dugme3_Tikla;
            dugme3.MouseEnter += Dugme_UzerineGel;
            dugme3.MouseLeave += Dugme_Ayril;

            // dugme4
            dugme4.Location = new System.Drawing.Point(280, 10);
            dugme4.Name = "dugme4";
            dugme4.Size = new System.Drawing.Size(80, 30);
            dugme4.TabIndex = 4;
            dugme4.Text = "Oyun";
            dugme4.UseVisualStyleBackColor = false;
            dugme4.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            dugme4.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme4.FlatStyle = FlatStyle.Flat;
            dugme4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            dugme4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dugme4.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            dugme4.Click += Dugme4_Tikla;
            dugme4.MouseEnter += Dugme_UzerineGel;
            dugme4.MouseLeave += Dugme_Ayril;

            // zamanAraligiKutusu
            zamanAraligiKutusu.Location = new System.Drawing.Point(370, 10);
            zamanAraligiKutusu.Size = new System.Drawing.Size(120, 30);
            zamanAraligiKutusu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            zamanAraligiKutusu.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            zamanAraligiKutusu.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            zamanAraligiKutusu.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            zamanAraligiKutusu.Name = "zamanAraligiKutusu";
            zamanAraligiKutusu.TabIndex = 5;

            // sistemBilgiTablosu
            sistemBilgiTablosu.Location = new System.Drawing.Point(10, 50);
            sistemBilgiTablosu.Name = "sistemBilgiTablosu";
            sistemBilgiTablosu.Size = new System.Drawing.Size(760, 600);
            sistemBilgiTablosu.TabIndex = 6;
            sistemBilgiTablosu.BackgroundColor = System.Drawing.Color.FromArgb(27, 27, 27);
            sistemBilgiTablosu.ForeColor = System.Drawing.Color.White;
            sistemBilgiTablosu.ColumnHeadersVisible = false;
            sistemBilgiTablosu.RowHeadersVisible = false;
            sistemBilgiTablosu.ReadOnly = true;
            sistemBilgiTablosu.AllowUserToAddRows = false;
            sistemBilgiTablosu.AllowUserToDeleteRows = false;
            sistemBilgiTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sistemBilgiTablosu.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            sistemBilgiTablosu.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            sistemBilgiTablosu.DefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular);
            sistemBilgiTablosu.ScrollBars = ScrollBars.Vertical;

            // izlemeGrafik
            izlemeGrafik.Location = new System.Drawing.Point(10, 50);
            izlemeGrafik.Name = "izlemeGrafik";
            izlemeGrafik.Size = new System.Drawing.Size(480, 400);
            izlemeGrafik.TabIndex = 7;

            // gucGrafik
            gucGrafik.Location = new System.Drawing.Point(500, 50);
            gucGrafik.Name = "gucGrafik";
            gucGrafik.Size = new System.Drawing.Size(270, 400);
            gucGrafik.TabIndex = 8;

            // anlikGucEtiketi
            anlikGucEtiketi.Location = new System.Drawing.Point(500, 460);
            anlikGucEtiketi.Size = new System.Drawing.Size(270, 20);
            anlikGucEtiketi.Text = "Anlýk: 0.0 W";
            anlikGucEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            anlikGucEtiketi.ForeColor = System.Drawing.Color.Cyan;
            anlikGucEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            anlikGucEtiketi.Name = "anlikGucEtiketi";
            anlikGucEtiketi.TabIndex = 9;

            // ortalamaGucEtiketi
            ortalamaGucEtiketi.Location = new System.Drawing.Point(500, 490);
            ortalamaGucEtiketi.Size = new System.Drawing.Size(270, 20);
            ortalamaGucEtiketi.Text = "Ortalama: 0.0 W";
            ortalamaGucEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            ortalamaGucEtiketi.ForeColor = System.Drawing.Color.Cyan;
            ortalamaGucEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            ortalamaGucEtiketi.Name = "ortalamaGucEtiketi";
            ortalamaGucEtiketi.TabIndex = 10;

            // Form1
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(780, 660);
            Controls.Add(anlikGucEtiketi);
            Controls.Add(ortalamaGucEtiketi);
            Controls.Add(gucGrafik);
            Controls.Add(izlemeGrafik);
            Controls.Add(sistemBilgiTablosu);
            Controls.Add(zamanAraligiKutusu);
            Controls.Add(dugme4);
            Controls.Add(dugme3);
            Controls.Add(dugme2);
            Controls.Add(dugme1);
            Name = "Form1";
            Text = "Adam'ýn GÜÇ YÖNETÝM ARACI";
            BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            FormClosing += FormuKapatirken;
            ((System.ComponentModel.ISupportInitialize)sistemBilgiTablosu).EndInit();
            ResumeLayout(false);
        }

        private void ZamanAraligiKutusunuKur()
        {
            zamanAraligiKutusu.Items.AddRange(new object[] { "1 Dakika", "5 Dakika", "30 Dakika", "1 Saat", "1 Gün", "1 Hafta" });
            zamanAraligiKutusu.SelectedIndex = 1;
            zamanAraligiKutusu.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                var yeniAralik = zamanAraligiKutusu.SelectedIndex switch
                {
                    0 => TimeSpan.FromMinutes(1),
                    1 => TimeSpan.FromMinutes(5),
                    2 => TimeSpan.FromMinutes(30),
                    3 => TimeSpan.FromHours(1),
                    4 => TimeSpan.FromDays(1),
                    5 => TimeSpan.FromDays(7),
                    _ => TimeSpan.FromMinutes(5)
                };
                izlemeGrafik.SeciliZamanAraligi = yeniAralik;
                gucGrafik.SeciliZamanAraligi = yeniAralik;
            };
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
                    {
                        sistemBilgiTablosu.Rows.Add(satir);
                    }
                }
            }
        }

        private void Dugme1_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = true;
            izlemeGrafik.Visible = false;
            gucGrafik.Visible = false;
            anlikGucEtiketi.Visible = false;
            ortalamaGucEtiketi.Visible = false;
        }

        private void Dugme2_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            izlemeGrafik.Visible = true;
            gucGrafik.Visible = true;
            anlikGucEtiketi.Visible = true;
            ortalamaGucEtiketi.Visible = true;
        }

        private void Dugme3_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            izlemeGrafik.Visible = false;
            gucGrafik.Visible = false;
            anlikGucEtiketi.Visible = false;
            ortalamaGucEtiketi.Visible = false;
        }

        private void Dugme4_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            izlemeGrafik.Visible = false;
            gucGrafik.Visible = false;
            anlikGucEtiketi.Visible = false;
            ortalamaGucEtiketi.Visible = false;
        }

        private void Dugme_UzerineGel(object? sender, EventArgs e)
        {
            if (sender is Button dugme)
            {
                dugme.ForeColor = Color.FromArgb(255, 50, 50);
                dugme.FlatAppearance.BorderColor = Color.FromArgb(255, 50, 50);
            }
        }

        private void Dugme_Ayril(object? sender, EventArgs e)
        {
            if (sender is Button dugme)
            {
                dugme.ForeColor = Color.FromArgb(255, 0, 0);
                dugme.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            }
        }

        private void FormuKapatirken(object? sender, FormClosingEventArgs e)
        {
            izlemeGrafik.GuncellemeyiDurdur();
            gucGrafik.GuncellemeyiDurdur();
            sistemMonitor.Kaydet();
        }

        public void GucBilgileriniGuncelle(double anlikGuc, double ortalamaGuc)
        {
            anlikGucEtiketi.Text = $"Anlýk: {anlikGuc:F1} W";
            ortalamaGucEtiketi.Text = $"Ortalama: {ortalamaGuc:F1} W";
        }
    }
}