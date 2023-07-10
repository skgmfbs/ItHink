using AutoMapper;
using DotnetCoreSample.Core.Services.Common.Profiles;

namespace DotnetCoreSample.Test.Helpers
{
    public static class AutoMapperProfileSettingsHelper
    {
        private static bool initial = false;

        public static void Initialize()
        {
            if (!initial)
            {
                initial = true;
                Mapper.Initialize(m =>
                {
                    m.AddProfile<CompanyProfile>();
                    m.AddProfile<CountryProfile>();
                });
            }
        }
    }
}
