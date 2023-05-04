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
    public class DdlController : ControllerBase
    {
        private readonly DdltimeContext _context;

        public DdlController(DdltimeContext context)
        {
            _context = context;
        }
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.AdminDdltime>>> GetDdlTime()
        {
            try
            {
                var result = await _context.AdminDdltimes.FromSqlInterpolated($"SELECT * FROM dbo.Admin_ddltime").ToListAsync();
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
        public async Task<int> UpdateDdltime(short year, short month, short datetime, short workyear, short workmonth, short workdate)
        {
            try
            {
                var appinfo = await _context.AdminDdltimes.SingleOrDefaultAsync(x => x.Idx == 1);
                appinfo.Year = year;
                appinfo.Month = month;
                appinfo.DateTime = datetime;
                appinfo.WorkYear = workyear;
                appinfo.WorkMonth = workmonth;
                appinfo.WorkDate = workdate;
                Console.WriteLine(appinfo.Month);
                await _context.SaveChangesAsync();
                //需加上checker
                //var result = await _context.AdminTimes.FromSqlInterpolated($"UPDATE dbo.adminTime SET Year = {year},Month = {month} ").ToListAsync();
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
