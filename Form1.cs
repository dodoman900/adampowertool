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
        private PowerGraph? gucGrafik;
        private System.Windows.Forms.Label? anlikGucEtiketi;
        private System.Windows.Forms.Label? ortalamaGucEtiketi;
        private SystemMonitor? sistemMonitor;

        public Form1()
        {
            BilesenleriBaslat();
            ZamanAraligiKutusunuKur();
            sistemMonitor = new SystemMonitor(this);
            sistemMonitor.GuncellemeleriKur();
            izlemeGrafik?.GuncellemeyiBaslat();
            gucGrafik?.GuncellemeyiBaslat();
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
            this.izlemeGrafik = new MonitorGraph();
            this.gucGrafik = new PowerGraph();
            this.anlikGucEtiketi = new System.Windows.Forms.Label();
            this.ortalamaGucEtiketi = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.sistemBilgiTablosu)).BeginInit();
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
            this.izlemeGrafik.Location = new System.Drawing.Point(10, 50);
            this.izlemeGrafik.Name = "izlemeGrafik";
            this.izlemeGrafik.Size = new System.Drawing.Size(480, 400);
            this.izlemeGrafik.TabIndex = 7;

            // gucGrafik
            this.gucGrafik.Location = new System.Drawing.Point(500, 50);
            this.gucGrafik.Name = "gucGrafik";
            this.gucGrafik.Size = new System.Drawing.Size(270, 400);
            this.gucGrafik.TabIndex = 8;

            // anlikGucEtiketi
            this.anlikGucEtiketi.Location = new System.Drawing.Point(500, 460);
            this.anlikGucEtiketi.Size = new System.Drawing.Size(270, 20);
            this.anlikGucEtiketi.Text = "Anlýk: 0.0 W";
            this.anlikGucEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.anlikGucEtiketi.ForeColor = System.Drawing.Color.Cyan;
            this.anlikGucEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.anlikGucEtiketi.Name = "anlikGucEtiketi";
            this.anlikGucEtiketi.TabIndex = 9;

            // ortalamaGucEtiketi
            this.ortalamaGucEtiketi.Location = new System.Drawing.Point(500, 490);
            this.ortalamaGucEtiketi.Size = new System.Drawing.Size(270, 20);
            this.ortalamaGucEtiketi.Text = "Ortalama: 0.0 W";
            this.ortalamaGucEtiketi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ortalamaGucEtiketi.ForeColor = System.Drawing.Color.Cyan;
            this.ortalamaGucEtiketi.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.ortalamaGucEtiketi.Name = "ortalamaGucEtiketi";
            this.ortalamaGucEtiketi.TabIndex = 10;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 660);
            this.Controls.Add(this.anlikGucEtiketi);
            this.Controls.Add(this.ortalamaGucEtiketi);
            this.Controls.Add(this.gucGrafik);
            this.Controls.Add(this.izlemeGrafik);
            this.Controls.Add(this.sistemBilgiTablosu);
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
            this.ResumeLayout(false);
        }

        private void ZamanAraligiKutusunuKur()
        {
            zamanAraligiKutusu?.Items.AddRange(new object[] { "1 Dakika", "5 Dakika", "30 Dakika", "1 Saat", "1 Gün", "1 Hafta" });
            zamanAraligiKutusu!.SelectedIndex = 1;
            zamanAraligiKutusu.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                if (izlemeGrafik != null && gucGrafik != null)
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
                }
            };
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
            gucGrafik!.Visible = false;
            anlikGucEtiketi!.Visible = false;
            ortalamaGucEtiketi!.Visible = false;
        }

        private void Dugme2_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = true;
            gucGrafik!.Visible = true;
            anlikGucEtiketi!.Visible = true;
            ortalamaGucEtiketi!.Visible = true;
        }

        private void Dugme3_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = false;
            gucGrafik!.Visible = false;
            anlikGucEtiketi!.Visible = false;
            ortalamaGucEtiketi!.Visible = false;
        }

        private void Dugme4_Tikla(object? sender, EventArgs e)
        {
            sistemBilgiTablosu!.Visible = false;
            izlemeGrafik!.Visible = false;
            gucGrafik!.Visible = false;
            anlikGucEtiketi!.Visible = false;
            ortalamaGucEtiketi!.Visible = false;
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
            gucGrafik?.GuncellemeyiDurdur();
            sistemMonitor?.Kaydet();
        }

        public void GucBilgileriniGuncelle(double anlikGuc, double ortalamaGuc)
        {
            anlikGucEtiketi!.Text = $"Anlýk: {anlikGuc:F1} W";
            ortalamaGucEtiketi!.Text = $"Ortalama: {ortalamaGuc:F1} W";
        }
    }
}