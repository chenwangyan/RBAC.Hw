using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.RoleService;
using Rbac.Entity;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController<IServiceRole, Role, RoleDTO>
    {
        private readonly IServiceRole role;

        public RoleController(IServiceRole Role) : base(Role)
        {
            role = Role;
        }
        /// <summary>
        /// 角色显示
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Tuple<List<RoleDTO>, int> GetRolePage(RolePage dto)
        {
            return role.GetRolePage(dto);
        }
        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public int GetAddMenuRole(MenuRoleDTO dto)
        {
            return role.GetAddMenuRole(dto);
        }

        /// <summary>
        /// 返回已经拥有的权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<MenuRoleResult> GetMenuRoleResult(int RoleId)
        {
            return role.GetMenuRoleResult(RoleId);
        }
    }
}
