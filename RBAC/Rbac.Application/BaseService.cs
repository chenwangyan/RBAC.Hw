using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;

namespace Rbac.Application
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TDto : class,new()
        where TEntity : class,new()
    {
        private readonly IBaseRepository<TEntity, int> repository;
        private readonly IMapper mapper;

        public BaseService(IBaseRepository<TEntity, int> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
      

        public virtual int Add(TDto dto)
        {
            return repository.Add(mapper.Map<TEntity>(dto));
        }

        public virtual int Delete(int id)
        {
            return repository.Delete(id);
        }

        public virtual TDto FindOne(int id)
        {
            return mapper.Map<TDto>(repository.FindOne(id)); 
        }

        public virtual List<TDto> GetList()
        {
            return mapper.Map<List<TDto>>(repository.FindAll());
        }

        public int Update(TDto dto)
        {
            return repository.Update(mapper.Map<TEntity>(dto));
        }
    }
}
