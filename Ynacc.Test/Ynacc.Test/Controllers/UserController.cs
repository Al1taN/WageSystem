using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Polly;
using Ynacc.Wage;
using Ynacc.Wage.db;

namespace Ynacc.Wage.Controllers
{
    [Route("api/app/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.AdminWage>>> GetDept()
        {
            try
            {
                var staffcode = User.GetClaim("staffcode");
                var pid = staffcode[^6..];
                Console.WriteLine(pid);
                var result = await _context.AdminWages.FromSqlInterpolated($"SELECT * FROM dbo.Admin_wage where pid = {pid}").ToListAsync();
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
        [Authorize(Roles = "Wage_Admin")]
        [HttpPost]
        public async Task<int> ImpDept([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                Console.WriteLine(dict["pid"]);
                //需加上checker
                //var result = await _context.Admins.FromSqlInterpolated($"INSERT INTO dbo.admin (pid,pname,psd,dept,prole) VALUES ({dict["pid"]},{dict["pname"]},{dict["psd"]},{dict["dept"]},{dict["prole"]}) ").ToListAsync();
                Ynacc.Wage.Dal.AdminWage appinfo = new Ynacc.Wage.Dal.AdminWage()
                {
                    Pid = dict["pid"].ToString(),
                    Dept = dict["dept"].ToString()
                };
                await _context.AdminWages.AddAsync(appinfo);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 500;
            }
        }

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.AdminWage>>> GetWageUser()
        {
            try
            {
                var result = await _context.AdminWages.FromSqlInterpolated($"SELECT * FROM dbo.Admin_wage").ToListAsync();
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
        [Authorize(Roles = "Wage_Admin")]
        [HttpGet]
        public async Task<int> DeleteWageUser(string pid,string dept)
        {
            try
            {
                var user = _context.AdminWages.Where(x => x.Pid == pid.ToString() && x.Dept == dept.ToString()).FirstOrDefault();
                if(user != null) {
                    _context.Remove(user);
                }
                await _context.SaveChangesAsync();
                return 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 500;
            }
        }

    }
}

