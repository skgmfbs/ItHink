using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DotnetCoreSample.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private IDbContextTransaction transaction;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public void BeginTransaction()
        {
            transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
        }

        public bool IsInTransaction()
        {
            return transaction != null;
        }

        public void RollBack()
        {
            transaction.Rollback();
            transaction = null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        }
    }
}
