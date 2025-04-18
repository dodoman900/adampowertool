using System;
using System.Collections.Generic;

namespace AdamPowerTool
{
    public class SistemVerileri
    {
        public List<(DateTime zaman, double deger)> islemciVerileri { get; set; } = new();
        public List<(DateTime zaman, double deger)> ramVerileri { get; set; } = new();
        public List<(DateTime zaman, double deger)> diskVerileri { get; set; } = new();
        public List<(DateTime zaman, double deger)> ekranKartiVerileri { get; set; } = new();
        public List<(DateTime zaman, double deger)> gucVerileri { get; set; } = new();
        public double? islemciSicakligi { get; set; }
        public double? ekranKartiSicakligi { get; set; }
    }
}