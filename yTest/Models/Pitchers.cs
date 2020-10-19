using System;
using System.Collections.Generic;

namespace yTest.Models
{
    public partial class Pitchers
    {
        public int PitcherId { get; set; }
        public string PitcherName { get; set; }
        public string TeamName { get; set; }
        public double VarEra { get; set; }
        public double VarIps { get; set; }
        public double VarKo9 { get; set; }
        public double VarGbp { get; set; }
        public double VarBb9 { get; set; }
        public double VarFb9 { get; set; }
        public int VarFanId { get; set; }
    }
}
