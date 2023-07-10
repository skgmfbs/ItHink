using DotnetCoreSample.Core.Settings;
using DotnetCoreSample.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCoreSample.Api.Startup
{
    public static class StartupExtension
    {
        public static void AddEntityFrameworkNpgsql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkNpgsql()
                .AddDbContextPool<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString));
        }

        public static void AddEntityFrameworkInMemoryDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseInMemoryDatabase("DotnetSampleDB"));
        }

        public static IServiceCollection AddCustomSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomSettings>(configuration.GetSection("CustomSettings"));
            return services;
        }

        /**
         * https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.2
         */
        public static IApplicationBuilder UseCustomForwarding(this IApplicationBuilder app)
        {
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false,
                ForwardLimit = null
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardOptions);
            return app;
        }
    }
}
