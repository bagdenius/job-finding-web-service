using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Abstract;
using UnitOfWorkSpace.Abstract;

namespace UnitOfWorkSpace
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IVacancyRepository, VacancyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddDbContext<DatabaseContext>(options =>
            //    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Resumes&Vacancies;Trusted_Connection=True;MultipleActiveResultSets=true"));
            return services;
        }
    }
}
