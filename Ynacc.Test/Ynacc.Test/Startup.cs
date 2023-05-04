using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ynacc.Wage.db;

namespace Ynacc.Wage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Register the OpenIddict validation components.
            services.AddOpenIddict()
                .AddValidation(options =>
                {
                options.SetIssuer(Configuration.GetValue<string>("Issuer"));
                    options.AddAudiences(Configuration.GetValue<string>("ClientId"));
                    options.UseIntrospection()
                          .SetClientId(Configuration.GetValue<string>("ClientId"))
                          .SetClientSecret(Configuration.GetValue<string>("ClientSecret"));
                options.UseSystemNetHttp();
                options.UseAspNetCore();
                });
            services.AddCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //
            services.AddDbContext<AdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<LateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<OvertimeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<LabourLateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<LabourOverContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TimeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PersonnelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PersonnelWorkContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DdltimeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "OpenId Manager API",
                    Description = "Manger OpenId"
                });
                // 为 Swagger JSON and UI设置xml文档注释路径,需打开“生成包含API文档的文件”并取消 1591 警告
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath ?? "", "Ynacc.Wage.xml");
                c.IncludeXmlComments(xmlPath);
            });
#endif
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            // Configure the HTTP request pipeline.
#if DEBUG
            // if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
#endif
            app.UseStaticFiles();
            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>("CorsHosts").Split(','));
                builder.WithMethods("GET", "POST");
                builder.WithHeaders("Authorization", "Content-Type");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapControllerRoute(
                name: "default",
                pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
