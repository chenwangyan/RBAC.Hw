using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MenuRoles
{
    public class MenuRoleRepository : BaseRepository<MenuRole,int>,IMenuRoleRepository
    {
        public MenuRoleRepository(RbacDbContext dbContext) : base(dbContext)
        {
        }
    }
}
