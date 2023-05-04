using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Ynacc.Wage.Dal
{
    public partial class Admin
    {
        public string Pid { get; set; } = null!;
        public string Pname { get; set; } = null!;
        public string Psd { get; set; } = null!;
        public string Dept { get; set; } = null!;
        public string Prole { get; set; } = null!;
    }
}
