using DotnetCoreSample.Core.Common;
using System;

namespace DotnetCoreSample.Core.Entities.Country
{
    public class Country : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
