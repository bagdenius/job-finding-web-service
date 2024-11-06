using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using System.Reflection;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IVacancyService, VacancyService>();
            services.AddAutoMapper(typeof(UserMapper).GetTypeInfo().Assembly);
            return services;
        }
    }
}
