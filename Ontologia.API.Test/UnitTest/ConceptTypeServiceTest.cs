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
    public class ConceptTypeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsConceptTypeNotFoundResponse()
        {
            // Arrange
            var mockConceptTypeRepository = GetDefaultIConceptTypeRepositoryInstance();
            var ConceptTypeId = Guid.NewGuid();
            mockConceptTypeRepository.Setup(r => r.FindById(ConceptTypeId))
                .Returns(Task.FromResult<ConceptType>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new ConceptTypeService(mockConceptTypeRepository.Object, mockUnitOfWork.Object);

            // Act
            ConceptTypeResponse result = await service.GetByIdAsync(ConceptTypeId);
            var message = result.Message;
            // Assert
            message.Should().Be("ConceptType Not Found");
        }

        private Mock<IConceptTypeRepository> GetDefaultIConceptTypeRepositoryInstance()
        {
            return new Mock<IConceptTypeRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
