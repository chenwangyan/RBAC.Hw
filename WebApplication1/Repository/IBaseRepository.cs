using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBaseRepository<TEntity,TKey>
    {
        //添加
        int Add(TEntity entity);
        int Add(List<TEntity> entity);
        //删除
        int Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        int Delete(TKey key);
        //修改
        int Update(TEntity entity);
        //修改部分数据
        int Update(TEntity entity,params string[] ps);
        //查找
        List<TEntity> FindAll();
        TEntity FindOne(TKey key);
        //查找数据并不调动数据库  通过拉姆达表达式
        IQueryable<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate);
        //查找数据调用数据库  通过拉姆达表达式
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);



    }
}
