using DotnetCoreSample.Core.Entities.Country;
using DotnetCoreSample.Core.Interfaces.Services;
using Moq;
using System;

namespace DotnetCoreSample.Test.Mock
{
    public class CountryServiceMock
    {
        private readonly Mock<ICountryService> mockService;

        public Guid ValidId => Guid.Parse("3a931aff-f997-46d8-940b-da4de9e93c35");
        public ICountryService Service => mockService.Object;

        public CountryServiceMock()
        {
            mockService = new Mock<ICountryService>();
            mockService.Setup(p => p.GetById(It.Is<Guid>(id => id.Equals(ValidId)))).ReturnsAsync(() => CreateValidCountry());
        }

        private Country CreateValidCountry()
        {
            return new Country { Id = ValidId, Name = "Test Country" };
        }
    }
}
