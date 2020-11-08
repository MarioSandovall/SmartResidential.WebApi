using Microsoft.Extensions.DependencyInjection;

namespace Web.StartupExtensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(option =>
            {
                option.AddPolicy("corsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
