using DTO;
using Rbac.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Entity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : BaseController<IServiceMenu, Menu, MenuDTO>
    {
        private readonly IServiceMenu menu;

        public MenuController(IServiceMenu menu) : base(menu)
        {
            this.menu = menu;
        }
        [HttpGet]
        public List<MenuTreeDTO> GetMenuTreeList()
        {
            return menu.GetMenuList();
        }
        [HttpGet]
        public List<MenuAddDTO> GetMenuTreeAdd()
        {
            return menu.GetMenuAdd();
        }
        [HttpPost]
        public bool GetMenuUpdOne(Menu obj)
        {
            return menu.GetMenuUpdOne(obj);
        }
        [HttpPost]
        public bool GetMenuEdit(MenuDTO obj)
        {
            return menu.GetMenuEdit(obj);
        }
        [HttpPost]
        public bool GetMenuAdd(MenuDTO obj)
        {
            return menu.GetMenuAdd(obj);
        }
    }
}
