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
        private readonly SystemGraph sistemGrafik = new();
        private readonly SystemMonitor sistemMonitor;

        public Form1()
        {
            BilesenleriBaslat();
            ZamanAraligiKutusunuKur();
            sistemMonitor = new SystemMonitor();
            sistemMonitor.GuncellemeleriKur();
            SistemBilgiTablosunuDoldur();
            Dugme1_Tikla(null, EventArgs.Empty); // Varsayýlan sistem bilgileri sekmesi
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
            dugme1.Location = new Point(10, 10);
            dugme1.Name = "dugme1";
            dugme1.Size = new Size(80, 30);
            dugme1.TabIndex = 1;
            dugme1.Text = "Sistem";
            dugme1.UseVisualStyleBackColor = false;
            dugme1.BackColor = Color.FromArgb(46, 46, 46);
            dugme1.ForeColor = Color.FromArgb(255, 0, 0);
            dugme1.FlatStyle = FlatStyle.Flat;
            dugme1.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugme1.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            dugme1.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugme1.Click += Dugme1_Tikla;
            dugme1.MouseEnter += Dugme_UzerineGel;
            dugme1.MouseLeave += Dugme_Ayril;

            // dugme2
            dugme2.Location = new Point(100, 10);
            dugme2.Name = "dugme2";
            dugme2.Size = new Size(80, 30);
            dugme2.TabIndex = 2;
            dugme2.Text = "Veri Ýzleme";
            dugme2.UseVisualStyleBackColor = false;
            dugme2.BackColor = Color.FromArgb(46, 46, 46);
            dugme2.ForeColor = Color.FromArgb(255, 0, 0);
            dugme2.FlatStyle = FlatStyle.Flat;
            dugme2.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugme2.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            dugme2.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugme2.Click += Dugme2_Tikla;
            dugme2.MouseEnter += Dugme_UzerineGel;
            dugme2.MouseLeave += Dugme_Ayril;

            // dugme3
            dugme3.Location = new Point(190, 10);
            dugme3.Name = "dugme3";
            dugme3.Size = new Size(80, 30);
            dugme3.TabIndex = 3;
            dugme3.Text = "Optimizasyon";
            dugme3.UseVisualStyleBackColor = false;
            dugme3.BackColor = Color.FromArgb(46, 46, 46);
            dugme3.ForeColor = Color.FromArgb(255, 0, 0);
            dugme3.FlatStyle = FlatStyle.Flat;
            dugme3.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugme3.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            dugme3.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugme3.Click += Dugme3_Tikla;
            dugme3.MouseEnter += Dugme_UzerineGel;
            dugme3.MouseLeave += Dugme_Ayril;

            // dugme4
            dugme4.Location = new Point(280, 10);
            dugme4.Name = "dugme4";
            dugme4.Size = new Size(80, 30);
            dugme4.TabIndex = 4;
            dugme4.Text = "Oyun";
            dugme4.UseVisualStyleBackColor = false;
            dugme4.BackColor = Color.FromArgb(46, 46, 46);
            dugme4.ForeColor = Color.FromArgb(255, 0, 0);
            dugme4.FlatStyle = FlatStyle.Flat;
            dugme4.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 0);
            dugme4.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            dugme4.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            dugme4.Click += Dugme4_Tikla;
            dugme4.MouseEnter += Dugme_UzerineGel;
            dugme4.MouseLeave += Dugme_Ayril;

            // zamanAraligiKutusu
            zamanAraligiKutusu.Location = new Point(370, 10);
            zamanAraligiKutusu.Size = new Size(120, 30);
            zamanAraligiKutusu.DropDownStyle = ComboBoxStyle.DropDownList;
            zamanAraligiKutusu.BackColor = Color.FromArgb(60, 60, 60);
            zamanAraligiKutusu.ForeColor = Color.FromArgb(255, 0, 0);
            zamanAraligiKutusu.Font = new Font("Montserrat", 10F, FontStyle.Bold);
            zamanAraligiKutusu.Name = "zamanAraligiKutusu";
            zamanAraligiKutusu.TabIndex = 5;

            // sistemBilgiTablosu
            sistemBilgiTablosu.Location = new Point(10, 50);
            sistemBilgiTablosu.Name = "sistemBilgiTablosu";
            sistemBilgiTablosu.Size = new Size(760, 600);
            sistemBilgiTablosu.TabIndex = 6;
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

            // sistemGrafik
            sistemGrafik.Location = new Point(10, 50);
            sistemGrafik.Name = "sistemGrafik";
            sistemGrafik.Size = new Size(760, 500);
            sistemGrafik.TabIndex = 7;

            // Form1
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 660);
            Controls.Add(sistemGrafik);
            Controls.Add(sistemBilgiTablosu);
            Controls.Add(zamanAraligiKutusu);
            Controls.Add(dugme4);
            Controls.Add(dugme3);
            Controls.Add(dugme2);
            Controls.Add(dugme1);
            Name = "Form1";
            Text = "Adam'ýn GÜÇ YÖNETÝM ARACI";
            BackColor = Color.FromArgb(27, 27, 27);
            FormClosing += FormuKapatirken;
            ((System.ComponentModel.ISupportInitialize)sistemBilgiTablosu).EndInit();
            ResumeLayout(false);
        }

        private void ZamanAraligiKutusunuKur()
        {
            zamanAraligiKutusu.Items.AddRange(new object[] { "1 Dakika", "5 Dakika", "30 Dakika", "1 Saat", "1 Gün", "1 Hafta" });
            zamanAraligiKutusu.SelectedIndex = 0; // Varsayýlan 1 dakika
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
                    _ => TimeSpan.FromMinutes(1)
                };
                sistemGrafik.SeciliZamanAraligi = yeniAralik;
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
            sistemGrafik.Visible = false;
            sistemGrafik.GuncellemeyiDurdur();
        }

        private void Dugme2_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = true;
            sistemGrafik.GuncellemeyiBaslat();
        }

        private void Dugme3_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = false;
            sistemGrafik.GuncellemeyiDurdur();
        }

        private void Dugme4_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu.Visible = false;
            sistemGrafik.Visible = false;
            sistemGrafik.GuncellemeyiDurdur();
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
            sistemGrafik.GuncellemeyiDurdur();
            sistemMonitor.Kaydet();
        }
    }
}