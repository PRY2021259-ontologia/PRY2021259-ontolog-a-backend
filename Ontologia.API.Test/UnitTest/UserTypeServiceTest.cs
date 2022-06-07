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
using System.Linq;

namespace Ontologia.API.Test.UnitTest
{
    public class UserTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUserTypesReturnsEmptyCollection()
        {
            var mockUserTypeRepository = GetDefaultIUserTypeRepositoryInstance();
            mockUserTypeRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<UserType>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserTypeService(mockUserTypeRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await service.ListAsync();
            var userTypesCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, userTypesCount);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserTypeNotFoundResponse()
        {
            // Arrange
            var mockUserTypeRepository = GetDefaultIUserTypeRepositoryInstance();
            var userTypeId = Guid.NewGuid();
            mockUserTypeRepository.Setup(r => r.FindById(userTypeId))
                .Returns(Task.FromResult<UserType>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserTypeService(mockUserTypeRepository.Object, mockUnitOfWork.Object);
            // Act
            UserTypeResponse result = await service.GetByIdAsync(userTypeId);
            var message = result.Message;
            // Assert
            message.Should().Be("UserType Not Found");
        }

        private Mock<IUserTypeRepository> GetDefaultIUserTypeRepositoryInstance()
        {
            return new Mock<IUserTypeRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
