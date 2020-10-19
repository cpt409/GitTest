using System;
using System.Collections.Generic;

namespace xTest.Models
{
    public partial class Starters
    {
        public int StarterId { get; set; }
        public string StarterName { get; set; }
        public string TeamName { get; set; }
        public double VarEra { get; set; }
        public double VarIps { get; set; }
        public double VarKo9 { get; set; }
        public double VarBb9 { get; set; }
        public int VarFip { get; set; }
        public int FanId { get; set; }
    }
}
