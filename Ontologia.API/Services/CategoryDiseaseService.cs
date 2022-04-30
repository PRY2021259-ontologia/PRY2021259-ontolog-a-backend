using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class CategoryDiseaseService : ICategoryDiseaseService
    {
        private readonly ICategoryDiseaseRepository _categoryDiseaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryDiseaseService(ICategoryDiseaseRepository categoryDiseaseRepository, IUnitOfWork unitOfWork)
        {
            _categoryDiseaseRepository = categoryDiseaseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDiseaseResponse> DeleteAsync(Guid id)
        {
            var existingCategoryDisease = await _categoryDiseaseRepository.FindById(id);

            if (existingCategoryDisease == null)
                return new CategoryDiseaseResponse("CategoryDisease Not Found");

            try
            {
                existingCategoryDisease.IsActive = false;
                _categoryDiseaseRepository.Update(existingCategoryDisease);
                //_categoryDiseaseRepository.Remove(existingCategoryDisease);
                await _unitOfWork.CompleteAsync();

                return new CategoryDiseaseResponse(existingCategoryDisease);
            }
            catch (Exception ex)
            {
                return new CategoryDiseaseResponse($"An error ocurred while deleting CategoryDisease: {ex.Message}");
            }
        }

        public async Task<CategoryDiseaseResponse> GetByIdAsync(Guid id)
        {
            var existingCategoryDisease = await _categoryDiseaseRepository.FindById(id);

            if (existingCategoryDisease == null || !existingCategoryDisease.IsActive)
                return new CategoryDiseaseResponse("CategoryDisease Not Found");

            return new CategoryDiseaseResponse(existingCategoryDisease);
        }

        public async Task<IEnumerable<CategoryDisease>> ListAsync()
        {
            var all = await _categoryDiseaseRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<CategoryDiseaseResponse> SaveAsync(CategoryDisease categoryDisease)
        {
            try
            {
                await _categoryDiseaseRepository.AddAsync(categoryDisease);
                await _unitOfWork.CompleteAsync();

                return new CategoryDiseaseResponse(categoryDisease);
            }
            catch (Exception ex)
            {
                return new CategoryDiseaseResponse($"An error ocurred while saving the CategoryDisease: {ex.Message}");
            }
        }

        public async Task<CategoryDiseaseResponse> UpdateAsync(Guid id, CategoryDisease categoryDisease)
        {
            var existingCategoryDisease = await _categoryDiseaseRepository.FindById(id);

            if (existingCategoryDisease == null)
                return new CategoryDiseaseResponse("CategoryDisease Not Found");

            existingCategoryDisease.CategoryDiseaseName = categoryDisease.CategoryDiseaseName;
            existingCategoryDisease.CategoryDiseaseDescription = categoryDisease.CategoryDiseaseDescription;
            existingCategoryDisease.IsActive = categoryDisease.IsActive;
            existingCategoryDisease.CreatedOn = categoryDisease.CreatedOn;
            existingCategoryDisease.ModifiedOn = categoryDisease.ModifiedOn;

            try
            {
                _categoryDiseaseRepository.Update(existingCategoryDisease);
                await _unitOfWork.CompleteAsync();

                return new CategoryDiseaseResponse(existingCategoryDisease);
            }
            catch (Exception ex)
            {
                return new CategoryDiseaseResponse($"An error ocurred while updating CategoryDisease: {ex.Message}");
            }
        }
    }
}
