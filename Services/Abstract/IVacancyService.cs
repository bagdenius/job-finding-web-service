using Models;

namespace Services.Abstract
{
    public interface IVacancyService
    {
        Task Add(VacancyModel vacancy);
        Task Remove(string id);
        Task<VacancyModel> Get(string id);
        Task<IEnumerable<string>> GetStrings();
        Task<IEnumerable<VacancyModel>> GetByUserId(string userId);
        Task<IEnumerable<VacancyModel>> GetTopVacancies(int count);
        Task<IList<VacancyModel>> Search(string query);
        Task Update(VacancyModel vacancy);
    }
}