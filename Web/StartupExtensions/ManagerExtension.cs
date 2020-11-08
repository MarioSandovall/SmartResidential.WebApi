using Business.Interfaces;
using Business.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Web.StartupExtensions
{
    public static class ManagerExtension
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IHouseManager, HouseManager>();
            services.AddTransient<IUserLoginManager, UserLoginManager>();
            services.AddTransient<IResidentialManager, ResidentialManager>();
            services.AddTransient<IAnnouncementManager, AnnouncementManager>();
            services.AddTransient<IExpenseCategoryManager, ExpenseCategoryManager>();
            services.AddTransient<IResidentialStatusManager, ResidentialStatusManager>();

            return services;

        }

    }
}
