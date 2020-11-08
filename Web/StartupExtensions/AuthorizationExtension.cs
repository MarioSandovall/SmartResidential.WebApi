using Domain.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Web.StartupExtensions
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddPolicy(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationRoles.PolicyName,
                    authBuilder => authBuilder.RequireRole(AuthorizationRoles.AllRoles));
            });
        }

    }
}
