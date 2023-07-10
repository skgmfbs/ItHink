using DotnetCoreSample.Core.Common;

namespace DotnetCoreSample.Core.Entities.Company
{
    public class Address : ValueObject
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
