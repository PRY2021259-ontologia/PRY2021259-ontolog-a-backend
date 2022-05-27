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
    public class SuggestionTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoSuggestionTypesReturnsEmptyCollection()
        {
            var mockSuggestionTypeRepository = GetDefaultISuggestionTypeRepositoryInstance();
            mockSuggestionTypeRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<SuggestionType>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SuggestionTypeService(mockSuggestionTypeRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await service.ListAsync();
            var suggestionTypesCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, suggestionTypesCount);
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
