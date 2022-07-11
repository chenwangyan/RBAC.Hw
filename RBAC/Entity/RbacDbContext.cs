using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    public class RbacDbContext : DbContext
    {
        public RbacDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(m => {
                m.Property(m => m.UserName).HasMaxLength(50).IsRequired();
                m.Property(m => m.Password).IsRequired();
                m.Property(m => m.Email).HasMaxLength(50).IsRequired();
                m.Property(m => m.LastLoginIP).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(m => {
                m.Property(m => m.RoleName).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<AdminRole>(m => {
                //设置外键
                m.HasOne<Admin>().WithMany().HasForeignKey(k => k.AdminId);
                m.HasOne<Role>().WithMany().HasForeignKey(k => k.RoleId);
            });

            modelBuilder.Entity<Menu>(m => {
                m.Property(m => m.MenuName).HasMaxLength(50).IsRequired();
                m.Property(m => m.LinkUrl).HasMaxLength(500).IsRequired();
            });

            modelBuilder.Entity<MenuRole>(m => {
                m.HasOne<Menu>().WithMany().HasForeignKey(k => k.MenuId);
                m.HasOne<Role>().WithMany().HasForeignKey(k => k.RoleId);
            });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
