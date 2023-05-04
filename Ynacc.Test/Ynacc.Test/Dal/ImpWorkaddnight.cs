using System;
using System.Collections.Generic;

namespace Ynacc.Wage.Dal
{
    public partial class ImpWorkaddnight
    {
        public string Deptid { get; set; } = null!;
        public string Pid { get; set; } = null!;
        public string? Pname { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public short Expyear { get; set; }
        public short Expmonth { get; set; }
        public decimal? Adddays { get; set; }
        public decimal? Weekenddays { get; set; }
        public decimal? Holidays { get; set; }
        public decimal? Getamount { get; set; }
        public decimal? Ondutydays { get; set; }
        public decimal? Ondutyamount { get; set; }
        public string? Remarks { get; set; }
        public decimal? Earlynum { get; set; }
        public decimal? Middlenum { get; set; }
        public decimal? Nightnum { get; set; }
        public decimal? Alldaynum { get; set; }
        public decimal? Mealamount { get; set; }
        public string? Remarkss { get; set; }
        public string Maker { get; set; } = null!;
        public string? Checker { get; set; }
        public string? Auditer { get; set; }
        public bool? FlagCheck { get; set; }
        public bool? FlagAudit { get; set; }
        public bool? FlagCommit { get; set; }
    }
}
