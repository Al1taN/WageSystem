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
    public class Imp03Controller : ControllerBase
    {
        private readonly LabourLateContext _context;
        private readonly PersonnelWorkContext _context1;

        public Imp03Controller(LabourLateContext context, PersonnelWorkContext context1)
        {
            _context = context;
            _context1 = context1;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpPost]
        //查询数据
        public async Task<ActionResult<List<Ynacc.Wage.Dal.ImpLate03>>> GetWorkNewAttend([FromBody] JsonElement Data)
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
                List<Ynacc.Wage.Dal.ImpLate03> result = null;
                if (!checked01 && !checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where flag_check is NULL and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab1.deptid= {dept} and flag_check is NULL and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (!checked01 && checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where flag_check is NULL and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab1.deptid= {dept} and flag_check is NULL and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (checked01 && !checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where flag_check = {checked01} and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab1.deptid= {dept} and flag_check = {checked01} and flag_audit is NULL and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                if (checked01 && checked02)
                {
                    if (dept == "00")
                    {
                        result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where flag_check = {checked01} and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                        return result;
                    }
                    result = await _context.ImpLate03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab1.deptid= {dept} and flag_check = {checked01} and flag_audit = {checked02} and (({Byear} < year and {Eyear} = year and {Emonth} > month ) or ({Byear} < year and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or({Byear} < year and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} > year) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} > year) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} < month and {Eyear} = year and {Emonth} = month and {Eday} >= 8) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} > month) or ({Byear} = year and {Bmonth} = month and {Bday} <= 8 and {Eyear} = year and {Emonth} = month and {Eday} >= 8))").ToListAsync();
                }
                //var result = await _context.ImpLates.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注, flag_check 复核, flag_audit 审核 FROM dbo.imp_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where pid={ID} and flag_check = {checked01} and flag_audit = {checked02} and DATEDIFF(day,{begin},convert(varchar (30),concat(ABS(expyear),'-',ABS(expmonth),'-',ABS(startdate)),111))>=0 and DATEDIFF(day,dateadd(dd,latedays1,convert(varchar (30),concat(ABS(expyear),'-',ABS(expmonth),'-',ABS(startdate)),111)),{end})>=0 ").ToListAsync();
                return result;
                //({Byear} < expyear and {Eyear} = expyear and {Emonth} > month) or ({Byear} < expyear and {Eyear} = expyear and {Emonth} = month and {Eday} >= enddate)
                //or({Byear} < expyear and {Eyear} > expyear) or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} > expyear) or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} > expyear)
                //or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} = expyear and {Emonth} > month) or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} = expyear and {Emonth} = month and {Eday} >= enddate)
                //or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} = expyear and {Emonth} > month)
                //or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} = expyear and {Emonth} = month and {Eday} >= enddate)
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
        public async Task<string> ImpData03([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var item = JsonConvert.DeserializeObject<List<Dictionary<object, object>>>(json);
                using var transaction = _context.Database.BeginTransaction();
                var oldItem = _context.ImpWorkOtherlates.Where(x => x.Year == short.Parse(item[0]["Year"].ToString()) && x.Month == short.Parse(item[0]["Month"].ToString()) && x.Deptid == item[0]["Deptid"].ToString());
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

                    Ynacc.Wage.Dal.ImpWorkOtherlate appinfo = new Ynacc.Wage.Dal.ImpWorkOtherlate()
                    {
                        Deptid = dict["Deptid"].ToString(),
                        Pid = dict["Pid"].ToString(),
                        Pname = dict["Pname"].ToString(),
                        Latetype = dict["Latetype"].ToString(),
                        Year = short.Parse(dict["Year"].ToString()),
                        Month = short.Parse(dict["Month"].ToString()),
                        Expyear = short.Parse(dict["Expyear"].ToString()),
                        Expmonth = short.Parse(dict["Expmonth"].ToString()),
                        Startdate = short.Parse(dict["Startdate"].ToString()),
                        Enddate = short.Parse(dict["Enddate"].ToString()),
                        Latedays1 = short.Parse(dict["Latedays1"].ToString()),
                        Latedays2 = short.Parse(dict["Latedays2"].ToString()),
                        Remark = (dict.ContainsKey("Remark")) ? dict["Remark"].ToString() : null,
                        Maker = dict["Maker"].ToString(),
                        FlagCommit = true,
                        Addamount = (dict.ContainsKey("Addamount")) ? decimal.Parse(dict["Addamount"].ToString()) : null,
                        Addpoint = (dict.ContainsKey("Addpoint")) ? decimal.Parse(dict["Addpoint"].ToString()) : null,
                    };
                    await _context.ImpWorkOtherlates.AddAsync(appinfo);
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
