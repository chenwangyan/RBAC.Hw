﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AdminDTO
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
        public string Password { get; set; }

        ///<summary>
        ///邮箱
        ///</summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
