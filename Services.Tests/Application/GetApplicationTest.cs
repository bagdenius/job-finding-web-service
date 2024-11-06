using BLL.Tests.Common;
using Models;
using Services;
using Services.Exceptions;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.ApplicationTests
{
    public class GetApplicationTest : UnitOfWorkMock
    {
        [Fact]
        public async Task GetApplication_Success()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            var result = await service.Get(ContextMock.ApplicationIdForUpdate);

            // Assert
            result.ShouldBeOfType<ApplicationModel>();
            result.Id.ShouldBe(ContextMock.ApplicationIdForUpdate);
            result.ApplicantId.ShouldBe(ContextMock.UserIdWithApplication);
            result.Name.ShouldBe("Name1");
            result.IsApproved.ShouldBe(false);
            result.VacancyId.ShouldBe(ContextMock.VacancyIdForUpdate);
        }

        [Fact]
        public async Task GetApplication_FailedOnWrongId()
        {
            // Arrange
            var service = new ApplicationService(UnitOfWork, Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await service.Get(Guid.NewGuid().ToString()));
        }
    }
}
