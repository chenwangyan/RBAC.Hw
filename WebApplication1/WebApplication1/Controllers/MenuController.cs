using DTO;
using Rbac.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : BaseController<IServiceMenu, Menu, MenuDTO>
    {
        private readonly IServiceMenu menu;

        public MenuController(IServiceMenu menu) : base(menu)
        {
            this.menu = menu;
        }
    }
}
