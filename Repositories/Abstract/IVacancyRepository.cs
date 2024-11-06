using Entities;

namespace Repositories.Abstract
{
    public interface IVacancyRepository
    {
        Task Add(Vacancy vacancy);
        Task Remove(string id);
        Task<Vacancy> GetAsync(string id);
        Vacancy Get(string id);
        Task<IEnumerable<Vacancy>> Get();
        Task<IEnumerable<Vacancy>> GetByUserId(string userId);
        Task<IEnumerable<Vacancy>> GetTopVacancies(int count);
        Task<IList<Vacancy>> Search(string query);
        Task Update(Vacancy vacancy);
    }
}