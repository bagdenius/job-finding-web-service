using BLL.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.VacancyTests
{
    public class RemoveVacancyTests : UnitOfWorkMock
    {
        [Fact]
        public async Task RemoveVacancy_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            await service.Remove(ContextMock.VacancyIdForDelete);

            // Assert
            Assert.Null(await Context.Vacancies.SingleOrDefaultAsync(vacancy =>
            vacancy.Id == ContextMock.VacancyIdForDelete));
        }

        [Fact]
        public async Task RemoveVacancy_FailedOnWrongId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Remove(Guid.NewGuid().ToString()));
        }
    }
}
