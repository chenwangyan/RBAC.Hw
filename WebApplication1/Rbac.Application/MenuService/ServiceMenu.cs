using AutoMapper;
using DTO;
using Rbac.Entity;
using Repository;
using Repository.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework;

namespace Rbac.Application
{
    public class ServiceMenu : BaseService<Menu, MenuDTO>, IServiceMenu
    {
        private readonly IMenuRepository repository;
        private readonly IMapper mapper;

        public ServiceMenu(IMenuRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;   
        }

        /// <summary>
        /// 显示数据分支
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        public List<MenuTreeDTO> GetMenuList()
        {
            var list = repository.FindAll().ToList();
            //var menu = list.Where(m => m.ParentId == 0 && m.IsDelete == false).Select(m => new MenuDTO
            //{
            //    MenuId = m.MenuId,
            //    MenuName = m.MenuName,
            //    LinkUrl = m.LinkUrl,
            //}).ToList();
            var listone = list.Where(m => m.ParentId == 0 && m.IsDelete == false);
            var menu = mapper.Map<List<MenuTreeDTO>>(listone);

            NewMethod(list, menu);

            return menu;
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="list"></param>
        /// <param name="menu"></param>
        public void NewMethod(List<Menu> list, IEnumerable<MenuTreeDTO> menu)
        {
            foreach (var item in menu)
            {
                var FindList = list.Where(m => m.ParentId == item.MenuId && m.IsDelete == false);
                var mentDTO=mapper.Map<List<MenuTreeDTO>>(FindList);
                item.children.AddRange(mentDTO);
                NewMethod(list, mentDTO);
            }
        }


        /// <summary>
        ///  添加DTO
        /// </summary>
        /// <returns></returns>
        public List<MenuAddDTO> GetMenuAdd()
        {
            var list = repository.FindAll().ToList();
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
                Method(list, slist);
            }
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetMenuUpdOne(Menu obj)
        {
            var list = repository.FindAll().ToList();
            list = list.Where(m => m.IsDelete == false).ToList();
            list = list.Where(m => m.ParentId == obj.MenuId).ToList();
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return repository.UpdState(obj.MenuId)>0;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetMenuAdd(MenuDTO obj)
        {
            obj.CreateTime = DateTime.Now;
            obj.IsDelete = false;
            obj.CreateId = 0;
            return repository.Add(mapper.Map<Menu>(obj)) > 0;
        }
        /// <summary>
        /// 修改节点位置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool GetMenuEdit(MenuDTO obj)
        {
            if (obj.ParentId == 0)
            {
                string[] ps = { obj.MenuName,obj.LinkUrl};
                return repository.Update(mapper.Map<Menu>(obj), ps) > 0;
            }
            return repository.Update(mapper.Map<Menu>(obj)) > 0;
        }
    }
}
