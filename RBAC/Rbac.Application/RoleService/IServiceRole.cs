using DTO;
using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application.RoleService
{
    public interface IServiceRole:IBaseService<Role,RoleDTO>
    {
        Tuple<List<RoleDTO>,int> GetRolePage(RolePage dto);
        int GetAddMenuRole(MenuRoleDTO dto);
        List<MenuRoleResult> GetMenuRoleResult(int RoleId);
    }
}
