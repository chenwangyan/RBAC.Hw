using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> option):base(option)
        {
        }

    }
}
