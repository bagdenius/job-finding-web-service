using AutoMapper;
using Database;
using Mappers;
using Repositories;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using UnitOfWorkSpace;
using UnitOfWorkSpace.Abstract;

namespace BLL.Tests.Common
{
    public class UnitOfWorkMock : IDisposable
    {
        protected readonly DatabaseContext Context;
        protected readonly IApplicationRepository ApplicationRepository;
        protected readonly IVacancyRepository VacancyRepository;
        protected readonly IUserRepository UserRepository;
        protected readonly IUnitOfWork UnitOfWork;
        public IMapper Mapper;

        public UnitOfWorkMock()
        {
            Context = ContextMock.Create();
            ApplicationRepository = new ApplicationRepository(Context);
            VacancyRepository = new VacancyRepository(Context);
            UserRepository = new UserRepository(Context);
            UnitOfWork = new UnitOfWork(Context, ApplicationRepository, VacancyRepository, UserRepository);
            var configurationProvider = new MapperConfiguration(cfg =>
            cfg.AddProfiles(new List<Profile>()
            {
                new ApplicationMapper(), new ResumeMapper(),
                new UserMapper(), new VacancyMapper()
            }));
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose() => ContextMock.Destroy(Context);
    }
}
