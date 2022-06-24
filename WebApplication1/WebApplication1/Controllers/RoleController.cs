using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.RoleService;
using Rbac.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : BaseController<IServiceRole, Role, RoleDTO>
    {
        private readonly IServiceRole role;

        public RoleController(IServiceRole Role) : base(Role)
        {
            role = Role;
        }
    }
}
