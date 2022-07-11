using AutoMapper;
using DTO;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NPOI.POIFS.Crypt;
using Org.BouncyCastle.Asn1.Ocsp;
using Rbac.Entity;
using Repository;
using Repository.AdminRoles;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application.AdminService
{
    public class ServiceAdmin : BaseService<Admin, AdminDTO>, IServiceAdmin
    {
        private readonly IAdminRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor accessor;
        private readonly IAdminRoleRepository adminRole;

        public ServiceAdmin(IAdminRepository repository,
            IMapper mapper,
            IConfiguration configuration, 
            IHttpContextAccessor accessor,
            IAdminRoleRepository adminRole
            ) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.accessor = accessor;
            this.adminRole = adminRole;
        }
        /// <summary>
        /// 为用户分配角色权限
        /// </summary>
        /// <param name="adminRole"></param>
        /// <returns></returns>
        public int GetAddAdminRole(AdminRoleDTO dto)
        {
            //先删除再添加
            adminRole.Delete(m=>m.AdminId==dto.AdminId);
            var list = dto.RoleId.Select(m => new AdminRole { AdminId=dto.AdminId,RoleId=m }).ToList(); 
            return adminRole.Add(list);
        }

        /// <summary>
        /// 返回已有的权限
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public List<AdminRoleResult> GetAdminRoleResult(int AdminId)
        {
            var list = adminRole.FindAll(m=>m.AdminId== AdminId);
            return mapper.Map<List<AdminRoleResult>>(list);
        }
       

        /// <summary>
        /// 添加用户登陆信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public LoginAddDTO LoginAdd(AdminDTO dto)
        {
            if(repository.GetList(m=>m.UserName.Trim().ToUpper()==dto.UserName).Count()>0)
            {
                return new LoginAddDTO()
                {
                    Code = 1,
                    Mes = "用户名已存在"
                };
            }
            dto.UserName=dto.UserName.Trim().ToUpper();
            //dto.Password = MD5Encrypt(dto.Password); 
            dto.Password = Md5(dto.Password);
            dto.CreateTime = DateTime.Now;
            if(repository.Add(mapper.Map<Admin>(dto))==1)
            {
                return new LoginAddDTO()
                {
                    Code = 0,
                    Mes = "添加成功"
                };
            }
            return new LoginAddDTO()
            {
                Code = 2,
                Mes = "添加失败"
            };
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public LoginResult GetLogin(LoginDTO dto)
        {
            //accessor.HttpContext.Request.Cookies.TryGetValue("CodeKey",out string value);
            var value = accessor.HttpContext.Request.Cookies["CodeKey"];
            if (dto.CodeMa.Trim().ToUpper() != value.Trim().ToUpper())
            {
                return new LoginResult() { Code = 3, Mes = "验证码不正确" };
            }
            var list = repository.GetList(m => m.UserName.ToUpper().Trim() == dto.UserName);
            //判断是否存在用户
            if (list.Count==0)
            {
                return new LoginResult() { Code = 1, Mes = "用户不存在" };
            }
            else
            {
                if(MD5Encrypt(dto.Password).Trim().ToLower() != list[0].Password.Trim())
                {
                    return new LoginResult() { Code = 2, Mes = "密码不对" };
                }
            }
            //生成Token令牌
            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, dto.UserName)
            };

            //JWT密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:key"]));

            //算法，签名证书
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //过期时间
            DateTime expires = DateTime.UtcNow.AddHours(10);

            //Payload负载
            var token = new JwtSecurityToken(
                issuer: configuration["JwtConfig:Issuer"], //发布者、颁发者
                audience: configuration["JwtConfig:Audience"],  //令牌接收者
                claims: claims, //自定义声明信息
                notBefore: DateTime.UtcNow,  //创建时间
                expires: expires,   //过期时间
                signingCredentials: cred
                );

            var handler = new JwtSecurityTokenHandler();

            //生成令牌
            string jwt = handler.WriteToken(token);

            return new LoginResult() { Code = 0, Mes = "登陆成功", LoginToken = jwt };
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string Md5(string val)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(val)).Select(x => x.ToString("x2")));
        }


        /// <summary>
        /// MD5加密工具
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位

            }
            return strbul.ToString();
        }

        /// <summary>
        /// 返回分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public Tuple<List<ResultAdminPage>, int> GetAdminPage(AdminPage page)
        {
            var list=repository.QueryList();
            if(!string.IsNullOrWhiteSpace(page.UserName))
            {
                list = list.Where(m => m.UserName.Contains(page.UserName));
            }
            int toTalCount=list.Count();
            list = list.OrderBy(m => m.AdminId).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            var slist = mapper.Map<List<ResultAdminPage>>(list.ToList());
            return new Tuple<List<ResultAdminPage>, int>(slist,toTalCount);
        }
    }
}
