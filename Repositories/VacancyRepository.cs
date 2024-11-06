using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;

namespace Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly DatabaseContext _context;

        public VacancyRepository(DatabaseContext context) =>
            _context = context;

        public async Task<IEnumerable<Vacancy>> Get() =>
            await _context.Vacancies.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Vacancy>> GetTopVacancies(int count) =>
             await _context.Vacancies.AsNoTracking()
                .Include(p => p.Applications)
                .OrderByDescending(p => p.Applications.Count)
                .Take(count)
                .ToListAsync();

        public async Task<IEnumerable<Vacancy>> GetByUserId(string employerId) =>
            await _context.Vacancies.AsNoTracking()
            .Where(e => e.EmployerId == employerId)
            .Include(p => p.Applications).ToListAsync();

        public async Task<IList<Vacancy>> Search(string query)
        {
            List<Vacancy> jobs = new();
            if (!string.IsNullOrWhiteSpace(query))
            {
                jobs = (await Get()).Where(a =>
                        !string.IsNullOrEmpty(a.Title)
                        && a.Title.ToLower().Contains(query.ToLower()) ||
                        !string.IsNullOrEmpty(a.Sector)
                        && a.Sector.ToLower().Contains(query.ToLower()) ||
                        !string.IsNullOrEmpty(a.Location)
                        && a.Location.ToLower().Contains(query.ToLower()) ||
                        !string.IsNullOrEmpty(a.Description)
                        && a.Description.ToLower().Contains(query.ToLower())).ToList();
            }
            return jobs;
        }

        public async Task<Vacancy> GetAsync(string id) =>
            await _context.Vacancies.FirstOrDefaultAsync(v => v.Id == id);

        public Vacancy Get(string id) =>
            _context.Vacancies.AsNoTracking().FirstOrDefault(v => v.Id == id);

        public async Task Add(Vacancy vacancy) =>
            await _context.Vacancies.AddAsync(vacancy);

        public async Task Update(Vacancy vacancy) =>
            _context.Vacancies.Update(vacancy);

        public async Task Remove(string id)
        {
            var vacancy = await GetAsync(id);
            _context.Vacancies.Remove(vacancy);
        }
    }
}
