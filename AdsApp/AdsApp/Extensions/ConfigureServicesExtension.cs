using BL.Services;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdsApp.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureDAL(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddScoped<IAdRepository, AdRepository>();

            return services;
        }

        public static IServiceCollection ConfigureBL(this IServiceCollection services)
        {
            services.AddScoped<IAdService, AdService>();

            return services;
        }
    }
}
