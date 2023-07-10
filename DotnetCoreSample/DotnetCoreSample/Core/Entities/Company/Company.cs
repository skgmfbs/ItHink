using DotnetCoreSample.Core.Common;
using System;

namespace DotnetCoreSample.Core.Entities.Company
{
    public class Company : EntityBase<Guid>
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Guid CountryId { get; set; }
        public Country.Country Country { get; set; }
    }
}
