using AutoMapper;
using DTO;
using Rbac.Entity;
using Repository;
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
        private readonly IMapper mapper;

        public ServiceRole(IRoleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

    }
}
