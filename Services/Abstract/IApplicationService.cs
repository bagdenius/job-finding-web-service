using Models;

namespace Services.Abstract
{
    public interface IApplicationService
    {
        bool CheckApplication(string userId, string vacancyId);
        Task Add(ApplicationModel application);
        Task<IEnumerable<ApplicationModel>> FindByUserId(string userId);
        Task<ApplicationModel> Get(string id);
        IEnumerable<ApplicationModel> Get();
        Task<IEnumerable<string>> GetStringsAsync();
        Task Update(ApplicationModel application);
    }
}