using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginDTO
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }
        ///<summary>
        ///密码
        ///</summary>
        public string Password { get; set; }
        public string CodeMa { get; set; }
    }
}
