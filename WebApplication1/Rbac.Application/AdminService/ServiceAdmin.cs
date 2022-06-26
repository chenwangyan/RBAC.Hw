using AutoMapper;
using DTO;
using NPOI.POIFS.Crypt;
using Rbac.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application.AdminService
{
    public class ServiceAdmin : BaseService<Admin, AdminDTO>, IServiceAdmin
    {
        private readonly IBaseRepository<Admin, int> repository;
        private readonly IMapper mapper;

        public ServiceAdmin(IAdminRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public LoginAddDTO LoginAdd(AdminDTO dto)
        {
            if(repository.GetList(m=>m.UserName.Trim().ToUpper()==dto.UserName).Count()>0)
            {
                return new LoginAddDTO()
                {
                    Code = 0,
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
                    Code = 1,
                    Mes = "添加成功"
                };
            }
            return new LoginAddDTO()
            {
                Code = 2,
                Mes = "添加失败"
            };
        }

        public LoginResult GetLogin(LoginDTO dto)
        {
            var list = repository.GetList(m => m.UserName.ToUpper().Trim() == dto.UserName);
            //判断是否存在用户
            if (list==null)
            {
                return new LoginResult() { Code = 0, Mes = "用户不存在" };
            }
            else
            {
                if(MD5Encrypt(dto.Password).Trim() != list[0].Password.Trim())
                {
                    return new LoginResult() { Code = 1, Mes = "密码不对" };
                }
            }

            return new LoginResult() { Code=2,Mes="登陆成功",LoginToken=""}
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

    }
}
