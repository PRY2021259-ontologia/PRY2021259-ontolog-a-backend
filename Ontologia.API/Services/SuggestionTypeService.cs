using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class SuggestionTypeService : ISuggestionTypeService
    {
        private readonly ISuggestionTypeRepository _suggestionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SuggestionTypeService(ISuggestionTypeRepository suggestionTypeRepository, IUnitOfWork unitOfWork)
        {
            _suggestionTypeRepository = suggestionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SuggestionTypeResponse> DeleteAsync(Guid id)
        {
            var existingSuggestionType = await _suggestionTypeRepository.FindById(id);

            if (existingSuggestionType == null)
                return new SuggestionTypeResponse("SuggestionType Not Found");

            try
            {
                _suggestionTypeRepository.Remove(existingSuggestionType);
                await _unitOfWork.CompleteAsync();

                return new SuggestionTypeResponse(existingSuggestionType);
            }
            catch (Exception ex)
            {
                return new SuggestionTypeResponse($"An error ocurred while deleting SuggestionType: {ex.Message}");
            }
        }

        public async Task<SuggestionTypeResponse> GetByIdAsync(Guid id)
        {
            var existingSuggestionType = await _suggestionTypeRepository.FindById(id);

            if (existingSuggestionType == null || !existingSuggestionType.IsActive)
                return new SuggestionTypeResponse("SuggestionType Not Found");

            return new SuggestionTypeResponse(existingSuggestionType);
        }

        public async Task<IEnumerable<SuggestionType>> ListAsync()
        {
            var all = await _suggestionTypeRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<SuggestionTypeResponse> SaveAsync(SuggestionType suggestionType)
        {
            try
            {
                await _suggestionTypeRepository.AddAsync(suggestionType);
                await _unitOfWork.CompleteAsync();

                return new SuggestionTypeResponse(suggestionType);
            }
            catch (Exception ex)
            {
                return new SuggestionTypeResponse($"An error ocurred while saving the SuggestionType: {ex.Message}");
            }
        }

        public async Task<SuggestionTypeResponse> UpdateAsync(Guid id, SuggestionType suggestionType)
        {
            var existingSuggestionType = await _suggestionTypeRepository.FindById(id);

            if (existingSuggestionType == null)
                return new SuggestionTypeResponse("SuggestionType Not Found");

            existingSuggestionType.SuggestionTypeName = suggestionType.SuggestionTypeName;
            existingSuggestionType.SuggestionTypeDescription = suggestionType.SuggestionTypeDescription;
            existingSuggestionType.IsActive = suggestionType.IsActive;
            existingSuggestionType.CreatedOn = suggestionType.CreatedOn;
            existingSuggestionType.ModifiedOn = suggestionType.ModifiedOn;

            try
            {
                _suggestionTypeRepository.Update(existingSuggestionType);
                await _unitOfWork.CompleteAsync();

                return new SuggestionTypeResponse(existingSuggestionType);
            }
            catch (Exception ex)
            {
                return new SuggestionTypeResponse($"An error ocurred while updating SuggestionType: {ex.Message}");
            }
        }
    }
}
