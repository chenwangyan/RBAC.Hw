using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Rbac.Entity;

namespace Rbac.Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
             CreateMap<MenuDTO,Menu>().ReverseMap();
             CreateMap<MenuAddDTO,Menu>().ReverseMap();
             CreateMap<MenuTreeDTO,Menu>().ReverseMap();
             CreateMap<Admin,AdminDTO>().ReverseMap();
             CreateMap<Role,RoleDTO>().ReverseMap();
        }
    }
}
