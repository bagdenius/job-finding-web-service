using Entities;

namespace Repositories.Abstract
{
    public interface IApplicationRepository
    {
        bool CheckApplication(string userId, string vacancyId);
        Task Add(Application application);
        Task<IEnumerable<Application>> FindByUserId(string userId);
        Task<Application> GetAsync(string id);
        IEnumerable<Application> Get();
        Task<IEnumerable<Application>> GetAsync();
        Task Update(Application application);
    }
}