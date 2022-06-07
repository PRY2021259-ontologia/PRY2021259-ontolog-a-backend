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
    public class SuggestionStatusServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoSuggestionStatussReturnsEmptyCollection()
        {
            var mockSuggestionStatusRepository = GetDefaultISuggestionStatusRepositoryInstance();
            mockSuggestionStatusRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SuggestionStatus>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SuggestionStatusService(mockSuggestionStatusRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await service.ListAsync();
            var suggestionStatussCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, suggestionStatussCount);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsSuggestionStatusNotFoundResponse()
        {
            // Arrange
            var mockSuggestionStatusRepository = GetDefaultISuggestionStatusRepositoryInstance();
            var suggestionStatusId = Guid.NewGuid();
            mockSuggestionStatusRepository.Setup(r => r.GetById(suggestionStatusId))
                .Returns(Task.FromResult<SuggestionStatus>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SuggestionStatusService(mockSuggestionStatusRepository.Object, mockUnitOfWork.Object);
            // Act
            SuggestionStatusResponse result = await service.GetById(suggestionStatusId);
            var message = result.Message;
            // Assert
            message.Should().Be("SuggestionStatus Not Found");
        }

        private Mock<ISuggestionStatusRepository> GetDefaultISuggestionStatusRepositoryInstance()
        {
            return new Mock<ISuggestionStatusRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
