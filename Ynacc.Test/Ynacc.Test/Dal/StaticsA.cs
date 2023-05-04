using Microsoft.EntityFrameworkCore;

namespace Ynacc.Wage.Dal
{
    [Keyless]
    public partial class StaticsA
    {
        public string 工资号 { get; set; } = null!;
        public string? 姓名 { get; set; }
        public short 发生年份 { get; set; }
        public short 发生月份 { get; set; }
        public string? 请假类型 { get; set; }
        public short? 天数 { get; set; }
        public decimal? 补考勤扣款 { get; set; }
        public decimal? 补工作餐积点扣款 { get; set; }
        public int? 合计天数 { get; set; }
        public decimal? 合计补考勤扣款 { get; set; }
        public decimal? 合计补工作餐积点扣款 { get; set; }


    }
}
