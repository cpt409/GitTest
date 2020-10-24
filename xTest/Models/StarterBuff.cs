using System;
using System.Collections.Generic;

namespace xTest.Models
{
    public partial class StarterBuff
    {
        public int StarterBuffId { get; set; }
        public int StarterId { get; set; }
        public int BuffId { get; set; }
        public int FanId { get; set; }
    }
}
