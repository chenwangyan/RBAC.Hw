using DTO;
using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application.AdminService
{
    public interface IServiceAdmin:IBaseService<Admin,AdminDTO>
    {
        LoginAddDTO LoginAdd(AdminDTO dto);
        LoginResult GetLogin(LoginDTO dto);

        Tuple<List<ResultAdminPage>,int> GetAdminPage(AdminPage page);

        int GetAddAdminRole(AdminRoleDTO dto);
        List<AdminRoleResult> GetAdminRoleResult(int AdminId);
    }
}
