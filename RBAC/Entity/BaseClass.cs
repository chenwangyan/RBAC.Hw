using System;

namespace Rbac.Entity
{
    public class BaseClass
    {
        public DateTime CreateTime { get; set; }
        public int CreateId { get; set; }
        public bool IsDelete { get; set; }
    }
}
