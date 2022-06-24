using System;
using System.Linq;

namespace Repository
{
    public interface IRepositoryT<T>
    {
        bool Add(T t);
        bool Remove(T t);
        bool Edit(T t);
        IQueryable<T> Show();
    }
}
