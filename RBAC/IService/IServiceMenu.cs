using System;
using Repository;
using Rbac.Entity;
using System.Collections.Generic;
using DTO;
using System.Linq;

namespace IService
{
    public interface IServiceMenu:IRepositoryT<Menu>
    {
        List<MenuDTO> GetMenuList();
        List<MenuAddDTO> GetMenuAdd();
        bool GetMenuAdd(Menu obj);
        bool GetMenuDel(Menu obj);
        bool GetMenuEdit(Menu obj);
    }
}
