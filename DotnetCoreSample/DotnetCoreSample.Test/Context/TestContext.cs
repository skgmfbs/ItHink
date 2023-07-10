using DotnetCoreSample.Infrastructure;
using DotnetCoreSample.Test.Helpers;
using DotnetCoreSample.Test.Mock;
using DotnetCoreSample.Test.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace DotnetCoreSample.Test.Context
{
    public class TestContext : IDisposable
    {
        private DbConnection connection;

        public TestContext()
        {
        }
        
        public ServiceMock CreateServicesMock(ApplicationDbContext applicationDbContext = null)
        {
            return new ServiceMock(applicationDbContext);
        }

        #region Create Test Environment
        public void RunInTestEnvironment(Action<ServiceFactory> testCase)
        {
            using (var dbContext = CreateTestEnvironment())
            {
                var serviceFactory = new ServiceFactory(dbContext);
                testCase.Invoke(serviceFactory);
            }
        }
        private ApplicationDbContext CreateTestEnvironment()
        {
            AutoMapperProfileSettingsHelper.Initialize();

            ApplicationDbContext applicationDbContext = CreateDbContext();

            InitialDB(applicationDbContext);

            return applicationDbContext;
        }
        private ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(CreateDbContextOptions());
        }
        private DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;
        }
        private void InitialDB(ApplicationDbContext dbContext)
        {
            // Create the schema in the database
            dbContext.Database.EnsureCreated();

            // Create necessary test data here...
        }
        #endregion

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}

