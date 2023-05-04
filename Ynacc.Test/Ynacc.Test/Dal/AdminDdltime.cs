using System;
using System.Collections.Generic;

namespace Ynacc.Wage.Dal
{
    public partial class AdminDdltime
    {
        public int Idx { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public short DateTime { get; set; }
        public short WorkYear { get; set; }
        public short WorkMonth { get; set; }
        public short WorkDate { get; set; }
    }
}
