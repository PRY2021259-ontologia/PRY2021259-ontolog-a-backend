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
    public class SuggestionTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsSuggestionTypeNotFoundResponse()
        {
            // Arrange
            var mockSuggestionTypeRepository = GetDefaultISuggestionTypeRepositoryInstance();
            var suggestionTypeId = Guid.NewGuid();
            mockSuggestionTypeRepository.Setup(r => r.FindById(suggestionTypeId))
                .Returns(Task.FromResult<SuggestionType>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SuggestionTypeService(mockSuggestionTypeRepository.Object, mockUnitOfWork.Object);

            // Act
            SuggestionTypeResponse result = await service.GetByIdAsync(suggestionTypeId);
            var message = result.Message;
            // Assert
            message.Should().Be("SuggestionType Not Found");
        }

        private Mock<ISuggestionTypeRepository> GetDefaultISuggestionTypeRepositoryInstance()
        {
            return new Mock<ISuggestionTypeRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
