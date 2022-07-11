using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string LinkUrl { get; set; }
        public int ParentId { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateId { get; set; }
        public bool IsDelete { get; set; }
    }
}
