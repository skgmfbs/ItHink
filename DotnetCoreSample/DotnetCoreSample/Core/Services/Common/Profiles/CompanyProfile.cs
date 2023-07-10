using AutoMapper;
using DotnetCoreSample.Core.Services.Company;
using System;

namespace DotnetCoreSample.Core.Services.Common.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<AddCompany, Entities.Company.Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<UpdateCompany, Entities.Company.Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.Condition(x => x.CountryId.GetValueOrDefault() != Guid.Empty))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(x => x.CountryId));
        }
    }
}