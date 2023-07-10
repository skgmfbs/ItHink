using DotnetCoreSample.Core.Entities.Company;
using System;

namespace DotnetCoreSample.Core.Services.Company
{
    public class AddCompany : AddModel
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Guid? CountryId { get; set; }
    }
}
