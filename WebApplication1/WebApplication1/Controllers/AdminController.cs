using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rbac.Application.AdminService;
using Rbac.Entity;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class AdminController : BaseController<IServiceAdmin, Admin, AdminDTO>
    {
        private readonly IServiceAdmin admin;

        public AdminController(IServiceAdmin admin) : base(admin)
        {
            this.admin = admin;
        }
        [HttpPost]
        public Tuple<List<ResultAdminPage>, int> GetAdminPage(AdminPage page)
        {
            return admin.GetAdminPage(page);
        }
    }
}
