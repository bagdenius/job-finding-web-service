using AutoMapper;
using Entities;
using Models;
using Services.Abstract;
using Services.Exceptions;
using UnitOfWorkSpace.Abstract;

namespace Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<IEnumerable<string>> GetStringsAsync()
        {
            return from application in await _unitOfWork.Applications.GetAsync()
                   select $"{application.Id}," +
                          $"{application.ApplicantId}," +
                          $"{application.Name}," +
                          $"{application.CreationDate}," +
                          $"{application.IsApproved}," +
                          $"{application.Vacancy.Title}," +
                          $"{application.Vacancy.CreationDate}," +
                          $"{application.Vacancy.EmployerId}";
        }

        public IEnumerable<ApplicationModel> Get()
        {
            return _mapper.Map<IEnumerable<ApplicationModel>>(_unitOfWork.Applications.Get());
        }

        public async Task<IEnumerable<ApplicationModel>> FindByUserId(string userId)
        {
            if (await _unitOfWork.Users.GetAsync(userId) == null)
                throw new NotFoundException(nameof(User), userId);
            return _mapper.Map<IEnumerable<ApplicationModel>>(
                await _unitOfWork.Applications.FindByUserId(userId));
        }

        public async Task<ApplicationModel> Get(string Id)
        {
            var application = await _unitOfWork.Applications.GetAsync(Id);
            if (application == null)
                throw new NotFoundException(nameof(Application), Id);
            return _mapper.Map<ApplicationModel>(application);
        }

        public async Task Add(ApplicationModel application)
        {
            if (await _unitOfWork.Users.GetAsync(application.ApplicantId) == null)
                throw new NotFoundException(nameof(User), application.ApplicantId);
            if (await _unitOfWork.Vacancies.GetAsync(application.VacancyId) == null)
                throw new NotFoundException(nameof(Vacancy), application.VacancyId);
            await _unitOfWork.Applications.Add(_mapper.Map<Application>(application));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(ApplicationModel application)
        {
            if (await _unitOfWork.Users.GetAsync(application.ApplicantId) == null)
                throw new NotFoundException(nameof(User), application.ApplicantId);
            if (await _unitOfWork.Vacancies.GetAsync(application.VacancyId) == null)
                throw new NotFoundException(nameof(Vacancy), application.VacancyId);
            var app = await _unitOfWork.Applications.GetAsync(application.Id);
            app.IsApproved = application.IsApproved;
            await _unitOfWork.SaveAsync();
        }

        public bool CheckApplication(string userId, string vacancyId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new NotFoundException(nameof(User), userId);
            if (_unitOfWork.Vacancies.Get(vacancyId) == null)
                throw new NotFoundException(nameof(Vacancy), vacancyId);
            return _unitOfWork.Applications.CheckApplication(userId, vacancyId);
        }
    }
}
