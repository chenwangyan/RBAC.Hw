using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.AdminService;
using Rbac.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceAdmin login;

        public LoginController(IServiceAdmin login)
        {
            this.login = login;
        }
        [HttpPost]
        public LoginResult GetLogin(LoginDTO dto)
        {
            return login.GetLogin(dto);
        }
        [HttpPost]
        public LoginAddDTO LoginAdd(AdminDTO dto)
        {
            return login.LoginAdd(dto);
        }
    }
}
