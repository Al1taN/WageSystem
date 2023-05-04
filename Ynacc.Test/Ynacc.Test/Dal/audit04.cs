using Microsoft.EntityFrameworkCore;
namespace Ynacc.Wage.Dal
{
    [Keyless]
    public class audit04
    {
        public string 部门 { get; set; } = null!;
        public string 工资号 { get; set; } = null!;
        public string? 姓名 { get; set; }
        public short 发生年份 { get; set; }
        public short 发生月份 { get; set; }
        public decimal? 平均加班小时数 { get; set; }
        public decimal? 周末加班小时数 { get; set; }
        public decimal? 法定节日加班小时数 { get; set; }
        public decimal? 补加班工资 { get; set; }
        public decimal? 值班天数 { get; set; }
        public decimal? 补值班费 { get; set; }
        public string? 备注 { get; set; }
        public decimal? 早班次数 { get; set; }
        public decimal? 中班次数 { get; set; }
        public decimal? 夜班次数 { get; set; }
        public decimal? 昼夜次数 { get; set; }
        public decimal? 补早中夜餐补贴 { get; set; }
        public string? 备注2 { get; set; }
        public bool? 复核 { get; set; }
        public bool? 审核 { get; set; }
        public string 经办人 { get; set; } = null!;
        public string? 复核人 { get; set; }
        public string? 审核人 { get; set; }

    }
}
