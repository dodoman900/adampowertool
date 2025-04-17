using System;
using System.Collections.Generic;

namespace AdamPowerTool
{
    public class SystemData
    {
        public List<(DateTime time, double value)> cpuData { get; set; }
        public List<(DateTime time, double value)> ramData { get; set; }
        public List<(DateTime time, double value)> diskData { get; set; }
        public List<(DateTime time, double value)> gpuData { get; set; }
        public List<(DateTime time, double value)> powerData { get; set; }
        public double? cpuTemp { get; set; }
        public double? gpuTemp { get; set; }

        public SystemData()
        {
            cpuData = new List<(DateTime, double)>();
            ramData = new List<(DateTime, double)>();
            diskData = new List<(DateTime, double)>();
            gpuData = new List<(DateTime, double)>();
            powerData = new List<(DateTime, double)>();
            cpuTemp = null;
            gpuTemp = null;
        }
    }

    public static class BilgisayarBilgileri
    {
        public static SystemData GetSystemData(TimeSpan timeRange)
        {
            var data = new SystemData();
            data.cpuData.Add((DateTime.Now, 22.0));
            data.ramData.Add((DateTime.Now, 78.1));
            data.diskData.Add((DateTime.Now, 100.0));
            data.gpuData.Add((DateTime.Now, 5.0));
            data.powerData.Add((DateTime.Now, 45.0));
            data.cpuTemp = 0.0;
            data.gpuTemp = 0.0;
            return data;
        }

        public static string GetBilgi()
        {
            return "Test sistem bilgisi";
        }
    }
}