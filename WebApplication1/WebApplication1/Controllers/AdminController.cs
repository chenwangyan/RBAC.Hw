using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.AdminService;
using Rbac.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminController : BaseController<IServiceAdmin, Admin, AdminDTO>
    {
        private readonly IServiceAdmin admin;

        public AdminController(IServiceAdmin admin) : base(admin)
        {
            this.admin = admin;
        }
        [HttpPost]
        public LoginAddDTO LoginAdd(AdminDTO dto)
        {
            return admin.LoginAdd(dto);
        }
        
    }
}
