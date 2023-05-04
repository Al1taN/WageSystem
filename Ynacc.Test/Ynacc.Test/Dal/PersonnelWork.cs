using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Ynacc.Wage.Dal
{
    [Keyless]
    public partial class PersonnelWork
    {
        public string Pid { get; set; } = null!;
        public string Pname { get; set; } = null!;
        public string Deptid { get; set; } = null!;
        public string? Educationid { get; set; }
        public DateTime? Workdate { get; set; }
        public DateTime? Joindate { get; set; }
        public DateTime? Borndate { get; set; }
        public string? Psex { get; set; }
        public string? Nationid { get; set; }
        public string? Fullspell { get; set; }
        public string? Simspell { get; set; }
        public string? Remark { get; set; }
    }
}
