using System;
using System.Collections.Generic;
using System.Text;

namespace DebugApplication.Model
{
    class Coordinates
    {
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public override string ToString() => $"Широта: {Lat}\nДолгота: {Lng}";
    }
}
