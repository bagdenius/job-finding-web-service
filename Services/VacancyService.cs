using AutoMapper;
using Entities;
using Models;
using Services.Abstract;
using Services.Exceptions;
using UnitOfWorkSpace.Abstract;

namespace Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacancyService(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<IEnumerable<string>> GetStrings()
        {
            return from job in await _unitOfWork.Vacancies.Get()
                   select $"{job.Id}," +
                   $"{job.EmployerId}," +
                   $"{job.Title}," +
                   $"{job.Sector}," +
                   $"{job.CreationDate}," +
                   $"{job.Location},";
        }

        public async Task<IEnumerable<VacancyModel>> GetTopVacancies(int count)
        {
            return _mapper.Map<IEnumerable<VacancyModel>>(await _unitOfWork.Vacancies.GetTopVacancies(count));
        }

        public async Task<IEnumerable<VacancyModel>> GetByUserId(string userId)
        {
            if (await _unitOfWork.Users.GetAsync(userId) == null)
                throw new NotFoundException(nameof(User), userId);
            return _mapper.Map<IEnumerable<VacancyModel>>(
                await _unitOfWork.Vacancies.GetByUserId(userId));
        }

        public async Task<IList<VacancyModel>> Search(string query)
        {
            return _mapper.Map<IList<VacancyModel>>(await _unitOfWork.Vacancies.Search(query));
        }

        public async Task<VacancyModel> Get(string Id)
        {
            var vacancy = await _unitOfWork.Vacancies.GetAsync(Id);
            if (vacancy == null)
                throw new NotFoundException(nameof(Vacancy), Id);
            return _mapper.Map<VacancyModel>(vacancy);
        }

        public async Task Add(VacancyModel vacancy)
        {
            if (await _unitOfWork.Users.GetAsync(vacancy.EmployerId) == null)
                throw new NotFoundException(nameof(User), vacancy.EmployerId);
            await _unitOfWork.Vacancies.Add(_mapper.Map<Vacancy>(vacancy));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(VacancyModel vacancy)
        {
            if (await _unitOfWork.Users.GetAsync(vacancy.EmployerId) == null)
                throw new NotFoundException(nameof(User), vacancy.EmployerId);
            var updateObj = await _unitOfWork.Vacancies.GetAsync(vacancy.Id);
            updateObj.Id = vacancy.Id;
            updateObj.Title = vacancy.Title;
            updateObj.Picture = vacancy.Picture;
            updateObj.Description = vacancy.Description;
            updateObj.Location = vacancy.Location;
            updateObj.Sector = vacancy.Sector;
            updateObj.EmployerId = vacancy.EmployerId;
            updateObj.CreationDate = vacancy.CreationDate;
            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(string Id)
        {
            var vacancy = _unitOfWork.Vacancies.Get(Id);
            if (vacancy == null)
                throw new NotFoundException(nameof(Vacancy), Id);
            await _unitOfWork.Vacancies.Remove(Id);
            await _unitOfWork.SaveAsync();
        }
    }
}
