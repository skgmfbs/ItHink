using AutoMapper;
using DotnetCoreSample.Api.Startup;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace DotnetCoreSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddEntityFrameworkNpgsql(Configuration);
            services.AddEntityFrameworkInMemoryDatabase(Configuration);

            services.AddCors();

            services.AddMvc(
                config =>
                {
                    //config.Filters.Add(new AuthorizeFilter());
                    //config.Filters.Add(new ErrorFilterAttribute());
                }
            ).AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
            ).AddFluentValidation()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Singleton);
            services.AddCustomSettings(Configuration);
            return services.AddCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
