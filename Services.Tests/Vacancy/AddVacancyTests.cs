using BLL.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Services.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.VacancyTests
{
    public class AddVacancyTests : UnitOfWorkMock
    {
        [Fact]
        public async Task AddApplication_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);
            var Id = Guid.NewGuid().ToString();
            var Title = "Title";
            var Description = "Description";
            var Location = "Location";
            var Sector = "Sector";
            var EmployerId = ContextMock.UserIdWithApplication;

            // Act
            await service.Add(new VacancyModel
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Location = Location,
                Sector = Sector,
                EmployerId = EmployerId
            });

            // Assert
            Assert.NotNull(
                await Context.Vacancies.SingleOrDefaultAsync(vacancy =>
                vacancy.Id == Id
                && vacancy.Title == Title
                && vacancy.Description == Description
                && vacancy.Location == Location
                && vacancy.Sector == Sector
                && vacancy.EmployerId == EmployerId));
        }

        [Fact]
        public async Task AddVacancy_FailedOnWrongEmployerId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);
            var EmployerId = Guid.NewGuid().ToString();

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Add(new VacancyModel { EmployerId = EmployerId }));
        }
    }
}
