using System;
using System.Collections.Generic;

namespace AdamPowerTool
{
    public class SistemVerileri
    {
        public List<(DateTime zaman, double deger)> islemciVerileri { get; set; } = new List<(DateTime, double)>();
        public List<(DateTime zaman, double deger)> ramVerileri { get; set; } = new List<(DateTime, double)>();
        public List<(DateTime zaman, double deger)> diskVerileri { get; set; } = new List<(DateTime, double)>();
        public List<(DateTime zaman, double deger)> ekranKartiVerileri { get; set; } = new List<(DateTime, double)>();
        public List<(DateTime zaman, double deger)> gucVerileri { get; set; } = new List<(DateTime, double)>();
    }
}