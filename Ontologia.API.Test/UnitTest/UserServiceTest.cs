using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services.Communications;
using Ontologia.API.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace Ontologia.API.Test.UnitTest
{
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserNotFoundResponse()
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var userId = Guid.NewGuid();
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);
            // Act
            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;
            // Assert
            message.Should().Be("User Not Found");
        }

        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
