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
using AutoMapper;
using Ontologia.API.Domain.Services;

namespace Ontologia.API.Test.UnitTest
{
    public class PlantDiseaseServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoPlantDiseasesReturnsEmptyCollection()
        {
            var mockPlantDiseaseRepository = GetDefaultIPlantDiseaseRepositoryInstance();
            mockPlantDiseaseRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<PlantDisease>());
            var mockSearchService = GetDefaultISearchServiceInstance();
            var mockMapperService = GetDefaultIMapperInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserConceptPlantDiseaseRepository = GetDefaultIUserConceptPlantDiseaseRepositoryInstance();
            var service = new PlantDiseaseService(mockPlantDiseaseRepository.Object, mockUnitOfWork.Object, mockUserConceptPlantDiseaseRepository.Object, mockSearchService.Object, mockMapperService.Object);

            // Act
            var result = await service.ListAsync();
            var plantDiseasesCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, plantDiseasesCount);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsPlantDiseaseNotFoundResponse()
        {
            // Arrange
            var mockPlantDiseaseRepository = GetDefaultIPlantDiseaseRepositoryInstance();
            var plantDiseaseId = Guid.NewGuid();
            mockPlantDiseaseRepository.Setup(r => r.GetById(plantDiseaseId))
                .Returns(Task.FromResult<PlantDisease>(null));
            var mockSearchService = GetDefaultISearchServiceInstance();
            var mockMapperService = GetDefaultIMapperInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserConceptPlantDiseaseRepository = GetDefaultIUserConceptPlantDiseaseRepositoryInstance();

            var service = new PlantDiseaseService(mockPlantDiseaseRepository.Object, mockUnitOfWork.Object, mockUserConceptPlantDiseaseRepository.Object, mockSearchService.Object, mockMapperService.Object);

            // Act
            PlantDiseaseResponse result = await service.GetById(plantDiseaseId);
            var message = result.Message;
            // Assert
            message.Should().Be("PlantDisease Not Found");
        }

        private Mock<IPlantDiseaseRepository> GetDefaultIPlantDiseaseRepositoryInstance()
        {
            return new Mock<IPlantDiseaseRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IUserConceptPlantDiseaseRepository> GetDefaultIUserConceptPlantDiseaseRepositoryInstance()
        {
            return new Mock<IUserConceptPlantDiseaseRepository>();
        }

        private Mock<ISearchService> GetDefaultISearchServiceInstance()
        {
            return new Mock<ISearchService>();
        }

        private Mock<IMapper> GetDefaultIMapperInstance()
        {
            return new Mock<IMapper>();
        }
    }
}
