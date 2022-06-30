using System;
using Repository;
using Rbac.Entity;
using System.Collections.Generic;
using DTO;
using System.Linq;

namespace Rbac.Application
{
    public interface IServiceMenu:IBaseService<Menu,MenuDTO>
    {
        #region
        //List<MenuDTO> GetMenuList();
        //List<MenuAddDTO> GetMenuAdd();
        //bool GetMenuAdd(Menu obj);
        //bool GetMenuDel(Menu obj);
        //bool GetMenuEdit(Menu obj);
        #endregion
        List<MenuTreeDTO> GetMenuList();
        List<MenuAddDTO> GetMenuAdd();
        bool GetMenuUpdOne(Menu obj);
        bool GetMenuEdit(MenuDTO obj);
        bool GetMenuAdd(MenuDTO obj);
    }
}
