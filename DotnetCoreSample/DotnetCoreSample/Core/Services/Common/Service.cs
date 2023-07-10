using AutoMapper;
using DotnetCoreSample.Core.Common;
using DotnetCoreSample.Core.Exceptions;
using DotnetCoreSample.Core.Interfaces.Repositories;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Services
{
    public class Service<T> : IService<T> where T : EntityBase<Guid>
    {
        private readonly IMapper mapper;
        protected readonly IRepository<T> repository;

        public Service(IRepository<T> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            return repository.GetAll();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await repository.GetById(id);
        }

        public virtual async Task<Guid> Add(AddModel addModel)
        {
            T entity = (T)Activator.CreateInstance(typeof(T));
            mapper.Map(addModel, entity);
            Guid id = repository.Add(entity);
            await (repository as Repository<T>).SaveChangesAsync();
            return id;
        }

        public virtual async Task Update(UpdateModel updateModel)
        {
            T entity = await GetById(updateModel.Id) ?? throw new EntityNotFoundException(updateModel.Id);
            mapper.Map(updateModel, entity);
            repository.Update(entity);
            await (repository as Repository<T>).SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            T entity = await GetById(id) ?? throw new EntityNotFoundException(id);
            repository.Delete(entity);
            await (repository as Repository<T>).SaveChangesAsync();
        }
    }
}
