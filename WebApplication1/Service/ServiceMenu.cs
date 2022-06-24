using DTO;
using IService;
using Rbac.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework;

namespace Service
{
    public class ServiceMenu:RepositoryT<Menu>,IServiceMenu
    {
        public ServiceMenu(RbacDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 显示数据分支
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        public List<MenuDTO> GetMenuList()
        {
            var list = Show().ToList();
            var menu = list.Where(m => m.ParentId == 0 && m.IsDelete==false).Select(m => new MenuDTO
            {
                MenuId = m.MenuId,
                MenuName = m.MenuName,
                LinkUrl = m.LinkUrl
            }).ToList();

            NewMethod(list, menu);

            return menu;
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="list"></param>
        /// <param name="menu"></param>
        public void NewMethod(List<Menu> list, IEnumerable<MenuDTO> menu)
        {
            foreach (var item in menu)
            {
                var slist = list.Where(m => m.ParentId == item.MenuId && m.IsDelete==false).Select(m => new MenuDTO
                {
                    MenuId = m.MenuId,
                    MenuName = m.MenuName,
                    LinkUrl = m.LinkUrl
                }).ToList();
                item.children.AddRange(slist);
                NewMethod(list, slist);
            }
        }


        /// <summary>
        ///  添加DTO
        /// </summary>
        /// <returns></returns>
        public List<MenuAddDTO> GetMenuAdd()
        {
            var list = Show().ToList();
            var menu = list.Where(m => m.ParentId == 0 && m.IsDelete == false).Select(m => new MenuAddDTO
            {
                value = m.MenuId,
                label = m.MenuName,
            }).ToList();

            Method(list, menu);
            return menu;
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="list"></param>
        /// <param name="menu"></param>
        public void Method(List<Menu> list, List<MenuAddDTO> menu)
        {
            foreach (var item in menu)
            {
                var slist = list.Where(m => m.ParentId == item.value && m.IsDelete == false).Select(m => new MenuAddDTO
                {
                    value = m.MenuId,
                    label = m.MenuName
                }).ToList();
                item.children.AddRange(slist);
                Method(list,slist);
            }
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetMenuDel(Menu obj)
        {
            var list = DbContext.Menus.ToList();
            list = list.Where(m => m.IsDelete == false).ToList();
            list = list.Where(m => m.ParentId ==obj.MenuId).ToList();
            if(list.Count>0)
            {
                return false;
            }
            else
            {
                var sllist = DbContext.Menus.Find(obj.MenuId);
                sllist.IsDelete = true;
                return DbContext.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetMenuAdd(Menu obj)
        {
            obj.CreateTime=DateTime.Now;
            obj.IsDelete = false;
            obj.CreateId = 0;
            return Add(obj);
        }
        /// <summary>
        /// 修改节点位置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool GetMenuEdit(Menu obj)
        {

            //if(obj.ParentId==0)
            //{
            //    obj.ParentId = list.ParentId;
            //}
            if (obj.ParentId==0)
            {
                return DbContext.Menus.Where(m => obj.ParentId == 0 && m.MenuId==obj.MenuId).UpdateFromQuery(s => new Menu { MenuName = obj.MenuName, LinkUrl = obj.LinkUrl }) > 0;
            }
            //DbContext.Menus.Update()
            return Edit(obj);
        }
    }
}
