using DotnetCoreSample.Core.Common;
using DotnetCoreSample.Core.Interfaces.Services;
using DotnetCoreSample.Core.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Api.Controllers
{
    public abstract class ApiControllerBase<TEntity, TAddModel, TUpdateModel> : ControllerBase
        where TEntity : EntityBase<Guid>
        where TAddModel : AddModel
        where TUpdateModel : UpdateModel
    {
        protected readonly IService<TEntity> service;

        protected ApiControllerBase(IService<TEntity> service)
        {
            this.service = service;
        }

        // GET api/[controller]
        [HttpGet]
        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            return await service.GetAll();
        }

        // GET api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            return await service.GetById(id);
        }

        // POST api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult<Guid>> Post([CustomizeValidator] TAddModel addModel)
        {
            return new CreatedResult(Request.Path, new { Id = await service.Add(addModel) });
        }

        // PUT api/[controller]/5
        [HttpPut("{id}")]
        public virtual async Task Put(Guid id, [CustomizeValidator] TUpdateModel updateModel)
        {
            await service.Update(updateModel);
        }

        // DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public virtual async Task Delete(Guid id)
        {
            await service.Delete(id);
        }
    }
}
