using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) =>
            _context = context;

        public async Task<IEnumerable<User>> Get() =>
             await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetAsync(string Id) =>
            await _context.Users.AsNoTracking().FirstOrDefaultAsync(v => v.Id == Id);

        public User Get(string Id) =>
            _context.Users.AsNoTracking().FirstOrDefault(v => v.Id == Id);

        public async Task Add(User user) =>
            await _context.Users.AddAsync(user);

        public async Task Update(User user) =>
            _context.Users.Update(user);

        public async Task Remove(string id)
        {
            var user = await GetAsync(id);
            _context.Users.Remove(user);
        }
    }
}
