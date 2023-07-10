using DotnetCoreSample.Core.Common;
using DotnetCoreSample.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Core.Interfaces.Services
{
    public interface IService<T> where T : EntityBase<Guid>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<Guid> Add(AddModel addModel);

        Task Update(UpdateModel updateModel);

        Task Delete(Guid id);
    }
}
