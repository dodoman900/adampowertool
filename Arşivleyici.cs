using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AdamPowerTool
{
    public static class Arşivleyici
    {
        private static readonly string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "systemData.json");
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arşivLog.txt");

        // Verileri JSON’a kaydet ve arşivleme zamanını logla
        public static void SaveSystemData(List<(DateTime time, double cpu, double ramUsed, double ramTotal, double disk, double gpu, double cpuTemp, double gpuTemp)> systemData)
        {
            try
            {
                string json = JsonConvert.SerializeObject(systemData, Formatting.Indented);
                File.WriteAllText(dataFilePath, json);

                // Arşivleme zamanını logla
                string logEntry = $"Arşivleme yapıldı: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Arşivleme başarısız oldu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // JSON’dan verileri yükle
        public static List<(DateTime time, double cpu, double ramUsed, double ramTotal, double disk, double gpu, double cpuTemp, double gpuTemp)> LoadSystemData()
        {
            try
            {
                if (File.Exists(dataFilePath))
                {
                    string json = File.ReadAllText(dataFilePath);
                    var data = JsonConvert.DeserializeObject<List<(DateTime, double, double, double, double, double, double, double)>>(json) ?? new List<(DateTime, double, double, double, double, double, double, double)>();
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Arşiv yüklenemedi: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<(DateTime, double, double, double, double, double, double, double)>();
        }

        // 1 haftadan eski verileri sil
        public static void CleanOldData(List<(DateTime time, double cpu, double ramUsed, double ramTotal, double disk, double gpu, double cpuTemp, double gpuTemp)> systemData)
        {
            try
            {
                int initialCount = systemData.Count;
                systemData.RemoveAll(d => (DateTime.Now - d.time).TotalDays > 7);

                if (systemData.Count < initialCount)
                {
                    // Değişiklikleri kaydet
                    SaveSystemData(systemData);
                    string logEntry = $"Eski veriler silindi: {DateTime.Now:yyyy-MM-dd HH:mm:ss}, Silinen kayıt sayısı: {initialCount - systemData.Count}\n";
                    File.AppendAllText(logFilePath, logEntry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eski veriler silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}