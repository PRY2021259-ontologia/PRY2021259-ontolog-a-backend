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
    public class UserSuggestionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUserSuggestionsReturnsEmptyCollection()
        {
            var mockUserSuggestionRepository = GetDefaultIUserSuggestionRepositoryInstance();
            mockUserSuggestionRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<UserSuggestion>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserSuggestionService(mockUserSuggestionRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await service.ListAsync();
            var userSuggestionsCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, userSuggestionsCount);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserSuggestionNotFoundResponse()
        {
            // Arrange
            var mockUserSuggestionRepository = GetDefaultIUserSuggestionRepositoryInstance();
            var userSuggestionId = Guid.NewGuid();
            mockUserSuggestionRepository.Setup(r => r.GetById(userSuggestionId))
                .Returns(Task.FromResult<UserSuggestion>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserSuggestionService(mockUserSuggestionRepository.Object, mockUnitOfWork.Object);
            // Act
            UserSuggestionResponse result = await service.GetById(userSuggestionId);
            var message = result.Message;
            // Assert
            message.Should().Be("UserSuggestion Not Found");
        }

        private Mock<IUserSuggestionRepository> GetDefaultIUserSuggestionRepositoryInstance()
        {
            return new Mock<IUserSuggestionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
