using Rbac.Entity;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class MenuAddDTO
    {
        public int value { get; set; }
        public string label { get; set; }
        public  List<MenuAddDTO> children { get; set; }=new List<MenuAddDTO>();
    }
}
