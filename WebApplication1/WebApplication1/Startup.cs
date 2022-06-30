using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rbac.Entity;
using Repository;
using NPOI.SS.Formula.Functions;
using Rbac.Application;
using Repository.Menus;
using System.Reflection;
using Rbac.Application.AdminService;
using Rbac.Application.RoleService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Rbac.Application.VFCode;
using Microsoft.AspNetCore.Http;
using Repository.MenuRoles;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
            .AddNewtonsoftJson(options => {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // 不使用驼峰
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 如字段为null值，该字段不会返回到前端
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //添加 DistributedCache 服务
            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                //设置过期时间为5分钟
                option.IOTimeout = TimeSpan.FromMinutes(5);
            });

            //添加JWT小锁
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });

                //开启权限小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //在header中添加token，传递到后台
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            //注册
            services.AddDbContext<RbacDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("sqlserver"));
            });

            services.AddAutoMapper(Assembly.Load("Rbac.Application"));

            //作用域
            #region
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAdminRepository,AdminRepository>();
            services.AddScoped<IMenuRoleRepository, MenuRoleRepository>();

            services.AddScoped<IServiceAdmin,ServiceAdmin>();
            services.AddScoped<IServiceMenu,ServiceMenu>();
            services.AddScoped<IServiceRole,ServiceRole>();

            services.AddScoped<IServiceCode, ServiceCode>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            #endregion

            //JWT令牌
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
            option =>
            {
                //Token验证参数
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtConfig:Issuer"],//发行人

                    //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtConfig:Audience"],//受众人

                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:key"])),

                    ValidateLifetime = true, //验证生命周期

                    RequireExpirationTime = true, //过期时间

                    ClockSkew = TimeSpan.Zero   //平滑过期偏移时间
                };
            }
            );

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    This lambda determines whether user consent for non - essential cookies is needed for a given request.
            //     options.CheckConsentNeeded = context => false;//关闭GDPR规范    
            //     options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            //默认使用https端口
            //app.UseHttpsRedirection();

            //使用session
            app.UseSession();

            app.UseRouting();

            //跨域
            app.UseCors(option => { option.WithOrigins("http://localhost:8082").AllowAnyHeader().AllowAnyMethod().AllowCredentials();});

            //认证中间件
            app.UseAuthentication();

            //授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
