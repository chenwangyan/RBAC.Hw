using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Menus
{
    public class MenuRepository : BaseRepository<Menu, int>, IMenuRepository
    {
        private readonly RbacDbContext dbContext;
        public MenuRepository(RbacDbContext dbContext) : base(dbContext)
        {
            this.dbContext=dbContext;
        }

        public int UpdState(int MenuId)
        {
            var list = dbContext.Menus.Find(MenuId);
            list.IsDelete = true;
            return dbContext.SaveChanges();
        }
    }
}
