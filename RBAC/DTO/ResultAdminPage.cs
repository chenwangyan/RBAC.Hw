using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   
    /// <summary>
    /// 数据注解   []特性  fluentapi
    /// </summary>
    public class ResultAdminPage : BaseClass
    {
        ///<summary>
        ///管理员Id
        ///</summary>
        public int AdminId { get; set; }

        ///<summary>
        ///用户名
        ///</summary>
        public string UserName { get; set; }

        ///<summary>
        ///密码
        ///</summary>

        ///<summary>
        ///邮箱
        ///</summary>
        public string Email { get; set; }

    }
}
