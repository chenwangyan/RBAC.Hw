using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RoleDTO: BaseClass
    {
        ///<summary>
        ///角色Id
        ///</summary>
        public int RoleId { get; set; }

        ///<summary>
        ///角色名称
        ///</summary>
        public string RoleName { get; set; }
    }
}
