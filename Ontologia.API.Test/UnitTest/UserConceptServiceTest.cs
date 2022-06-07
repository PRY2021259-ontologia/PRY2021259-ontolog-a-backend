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
    public class UserConceptServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUserConceptsReturnsEmptyCollection()
        {
            var mockUserConceptRepository = GetDefaultIUserConceptRepositoryInstance();
            mockUserConceptRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<UserConcept>());

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserConceptPlantDiseaseRepository = GetDefaultIUserConceptPlantDiseaseRepositoryInstance();
            var service = new UserConceptService(mockUserConceptRepository.Object, mockUnitOfWork.Object, mockUserConceptPlantDiseaseRepository.Object);

            // Act
            var result = await service.ListAsync();
            var UserConceptsCount = result.ToList().Count;

            // Assert
            Assert.AreEqual(0, UserConceptsCount);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserConceptNotFoundResponse()
        {
            // Arrange
            var mockUserConceptRepository = GetDefaultIUserConceptRepositoryInstance();
            var userConceptId = Guid.NewGuid();
            mockUserConceptRepository.Setup(r => r.GetById(userConceptId))
                .Returns(Task.FromResult<UserConcept>(null));

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserConceptPlantDiseaseRepository = GetDefaultIUserConceptPlantDiseaseRepositoryInstance();

            var service = new UserConceptService(mockUserConceptRepository.Object, mockUnitOfWork.Object, mockUserConceptPlantDiseaseRepository.Object);

            // Act
            UserConceptResponse result = await service.GetById(userConceptId);
            var message = result.Message;

            // Assert
            message.Should().Be("UserConcept Not Found");
        }

        private Mock<IUserConceptRepository> GetDefaultIUserConceptRepositoryInstance()
        {
            return new Mock<IUserConceptRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IUserConceptPlantDiseaseRepository> GetDefaultIUserConceptPlantDiseaseRepositoryInstance()
        {
            return new Mock<IUserConceptPlantDiseaseRepository>();
        }
    }
}
