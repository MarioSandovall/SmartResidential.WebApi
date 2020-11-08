using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.StartupExtensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddResidentialDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ResidentialContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Residential")));
        }
    }
}
