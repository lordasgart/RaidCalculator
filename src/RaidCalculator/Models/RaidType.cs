using System;
using System.Collections.Generic;
using System.Text;

namespace RaidCalculator.Models
{
    public class RaidType
    {
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public int Level { get; set; }
    }
}
