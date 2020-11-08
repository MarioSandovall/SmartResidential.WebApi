using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Settings;

namespace Web.StartupExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));
            services.Configure<EmailConfiguration>(configuration.GetSection("Email"));

            return services;
        }
    }
}
