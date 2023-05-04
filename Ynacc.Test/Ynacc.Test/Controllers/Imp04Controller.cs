using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenIddict.Validation.AspNetCore;
using Polly;
using Ynacc.Wage;
using Ynacc.Wage.db;

namespace Ynacc.Wage.Controllers
{
    [Route("api/app/[action]")]
    [ApiController]
    public class Imp04Controller : ControllerBase
    {
        private readonly LabourOverContext _context;
        private readonly PersonnelWorkContext _context1;

        public Imp04Controller(LabourOverContext context, PersonnelWorkContext context1)
        {
            _context = context;
            _context1 = context1;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpPost]
        //查询数据
        public async Task<ActionResult<List<Ynacc.Wage.Dal.ImpLate04>>> GetWorkOvertime([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var checked01 = bool.Parse(dict["checked01"].ToString());
                var checked02 = bool.Parse(dict["checked02"].ToString());
                var begin = DateTime.Parse(dict["begin"].ToString());
                var end = DateTime.Parse(dict["end"].ToString());
                var dept = dict["dept"].ToString();
                string Byear = begin.Year.ToString();
                string Bmonth = begin.Month.ToString();
                string Bday = begin.Day.ToString();
                string Eyear = end.Year.ToString();
                string Emonth = end.Month.ToString();
                string Eday = end.Day.ToString();
                Console.WriteLine(checked02);
                List<Ynacc.Wage.Dal.ImpLate04> result = null;
                if (!checked01 && !checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where flag_check is NULL and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where tab1.deptid= {dept} and flag_check is NULL and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (!checked01 && checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where flag_check is NULL and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where tab1.deptid= {dept} and flag_check is NULL and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (checked01 && !checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where flag_check = {checked01} and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where tab1.deptid= {dept} and flag_check = {checked01} and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (checked01 && checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where flag_check = {checked01} and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate04s.FromSqlInterpolated($"  SELECT deptname 部门, tab1.pid 工资号, pname 姓名,year 导入年份,month 导入月份, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核 FROM dbo.imp_workaddnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid  where tab1.deptid= {dept} and flag_check = {checked01} and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter")]
        [HttpPost]
        public async Task<string> ImpData04([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var item = JsonConvert.DeserializeObject<List<Dictionary<object, object>>>(json);
                using var transaction = _context.Database.BeginTransaction();
                var oldItem = _context.ImpWorkaddnights.Where(x => x.Year == short.Parse(item[0]["Year"].ToString()) && x.Month == short.Parse(item[0]["Month"].ToString()) && x.Deptid == item[0]["Deptid"].ToString());
                foreach (var i in oldItem)
                {
                    _context.Remove(i);
                }
                foreach (var dict in item)
                {
                    //检测人员工资号是否与姓名匹配
                    string[] Pnames = _context1.PersonnelWorks.Where(x => x.Pid == dict["Pid"].ToString()).Select(a => a.Pname).ToArray();
                    if (Pnames[0] != dict["Pname"].ToString())
                    { return "工资号：" + dict["Pid"].ToString() + " 与姓名：" + dict["Pname"].ToString() + " 不匹配"; }

                    Ynacc.Wage.Dal.ImpWorkaddnight appinfo = new Ynacc.Wage.Dal.ImpWorkaddnight()
                    {
                        Deptid = dict["Deptid"].ToString(),
                        Pid = dict["Pid"].ToString(),
                        Pname = dict["Pname"].ToString(),
                        Year = short.Parse(dict["Year"].ToString()),
                        Month = short.Parse(dict["Month"].ToString()),
                        Expyear = short.Parse(dict["Expyear"].ToString()),
                        Expmonth = short.Parse(dict["Expmonth"].ToString()),
                        Adddays = (dict.ContainsKey("Adddays")) ? decimal.Parse(dict["Adddays"].ToString()) : null,
                        Weekenddays = (dict.ContainsKey("Weekenddays")) ? decimal.Parse(dict["Weekenddays"].ToString()) : null,
                        Holidays = (dict.ContainsKey("Holidays")) ? decimal.Parse(dict["Holidays"].ToString()) : null,
                        Getamount = (dict.ContainsKey("Getamount")) ? decimal.Parse(dict["Getamount"].ToString()) : null,
                        Ondutydays = (dict.ContainsKey("Ondutydays")) ? decimal.Parse(dict["Ondutydays"].ToString()) : null,
                        Ondutyamount = (dict.ContainsKey("Ondutyamount")) ? decimal.Parse(dict["Ondutyamount"].ToString()) : null,
                        Remarks = (dict.ContainsKey("Remarks")) ? dict["Remarks"].ToString() : null,
                        Earlynum = (dict.ContainsKey("Earlynum")) ? decimal.Parse(dict["Earlynum"].ToString()) : null,
                        Middlenum = (dict.ContainsKey("Middlenum")) ? decimal.Parse(dict["Middlenum"].ToString()) : null,
                        Nightnum = (dict.ContainsKey("Nightnum")) ? decimal.Parse(dict["Nightnum"].ToString()) : null,
                        Alldaynum = (dict.ContainsKey("Alldaynum")) ? decimal.Parse(dict["Alldaynum"].ToString()) : null,
                        Mealamount = (dict.ContainsKey("Mealamount")) ? decimal.Parse(dict["Mealamount"].ToString()) : null,
                        Remarkss = (dict.ContainsKey("Remarkss")) ? dict["Remarkss"].ToString() : null,
                        Maker = dict["Maker"].ToString(),
                        FlagCommit = true,
                    };
                    await _context.ImpWorkaddnights.AddAsync(appinfo);
                    await _context.SaveChangesAsync();
                }
                //var result = await _context.Admins.FromSqlInterpolated($"INSERT INTO dbo.admin (pid,pname,psd,dept,prole) VALUES ({dict["pid"]},{dict["pname"]},{dict["psd"]},{dict["dept"]},{dict["prole"]}) ").ToListAsync();
                transaction.Commit();
                return "200";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("another instance with the same key value"))
                {
                    return "错误：两条或多条语句主键重复";
                }
                if (ex.Message.Contains("Value was either too large or too small"))
                {
                    return "错误：字段长度不符合要求";
                }
                if (ex.Message.Contains("was not present in the dictionary"))
                {
                    return "错误：字段缺失或列名不正确";
                }
                if (ex.Message.Contains("Input string was not in a correct format"))
                {
                    return "错误：字段类型不符合要求";
                }
                return ex.Message;
            }
        }
    }
}
