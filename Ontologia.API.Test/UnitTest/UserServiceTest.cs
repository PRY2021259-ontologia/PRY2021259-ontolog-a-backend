using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services.Communications;
using Ontologia.API.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Ontologia.API.Test.UnitTest
{
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUsersReturnsEmptyCollection()
        {
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            mockUserRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<User>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            List<User> result = (List<User>)await service.ListAsync();
            int usersCount = result.Count;

            // Assert
            Assert.AreEqual(0, usersCount);
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
