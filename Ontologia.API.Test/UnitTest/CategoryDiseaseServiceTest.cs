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
    public class CategoryDiseaseServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryDiseaseNotFoundResponse()
        {
            // Arrange
            var mockCategoryDiseaseRepository = GetDefaultICategoryDiseaseRepositoryInstance();
            var CategoryDiseaseId = Guid.NewGuid();
            mockCategoryDiseaseRepository.Setup(r => r.FindById(CategoryDiseaseId))
                .Returns(Task.FromResult<CategoryDisease>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CategoryDiseaseService(mockCategoryDiseaseRepository.Object, mockUnitOfWork.Object);

            // Act
            CategoryDiseaseResponse result = await service.GetByIdAsync(CategoryDiseaseId);
            var message = result.Message;
            // Assert
            message.Should().Be("CategoryDisease Not Found");
        }

        private Mock<ICategoryDiseaseRepository> GetDefaultICategoryDiseaseRepositoryInstance()
        {
            return new Mock<ICategoryDiseaseRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
