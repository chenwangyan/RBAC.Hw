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
                // ����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // ��ʹ���շ�
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // ����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //��� DistributedCache ����
            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                //���ù���ʱ��Ϊ5����
                option.IOTimeout = TimeSpan.FromMinutes(5);
            });

            //���JWTС��
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });

                //����Ȩ��С��
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //��header�����token�����ݵ���̨
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���)ֱ���������������Bearer {token}(ע������֮����һ���ո�) \"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            //ע��
            services.AddDbContext<RbacDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("sqlserver"));
            });

            services.AddAutoMapper(Assembly.Load("Rbac.Application"));

            //������
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

            //JWT����
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
            option =>
            {
                //Token��֤����
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtConfig:Issuer"],//������

                    //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtConfig:Audience"],//������

                    //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:key"])),

                    ValidateLifetime = true, //��֤��������

                    RequireExpirationTime = true, //����ʱ��

                    ClockSkew = TimeSpan.Zero   //ƽ������ƫ��ʱ��
                };
            }
            );

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    This lambda determines whether user consent for non - essential cookies is needed for a given request.
            //     options.CheckConsentNeeded = context => false;//�ر�GDPR�淶    
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

            //Ĭ��ʹ��https�˿�
            //app.UseHttpsRedirection();

            //ʹ��session
            app.UseSession();

            app.UseRouting();

            //����
            app.UseCors(option => { option.WithOrigins("http://localhost:8082").AllowAnyHeader().AllowAnyMethod().AllowCredentials();});

            //��֤�м��
            app.UseAuthentication();

            //��Ȩ�м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
