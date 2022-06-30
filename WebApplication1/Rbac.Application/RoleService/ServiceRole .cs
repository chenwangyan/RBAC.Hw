using AutoMapper;
using DTO;
using Rbac.Entity;
using Repository;
using Repository.MenuRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Application.RoleService
{
    public class ServiceRole : BaseService<Role, RoleDTO>, IServiceRole
    {
        private readonly IRoleRepository repository;
        private readonly IMenuRoleRepository menuRole;
        private readonly IMapper mapper;

        public ServiceRole(
            IRoleRepository repository, 
            IMenuRoleRepository menuRole, 
            IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.menuRole = menuRole;
            this.mapper = mapper;
        }

        /// <summary>
        /// 显示Role数据 +分页+查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Tuple<List<RoleDTO>,int> GetRolePage(RolePage dto)
        {
            var list = repository.QueryList();
            if(!string.IsNullOrWhiteSpace(dto.RoleName))
            {
                list = list.Where(m=>m.RoleName.Contains(dto.RoleName));
            }
            int totalCount=list.Count();
            list = list.OrderBy(m => m.RoleId).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize);
            var slist = mapper.Map<List<RoleDTO>>(list);
            return new Tuple<List<RoleDTO>, int>(slist,totalCount);
        }

        public int GetAddMenuRole(MenuRoleDTO dto)
        {
            var list = dto.MenuId.Select(m => new MenuRole { MenuId = m, RoleId = dto.RoleId }).ToList();
            return menuRole.Add(list);
        }
    }
}
