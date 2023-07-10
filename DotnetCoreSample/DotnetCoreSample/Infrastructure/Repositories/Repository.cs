using DotnetCoreSample.Core.Common;
using DotnetCoreSample.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSample.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase<Guid>
    {
        private ApplicationDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public Repository(ApplicationDbContext dataContext)
        {
            DbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            DbSet = dataContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await DbSet.SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual Guid Add(T entity)
        {
            EntityEntry<T> newEntity = DbSet.Add(entity);
            return newEntity.Entity.Id;
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
