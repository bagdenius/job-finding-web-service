using BLL.Tests.Common;
using Models;
using Services;
using Services.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.VacancyTests
{
    public class GetVacancyListTests : UnitOfWorkMock
    {
        [Fact]
        public async Task GetVacancyStringsListAsync_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            var result = await service.GetStrings();

            // Assert
            result.Count().ShouldBe(3);
        }

        [Fact]
        public async Task GetTopVacancies_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            var result = await service.GetTopVacancies(2);

            // Assert
            result.ShouldBeOfType<List<VacancyModel>>();
            result.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetVacancyListByUserIdAsync_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            var result = await service.GetByUserId(ContextMock.UserIdWithApplication);

            // Assert
            result.ShouldBeOfType<List<VacancyModel>>();
            result.Count().ShouldBe(3);
        }

        [Fact]
        public async Task GetVacancyListByUserIdAsync_FailedOnWrongUserId()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.GetByUserId(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task SearchVacancies_Success()
        {
            // Arrange
            var service = new VacancyService(UnitOfWork, Mapper);

            // Act
            var result = await service.Search("Title");

            // Assert
            result.ShouldBeOfType<List<VacancyModel>>();
            result.Count().ShouldBe(3);
        }
    }
}
