using DotnetCoreSample.Core.Entities.Company;
using DotnetCoreSample.Core.Interfaces.Services;
using Moq;
using System;

namespace DotnetCoreSample.Test.Mock
{
    public class CompanyServiceMock
    {
        private readonly Mock<IService<Company>> mockService;

        public Guid ValidId => Guid.Parse("3a931aff-f997-46d8-940b-da4de9e93c35");
        public IService<Company> Service => mockService.Object;

        public CompanyServiceMock()
        {
            mockService = new Mock<IService<Company>>();
            mockService.Setup(p => p.GetById(It.Is<Guid>(id => id.Equals(ValidId)))).ReturnsAsync(() => CreateValidCompany());
        }

        private Company CreateValidCompany()
        {
            return new Company { Id = ValidId, Name = "Test Company" };
        }
    }
}
