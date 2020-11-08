using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;

namespace Web.StartupExtensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRespository>();
            services.AddTransient<IHouseRepository, HouseRepository>();
            services.AddTransient<IUserLoginRepository, UserLoginRepository>();
            services.AddTransient<IResidentialRepository, ResidentialRepository>();
            services.AddTransient<IAnnouncementRepository, AnnouncementRepository>();
            services.AddTransient<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddTransient<IResidentialStatusRepository, ResidentialStatusRepository>();

            return services;
        }
    }
}
