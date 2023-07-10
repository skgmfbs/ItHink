using DotnetCoreSample.Core.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : EntityBase<Guid>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Guid Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
