using NPOI.SS.Formula.Functions;
using Rbac.Entity;
using System;
using System.Linq;

namespace Repository
{
    public class RepositoryT<T> : IRepositoryT<T>  where T : class
    {
        public RbacDbContext DbContext { get; }

        public RepositoryT(RbacDbContext dbContext)
        {
            DbContext = dbContext;
        }
      
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(T t)
        {
            DbContext.Add(t);
            return DbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Edit(T t)
        {
            DbContext.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //DbContext.Set<T>().Where(m=>);
            return DbContext.SaveChanges() > 0;
        }
        //可修改单个字段
        #region
        //public bool Update(T t, params string[] ps)
        //{
        //    var en = _dbContext.Entry<T>(t);
        //    en.State = EntityState.Unchanged;
        //    foreach (var item in ps)
        //    {
        //        en.Property(item).IsModified = true;
        //    }
        //    return _dbContext.SaveChanges() > 0;
        //}
        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Remove(T t)
        {
            DbContext.Attach(t);
            DbContext.Remove(t);
            return DbContext.SaveChanges() > 0;
        }

        

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public System.Linq.IQueryable<T> Show()
        {
            var list=DbContext.Set<T>().AsQueryable();
            return list;
        }
    }
}
