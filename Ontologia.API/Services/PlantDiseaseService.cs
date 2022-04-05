using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class PlantDiseaseService : IPlantDiseaseService
    {
        public readonly IPlantDiseaseRepository _plantDiseaseRepository;
        public readonly IUnitOfWork _unitOfWork;

        public PlantDiseaseService(IPlantDiseaseRepository plantDiseaseRepository, IUnitOfWork unitOfWork)
        {
            _plantDiseaseRepository = plantDiseaseRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods
        public async Task<PlantDiseaseResponse> SaveAsync(PlantDisease plantDisease)
        {
            try
            {
                await _plantDiseaseRepository.AddAsync(plantDisease);
                await _unitOfWork.CompleteAsync();

                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error while saving PlantDisease:{ex.Message}");
            }
        }

        public async Task<IEnumerable<PlantDisease>> ListAsync()
        {
            return await _plantDiseaseRepository.ListAsync();
        }

        public async Task<PlantDiseaseResponse> GetById(Guid plantDiseaseId)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null)
                return new PlantDiseaseResponse("PlantDisease not found");
            return new PlantDiseaseResponse(existingPlantDisease);
        }

        public async Task<PlantDiseaseResponse> Update(Guid plantDiseaseId, PlantDisease plantDisease)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null)
                return new PlantDiseaseResponse("PlantDisease not found");

            existingPlantDisease.Name = plantDisease.Name;
            existingPlantDisease.Description = plantDisease.Description;
            existingPlantDisease.IsActive = plantDisease.IsActive;
            existingPlantDisease.CreatedOn = plantDisease.CreatedOn;
            existingPlantDisease.ModifiedOn = plantDisease.ModifiedOn;

            try
            {
                _plantDiseaseRepository.Update(existingPlantDisease);
                await _unitOfWork.CompleteAsync();
                return new PlantDiseaseResponse(existingPlantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error while updating PlantDisease: {ex.Message}");
            }
        }

        public async Task<PlantDiseaseResponse> Delete(Guid plantDiseaseId)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null)
                return new PlantDiseaseResponse("PlantDisease not found");
            try
            {
                _plantDiseaseRepository.Remove(existingPlantDisease);
                await _unitOfWork.CompleteAsync();
                return new PlantDiseaseResponse(existingPlantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while deleting PlantDisease: {ex.Message}");
            }
        }

        // Methods for CategoryDisease Entity
        public async Task<IEnumerable<PlantDisease>> ListByConceptTypeId(Guid categoryDiseaseId)
        {
            return await _plantDiseaseRepository.ListByCategoryDiseaseIdAsync(categoryDiseaseId);
        }

        public async Task<PlantDiseaseResponse> AssingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId)
        {
            try
            {
                await _plantDiseaseRepository.AssingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
                await _unitOfWork.CompleteAsync();
                PlantDisease plantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while assigning PlantDisease to conceptType: {ex.Message}");
            }
        }

        public async Task<PlantDiseaseResponse> UnassingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId)
        {
            try
            {
                await _plantDiseaseRepository.UnassingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
                await _unitOfWork.CompleteAsync();
                PlantDisease plantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while unassigning PlantDisease to conceptType: {ex.Message}");
            }
        }
    }
}
