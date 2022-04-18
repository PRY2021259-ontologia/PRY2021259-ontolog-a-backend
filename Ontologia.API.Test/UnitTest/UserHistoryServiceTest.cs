﻿using Ontologia.API.Domain.Models;
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
    public class UserHistoryServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserHistoryNotFoundResponse()
        {
            // Arrange
            var mockUserHistoryRepository = GetDefaultIUserHistoryRepositoryInstance();
            var userHistoryId = Guid.NewGuid();
            mockUserHistoryRepository.Setup(r => r.GetById(userHistoryId))
                .Returns(Task.FromResult<UserHistory>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserHistoryService(mockUserHistoryRepository.Object, mockUnitOfWork.Object);
            // Act
            UserHistoryResponse result = await service.GetById(userHistoryId);
            var message = result.Message;
            // Assert
            message.Should().Be("UserHistory Not Found");
        }

        private Mock<IUserHistoryRepository> GetDefaultIUserHistoryRepositoryInstance()
        {
            return new Mock<IUserHistoryRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
