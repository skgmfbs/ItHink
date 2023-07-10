using AutoMapper;
using DotnetCoreSample.Core.Services.Country;

namespace DotnetCoreSample.Core.Services.Common.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<AddCountry, Entities.Country.Country>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<UpdateCountry, Entities.Country.Country>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}