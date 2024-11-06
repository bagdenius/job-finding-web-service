using BLL.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Services.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ApplicationTests
{
    public class AddApplicationTests : UnitOfWorkMock
    {
        [Fact]
        public async Task AddApplication_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var Id = Guid.NewGuid().ToString();
            var ApplicantId = ContextMock.UserIdWithApplication;
            var Name = "Application name";
            var IsApproved = false;
            var VacancyId = ContextMock.VacancyIdForUpdate;

            // Act
            await service.Add(new ApplicationModel
            {
                Id = Id,
                ApplicantId = ApplicantId,
                Name = Name,
                IsApproved = IsApproved,
                VacancyId = VacancyId
            });

            // Assert
            Assert.NotNull(
                await Context.Applications.SingleOrDefaultAsync(application =>
                application.Id == Id
                && application.ApplicantId == ApplicantId
                && application.Name == Name
                && application.IsApproved == IsApproved
                && application.VacancyId == VacancyId));
        }

        [Fact]
        public async Task AddApplication_FailedOnWrongApplicantId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var ApplicantId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Add(new ApplicationModel { ApplicantId = ApplicantId }));
        }

        [Fact]
        public async Task AddApplication_FailedOnWrongVacancyId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);
            var ApplicantId = ContextMock.UserIdWithApplication;
            var VacancyId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Add(new ApplicationModel
            {
                ApplicantId = ApplicantId,
                VacancyId = VacancyId
            }));
        }
    }
}
