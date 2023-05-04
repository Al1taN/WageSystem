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
    public class Audit02Controller : ControllerBase
    {
        private readonly OvertimeContext _context;

        public Audit02Controller(OvertimeContext context)
        {
            _context = context;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.audit02>>> GetData02(string deptID, int year, int month)
        {
            try
            {
                var result = await _context.audit02s.FromSqlInterpolated($"SELECT deptname 部门, tab1.pid 工资号, pname 姓名, expyear 发生年份, expmonth 发生月份, adddays 平均加班小时数, weekenddays 周末加班小时数, holidays 法定节日加班小时数, getamount 补加班工资,ondutydays 值班天数,ondutyamount 补值班费, remarks 备注,earlynum 早班次数,middlenum 中班次数,nightnum 夜班次数,alldaynum 昼夜次数,mealamount 补早中夜餐补贴,remarkss 备注2, flag_check 复核, flag_audit 审核,ISNULL((select 姓名 from dbo.人员 where 编号 = maker),maker) 经办人,(select 姓名 from dbo.人员 where 编号 = checker) 复核人,(select 姓名 from dbo.人员 where 编号 = auditer) 审核人 FROM dbo.imp_addnight tab1 left join dbo.dept tab2 on tab1.deptid = tab2.deptid where tab2.deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
        public async Task<int> Recheck02([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Pid = dict["Pid"].ToString();
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpAddnights.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Checker = Pid;
                    item.FlagCheck = true;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit02s.FromSqlInterpolated($"UPDATE dbo.imp_addnight SET flag_check = '1',checker = {pid} where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
        public async Task<int> Unrecheck02([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpAddnights.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Checker = null;
                    item.FlagCheck = null;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit02s.FromSqlInterpolated($"UPDATE dbo.imp_addnight SET flag_check = NULL,checker = NULL where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
        public async Task<int> Audit02([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Pid = dict["Pid"].ToString();
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpAddnights.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Auditer = Pid;
                    item.FlagAudit = true;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit02s.FromSqlInterpolated($"UPDATE dbo.imp_addnight SET flag_audit = '1',auditer = {pid} where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
        public async Task<int> Unaudit02([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var Deptid = dict["Deptid"].ToString();
                var Year = short.Parse(dict["Year"].ToString());
                var Month = short.Parse(dict["Month"].ToString());
                var appinfo = _context.ImpAddnights.Where(x => x.Deptid == Deptid && x.Year == Year && x.Month == Month);
                foreach (var item in appinfo)
                {
                    item.Auditer = null;
                    item.FlagAudit = null;
                }
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.audit02s.FromSqlInterpolated($"UPDATE dbo.imp_addnight SET flag_audit = NULL,auditer = NULL where deptid={deptID}  and year={year} and month={month} ").ToListAsync();
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
