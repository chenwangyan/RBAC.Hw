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
    }
}
