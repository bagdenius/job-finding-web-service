using Entities;

namespace Repositories.Abstract
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<IEnumerable<User>> Get();
        Task<User> GetAsync(string Id);
        User Get(string Id);
        Task Remove(string id);
        Task Update(User user);
    }
}