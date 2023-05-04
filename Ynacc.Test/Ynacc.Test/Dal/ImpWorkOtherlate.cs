using System;
using System.Collections.Generic;

namespace Ynacc.Wage.Dal
{
    public partial class ImpWorkOtherlate
    {
        public string Deptid { get; set; } = null!;
        public string Pid { get; set; } = null!;
        public string? Pname { get; set; }
        public string Latetype { get; set; } = null!;
        public short Year { get; set; }
        public short Month { get; set; }
        public short Expyear { get; set; }
        public short Expmonth { get; set; }
        public short Startdate { get; set; }
        public short? Enddate { get; set; }
        public short? Latedays1 { get; set; }
        public short? Latedays2 { get; set; }
        public string? Remark { get; set; }
        public string Maker { get; set; } = null!;
        public string? Checker { get; set; }
        public string? Auditer { get; set; }
        public bool? FlagCheck { get; set; }
        public bool? FlagAudit { get; set; }
        public bool? FlagCommit { get; set; }
        public decimal? Addamount { get; set; }
        public decimal? Addpoint { get; set; }
    }
}
