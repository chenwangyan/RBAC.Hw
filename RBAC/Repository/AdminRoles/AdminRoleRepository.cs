using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AdminRoles
{
    public class AdminRoleRepository : BaseRepository<AdminRole, int>, IAdminRoleRepository
    {
        public AdminRoleRepository(RbacDbContext dbContext) : base(dbContext)
        {
        }
    }
}
