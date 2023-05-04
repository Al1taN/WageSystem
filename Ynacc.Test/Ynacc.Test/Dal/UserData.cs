using Microsoft.EntityFrameworkCore;

namespace Ynacc.Wage.Dal
{
    [Keyless]
    public partial class UserData
    {
        public string Pid { get; set; } = null!;
        public string Pname { get; set; } = null!;
        public string Dept { get; set; } = null!;
        public string Prole { get; set; } = null!;
    }
}
