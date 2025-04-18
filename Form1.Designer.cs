﻿namespace AdamPowerTool
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.timeRangeComboBox = new System.Windows.Forms.ComboBox();
            this.systemInfoLabel = new System.Windows.Forms.Label();
            this.monitorGraph = new AdamPowerTool.MonitorGraph();
            this.usageTable = new System.Windows.Forms.DataGridView();
            this.cpuTempLabel = new System.Windows.Forms.Label();
            this.gpuTempLabel = new System.Windows.Forms.Label();
            this.cpuLabel = new System.Windows.Forms.Label();
            this.gpuLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usageTable)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(10, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 600);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.tabControl1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.tabControl1.Appearance = TabAppearance.FlatButtons;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.SizeMode = TabSizeMode.Fixed;

            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.systemInfoLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 592);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sistem Bilgileri";
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.monitorGraph);
            this.tabPage2.Controls.Add(this.usageTable);
            this.tabPage2.Controls.Add(this.cpuTempLabel);
            this.tabPage2.Controls.Add(this.gpuTempLabel);
            this.tabPage2.Controls.Add(this.cpuLabel);
            this.tabPage2.Controls.Add(this.gpuLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 592);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Veri İzleme";
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(752, 592);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Optimizasyon";
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(752, 592);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Oyun";
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sistem";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.button1.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button_MouseLeave);

            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(100, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Veri İzleme";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.button2.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.button2.MouseLeave += new System.EventHandler(this.button_MouseLeave);

            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(190, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 3;
            this.button3.Text = "Optimizasyon";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button3.FlatStyle = FlatStyle.Flat;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.button3.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.button3.MouseLeave += new System.EventHandler(this.button_MouseLeave);

            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(280, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 30);
            this.button4.TabIndex = 4;
            this.button4.Text = "Oyun";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button4.FlatStyle = FlatStyle.Flat;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.button4.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Bold);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.button4.MouseLeave += new System.EventHandler(this.button_MouseLeave);

            // 
            // timeRangeComboBox
            // 
            this.timeRangeComboBox.Location = new System.Drawing.Point(370, 10);
            this.timeRangeComboBox.Size = new System.Drawing.Size(120, 30);
            this.timeRangeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeRangeComboBox.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.timeRangeComboBox.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.timeRangeComboBox.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.timeRangeComboBox.Name = "timeRangeComboBox";
            this.timeRangeComboBox.TabIndex = 5;

            // 
            // systemInfoLabel
            // 
            this.systemInfoLabel.Location = new System.Drawing.Point(6, 6);
            this.systemInfoLabel.Name = "systemInfoLabel";
            this.systemInfoLabel.Size = new System.Drawing.Size(740, 580);
            this.systemInfoLabel.TabIndex = 0;
            this.systemInfoLabel.ForeColor = System.Drawing.Color.White;
            this.systemInfoLabel.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.systemInfoLabel.AutoSize = false;
            this.systemInfoLabel.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);

            // 
            // monitorGraph
            // 
            this.monitorGraph.Location = new System.Drawing.Point(6, 90);
            this.monitorGraph.Name = "monitorGraph";
            this.monitorGraph.Size = new System.Drawing.Size(740, 500);
            this.monitorGraph.TabIndex = 0;

            // 
            // usageTable
            // 
            this.usageTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usageTable.Location = new System.Drawing.Point(6, 6);
            this.usageTable.Name = "usageTable";
            this.usageTable.RowHeadersVisible = false;
            this.usageTable.Size = new System.Drawing.Size(300, 80);
            this.usageTable.TabIndex = 1;
            this.usageTable.BackgroundColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.usageTable.ForeColor = System.Drawing.Color.White;
            this.usageTable.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
            this.usageTable.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.usageTable.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.usageTable.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.usageTable.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.usageTable.DefaultCellStyle.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);

            // 
            // cpuTempLabel
            // 
            this.cpuTempLabel.Location = new System.Drawing.Point(316, 6);
            this.cpuTempLabel.Size = new System.Drawing.Size(100, 40);
            this.cpuTempLabel.Text = "0.0°C";
            this.cpuTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cpuTempLabel.BackColor = System.Drawing.Color.Green;
            this.cpuTempLabel.ForeColor = System.Drawing.Color.White;
            this.cpuTempLabel.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.cpuTempLabel.Name = "cpuTempLabel";
            this.cpuTempLabel.TabIndex = 2;

            // 
            // gpuTempLabel
            // 
            this.gpuTempLabel.Location = new System.Drawing.Point(426, 6);
            this.gpuTempLabel.Size = new System.Drawing.Size(100, 40);
            this.gpuTempLabel.Text = "0.0°C";
            this.gpuTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gpuTempLabel.BackColor = System.Drawing.Color.Green;
            this.gpuTempLabel.ForeColor = System.Drawing.Color.White;
            this.gpuTempLabel.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold);
            this.gpuTempLabel.Name = "gpuTempLabel";
            this.gpuTempLabel.TabIndex = 3;

            // 
            // cpuLabel
            // 
            this.cpuLabel.Location = new System.Drawing.Point(316, 46);
            this.cpuLabel.Size = new System.Drawing.Size(100, 20);
            this.cpuLabel.Text = "CPU";
            this.cpuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cpuLabel.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.cpuLabel.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.cpuLabel.Name = "cpuLabel";
            this.cpuLabel.TabIndex = 4;

            // 
            // gpuLabel
            // 
            this.gpuLabel.Location = new System.Drawing.Point(426, 46);
            this.gpuLabel.Size = new System.Drawing.Size(100, 20);
            this.gpuLabel.Text = "GPU";
            this.gpuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gpuLabel.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
            this.gpuLabel.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
            this.gpuLabel.Name = "gpuLabel";
            this.gpuLabel.TabIndex = 5;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.timeRangeComboBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Adam's POWER MANAGEMENT TOOL";
            this.BackColor = System.Drawing.Color.FromArgb(27, 27, 27);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usageTable)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox timeRangeComboBox;
        private System.Windows.Forms.Label systemInfoLabel;
        private MonitorGraph monitorGraph;
        private System.Windows.Forms.DataGridView usageTable;
        private System.Windows.Forms.Label cpuTempLabel;
        private System.Windows.Forms.Label gpuTempLabel;
        private System.Windows.Forms.Label cpuLabel;
        private System.Windows.Forms.Label gpuLabel;
    }
}