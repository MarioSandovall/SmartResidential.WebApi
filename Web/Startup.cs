using AutoMapper;
using Business.AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Web.StartupExtensions;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddLocalization(option => option.ResourcesPath = "Resources/localization/Locales");

            services.AddAutoMapper(typeof(ResidentialProfile).GetTypeInfo().Assembly);

            services
                .AddResidentialDbContext(Configuration)
                .AddManagers()
                .AddRepositories()
                .AddServices()
                .AddConfigurations(Configuration);

            services
                .AddCorsPolicy()
                .AddPolicy()
                .AddAuthentication(Configuration);

            services.AddSwagger();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseRouting();

            app.UseCors("corsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.Uselocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
