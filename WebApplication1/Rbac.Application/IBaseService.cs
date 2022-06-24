using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Rbac.Application
{
    public interface IBaseService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : class, new()
    {
        int Add(TDto dto);
        int Update(TDto dto);
        int Delete(int id);
        TDto FindOne(int id);
        List<TDto> GetList();
    }
}
