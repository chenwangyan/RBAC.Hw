using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    /// <summary>
    /// 数据注解   []特性  fluentapi
    /// </summary>
    public class Admin : BaseClass
    {
        ///<summary>
        ///管理员Id
        ///</summary>
        public long AdminId { get; set; }

        ///<summary>
        ///用户名
        ///</summary>
        public string UserName { get; set; }

        ///<summary>
        ///密码
        ///</summary>
        public string Password { get; set; }

        ///<summary>
        ///邮箱
        ///</summary>
        public string Email { get; set; }

        ///<summary>
        ///末次登录时间
        ///</summary>
        public DateTime LastLoginTime { get; set; }

        ///<summary>
        ///末次登录IP
        ///</summary>
        public string LastLoginIP { get; set; }

        ///<summary>
        ///是否锁定
        ///</summary>
        public byte IsLock { get; set; }
    }
}
