using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    public class Audit03Controller : ControllerBase
    {
        private readonly LabourLateContext _context;

        public Audit03Controller(LabourLateContext context)
        {
            _context = context;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.audit03>>> GetData03(string deptID, int year, int month)
        {
            try
            {
                var result = await _context.audit03s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, latetype 缺勤类型, expyear 缺勤年度, expmonth 缺勤月份, startdate 缺勤开始日期, enddate 缺勤结束日期, latedays1 缺勤天数含休息日, latedays2 缺勤天数不含休息日, ISNULL(addamount,'0') 补考勤扣款,ISNULL(addpoint,'0') 补工作餐积点扣款, remark 备注,flag_check 复核,flag_audit 审核,ISNULL((select 姓名 from dbo.人员 where 编号 = maker),maker) 经办人,(select 姓名 from dbo.人员 where 编号 = checker) 复核人,(select 姓名 from dbo.人员 where 编号 = auditer) 审核人 FROM dbo.imp_work_otherlate tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab2.deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
        [Authorize(Roles = "Wage_Admin,Wage_Checker")]
        [HttpPost]
        public async Task<int> Recheck03([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Pid = dict["Pid"].ToString();
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpWorkOtherlates.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Checker = Pid;
                    item.FlagCheck = true;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit03s.FromSqlInterpolated($"UPDATE dbo.imp_work_otherlate SET flag_check = '1',checker = {pid} where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Checker")]
        [HttpPost]
        public async Task<int> Unrecheck03([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpWorkOtherlates.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Checker = null;
                    item.FlagCheck = null;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit03s.FromSqlInterpolated($"UPDATE dbo.imp_work_otherlate SET flag_check = NULL,checker = NULL where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Auditer")]
        [HttpPost]
        public async Task<int> Audit03([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Pid = dict["Pid"].ToString();
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpWorkOtherlates.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Auditer = Pid;
                    item.FlagAudit = true;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit03s.FromSqlInterpolated($"UPDATE dbo.imp_work_otherlate SET flag_audit = '1',auditer = {pid} where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Auditer")]
        [HttpPost]
        public async Task<int> Unaudit03([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpWorkOtherlates.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Auditer = null;
                    item.FlagAudit = null;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit03s.FromSqlInterpolated($"UPDATE dbo.imp_work_otherlate SET flag_audit = NULL,auditer = NULL where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
