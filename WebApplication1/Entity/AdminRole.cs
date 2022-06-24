using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    public class AdminRole
    {
        public int Id { get; set; }
        public long AdminId { get; set; }
        public int RoleId { get; set; }
    }
}
