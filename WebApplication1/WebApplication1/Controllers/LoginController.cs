using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.AdminService;
using Rbac.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        //private readonly IServiceAdmin admin;

        //public LoginController(IServiceAdmin admin) : base(admin)
        //{
        //    this.admin = admin;
        //}
        //[HttpPost]
        //public LoginAddDTO LoginAdd(AdminDTO dto)
        //{
        //    return admin.LoginAdd(dto);
        //}
    }   
}
