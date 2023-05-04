using Microsoft.EntityFrameworkCore;
namespace Ynacc.Wage.Dal
{
    [Keyless]
    public partial class audit03
    {

        public string 部门 { get; set; } = null!;
        public string 工资号 { get; set; } = null!;
        public string? 姓名 { get; set; }
        public string 缺勤类型 { get; set; } = null!;
        public short 缺勤年度 { get; set; }
        public short 缺勤月份 { get; set; }
        public short 缺勤开始日期 { get; set; }
        public short? 缺勤结束日期 { get; set; }
        public short? 缺勤天数含休息日 { get; set; }
        public short? 缺勤天数不含休息日 { get; set; }
        public decimal? 补考勤扣款 { get; set; }
        public decimal? 补工作餐积点扣款 { get; set; }
        public string? 备注 { get; set; }
        public bool? 复核 { get; set; }
        public bool? 审核 { get; set; }
        public string 经办人 { get; set; } = null!;
        public string? 复核人 { get; set; }
        public string? 审核人 { get; set; }

    }
}