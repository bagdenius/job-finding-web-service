using Repositories.Abstract;

namespace UnitOfWorkSpace.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        IVacancyRepository Vacancies { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}
