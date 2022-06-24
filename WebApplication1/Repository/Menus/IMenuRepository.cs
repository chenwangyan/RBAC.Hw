using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Menus
{
    public interface IMenuRepository:IBaseRepository<Menu,int>
    {
        int UpdState(int MenuId);
    }
}
