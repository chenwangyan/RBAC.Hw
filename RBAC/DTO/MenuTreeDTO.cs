using Rbac.Entity;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class MenuTreeDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string LinkUrl { get; set; }
        public  List<MenuTreeDTO> children { get; set; }=new List<MenuTreeDTO>();
    }
}
