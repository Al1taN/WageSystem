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
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace Ynacc.Wage.Controllers
{
    [Route("api/app/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminContext _context;

        public AdminController(AdminContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.UserData>>> GetUser(string pid, string psd)
        {
            try
            {
                Console.WriteLine(pid);
                var result = await _context.UserDatas.FromSqlInterpolated($"SELECT pid,pname,dept,prole FROM dbo.admin where pid ={pid} and psd ={psd}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Ynacc.Wage.Dal.UserData>>> Login([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                string Pid = dict["username"].ToString();
                string Psd = dict["password"].ToString();
                var result = await _context.UserDatas.FromSqlInterpolated($"SELECT pid,pname,dept,prole FROM dbo.admin where pid ={Pid} and psd ={Psd}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        //导入用户数据
        [HttpPost]
        public async Task<string> ImpUser([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                Console.WriteLine(dict["pid"]);
                //需加上checker
                //var result = await _context.Admins.FromSqlInterpolated($"INSERT INTO dbo.admin (pid,pname,psd,dept,prole) VALUES ({dict["pid"]},{dict["pname"]},{dict["psd"]},{dict["dept"]},{dict["prole"]}) ").ToListAsync();
                Ynacc.Wage.Dal.Admin appinfo = new Ynacc.Wage.Dal.Admin(){
                    Pid = dict["pid"].ToString(),
                    Pname = dict["pname"].ToString(),
                    Psd = dict["psd"].ToString(),
                    Dept = dict["dept"].ToString(),
                    Prole = dict["prole"].ToString()
                };
                await _context.Admins.AddAsync(appinfo);
                await _context.SaveChangesAsync();
                return "200";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return ex.Message;
            }
        }
    }
}
