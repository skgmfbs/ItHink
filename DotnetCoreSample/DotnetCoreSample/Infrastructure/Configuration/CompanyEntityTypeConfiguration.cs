using DotnetCoreSample.Core.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace DotnetCoreSample.Infrastructure.Configuration
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LastModifiedOn);

            builder.Property(p => p.Address).HasConversion(new ValueConverter<Address, string>(
                value => value != null ? JsonConvert.SerializeObject(value) : null,
                value => value != null ? JsonConvert.DeserializeObject<Address>(value) : null)).HasColumnType("json");
            
            builder.Property(p => p.CountryId).IsRequired();
            builder.HasOne(p => p.Country);
        }
    }
}
