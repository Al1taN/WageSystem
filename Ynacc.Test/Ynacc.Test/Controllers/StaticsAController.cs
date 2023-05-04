using System;
using System.Collections.Generic;
using System.Data;
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
    public class StaticsAController : ControllerBase
    {
        private readonly LateContext _context;

        public StaticsAController(LateContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Wage_Admin,Wage_Commiter,Wage_Checker,Wage_Auditer")]
        [HttpPost]
        //统计数据
        public async Task<ActionResult<List<Ynacc.Wage.Dal.StaticsA>>> GetStaticsAttend([FromBody] JsonElement Data)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(Data);
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(json);
                var ID = dict["ID"].ToString();
                var beginDay = DateTime.Parse(dict["beginDay"].ToString());
                var endDay = DateTime.Parse(dict["endDay"].ToString());
                var dept = dict["dept"].ToString();
                string Byear = beginDay.Year.ToString();
                string Bmonth = beginDay.Month.ToString();
                string Bday = beginDay.Day.ToString();
                string Eyear = endDay.Year.ToString();
                string Emonth = endDay.Month.ToString();
                string Eday = endDay.Day.ToString();
                Console.WriteLine(endDay);
                List<Ynacc.Wage.Dal.StaticsA> result = null;
                if (ID == "000000"){
                    if (dept == "00")
                    {
                        result = await _context.StaticsAs.FromSqlInterpolated($" select tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab1.latetype 请假类型,tab1.latedays2 天数, ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0')  合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,latetype, addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,latetype, addamount,addpoint,latedays1,latedays2 from imp_otherlate la  ) tab where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month) ) tab1   left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,addamount,addpoint,latedays1,latedays2 from imp_otherlate la ) tab  where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month)  group by tab.pid,tab.pname,tab.expyear,tab.expmonth) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                        return result;
                    }
                    //var result = await _context.StaticsAs.FromSqlInterpolated($" select  tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab2.latetype 请假类型,tab1.latedays2 天数,ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0') 合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,  convert(varchar (30),concat(ABS(la.expyear),'-',ABS(la.expmonth),'-',ABS(la.startdate)),111) startday, addamount,addpoint,latedays1,latedays2 from imp_otherlate la ) tab  where DATEDIFF(day,{beginDay},tab.startday)>=0 and DATEDIFF(day,dateadd(dd,tab.latedays1,tab.startday),{endDay})>=0 ) tab1  left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint,latetype from  (select pid,pname,la.expyear,la.expmonth, convert(varchar (30),concat(ABS(la.expyear),'-',ABS(la.expmonth),'-',ABS(la.startdate)),111) startday,  addamount,addpoint,latedays1,latedays2,latetype from imp_otherlate la ) tab  where DATEDIFF(day,{beginDay},tab.startday)>=0 and DATEDIFF(day,dateadd(dd,tab.latedays1,tab.startday),{endDay})>=0  group by tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.latetype) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                    result = await _context.StaticsAs.FromSqlInterpolated($" select tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab1.latetype 请假类型,tab1.latedays2 天数, ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0')  合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,latetype, addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,latetype, addamount,addpoint,latedays1,latedays2 from imp_otherlate la where deptid = {dept} ) tab where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month) ) tab1   left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,addamount,addpoint,latedays1,latedays2 from imp_otherlate la where deptid = {dept}) tab  where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month)  group by tab.pid,tab.pname,tab.expyear,tab.expmonth) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                    return result;
                }
                else {
                    if (dept == "00")
                    {
                        result = await _context.StaticsAs.FromSqlInterpolated($" select tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab1.latetype 请假类型,tab1.latedays2 天数, ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0')  合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,latetype, addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,latetype, addamount,addpoint,latedays1,latedays2 from imp_otherlate la where pid={ID} ) tab where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month) ) tab1   left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,addamount,addpoint,latedays1,latedays2 from imp_otherlate la where pid={ID}) tab  where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month)  group by tab.pid,tab.pname,tab.expyear,tab.expmonth) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                        return result;
                    }
                    //var result = await _context.StaticsAs.FromSqlInterpolated($" select  tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab2.latetype 请假类型,tab1.latedays2 天数,ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0') 合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,  convert(varchar (30),concat(ABS(la.expyear),'-',ABS(la.expmonth),'-',ABS(la.startdate)),111) as startday, addamount,addpoint,latedays1,latedays2 from imp_otherlate la where pid={ID}) tab  where DATEDIFF(day,{beginDay},tab.startday)>=0 and DATEDIFF(day,dateadd(dd,tab.latedays1,tab.startday),{endDay})>=0 ) tab1  left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint,latetype from  (select pid,pname,la.expyear,la.expmonth, convert(varchar (30),concat(ABS(la.expyear),'-',ABS(la.expmonth),'-',ABS(la.startdate)),111) as startday,  addamount,addpoint,latedays1,latedays2,latetype from imp_otherlate la where pid={ID}) tab  where DATEDIFF(day,{beginDay},tab.startday)>=0 and DATEDIFF(day,dateadd(dd,tab.latedays1,tab.startday),{endDay})>=0  group by tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.latetype) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                    result = await _context.StaticsAs.FromSqlInterpolated($" select tab1.pid 工资号,tab1.pname 姓名,tab1.expyear 发生年份,tab1.expmonth 发生月份,tab1.latetype 请假类型,tab1.latedays2 天数, ISNULL(tab1.addamount,'0') 补考勤扣款,ISNULL(tab1.addpoint,'0') 补工作餐积点扣款, tab2.sumdiffday 合计天数,ISNULL(tab2.sumaddamount,'0')  合计补考勤扣款,ISNULL(tab2.sumaddpoint,'0') 合计补工作餐积点扣款  from ( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,tab.addamount,latetype, addpoint,latedays2 from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,latetype, addamount,addpoint,latedays1,latedays2 from imp_otherlate la where pid={ID} and deptid = {dept} ) tab where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month) ) tab1   left join( select  tab.pid,tab.pname,tab.expyear,tab.expmonth,sum(tab.latedays2) sumdiffday,sum(tab.addamount) sumaddamount, sum(addpoint) sumaddpoint from  (select pid,pname,la.expyear,la.expmonth,la.year,la.month,la.startdate,addamount,addpoint,latedays1,latedays2 from imp_otherlate la where pid={ID} and deptid = {dept}) tab  where ({Byear} < year and {Eyear} > year) or ({Byear} < year and {Eyear} = year and {Emonth} >= month ) or ({Byear} = year and {Bmonth} <= month and {Eyear} > year) or({Byear} = year and {Bmonth} <= month and {Eyear} = year and {Emonth} >= month)  group by tab.pid,tab.pname,tab.expyear,tab.expmonth) tab2 ON tab1.expyear=tab2.expyear and tab1.expmonth=tab2.expmonth and tab1.pid=tab2.pid order by tab1.expyear,tab1.expmonth").ToListAsync();
                    return result;
                }
                //({Byear} < expyear and {Eyear} = year and {Emonth} > month) or ({Byear} < expyear and {Eyear} = year and {Emonth} = month and {Eday} >= enddate)
                //or({Byear} < expyear and {Eyear} > year) or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} > year) or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} > year)
                //or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} = year and {Emonth} > month) or ({Byear} = expyear and {Bmonth} < expmonth and {Eyear} = year and {Emonth} = month and {Eday} >= enddate)
                //or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} = year and {Emonth} > month)
                //or ({Byear} = expyear and {Bmonth} = expmonth and {Bday} <= startdate and {Eyear} = year and {Emonth} = month and {Eday} >= enddate)
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
