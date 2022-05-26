using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class ConceptTypeService : IConceptTypeService
    {
        private readonly IConceptTypeRepository _conceptTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConceptTypeService(IConceptTypeRepository conceptTypeRepository, IUnitOfWork unitOfWork)
        {
            _conceptTypeRepository = conceptTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ConceptTypeResponse> DeleteAsync(Guid id)
        {
            var existingConceptType = await _conceptTypeRepository.FindById(id);

            if (existingConceptType == null)
                return new ConceptTypeResponse("ConceptType Not Found");

            try
            {
                existingConceptType.IsActive = false;
                _conceptTypeRepository.Update(existingConceptType);
                await _unitOfWork.CompleteAsync();

                return new ConceptTypeResponse(existingConceptType);
            }
            catch (Exception ex)
            {
                return new ConceptTypeResponse($"An error ocurred while deleting ConceptType: {ex.Message}");
            }
        }

        public async Task<ConceptTypeResponse> GetByIdAsync(Guid id)
        {
            var existingConceptType = await _conceptTypeRepository.FindById(id);

            if (existingConceptType == null || !existingConceptType.IsActive)
                return new ConceptTypeResponse("ConceptType Not Found");

            return new ConceptTypeResponse(existingConceptType);
        }

        public async Task<IEnumerable<ConceptType>> ListAsync()
        {
            var all = await _conceptTypeRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<ConceptTypeResponse> SaveAsync(ConceptType conceptType)
        {
            try
            {
                await _conceptTypeRepository.AddAsync(conceptType);
                await _unitOfWork.CompleteAsync();

                return new ConceptTypeResponse(conceptType);
            }
            catch (Exception ex)
            {
                return new ConceptTypeResponse($"An error ocurred while saving the conceptType: {ex.Message}");
            }
        }

        public async Task<ConceptTypeResponse> UpdateAsync(Guid id, ConceptType conceptType)
        {
            var existingConceptType = await _conceptTypeRepository.FindById(id);

            if (existingConceptType == null)
                return new ConceptTypeResponse("ConceptType Not Found");

            existingConceptType.ConceptTypeName = conceptType.ConceptTypeName;
            existingConceptType.ConceptTypeDescription = conceptType.ConceptTypeDescription;
            existingConceptType.IsActive = conceptType.IsActive;
            existingConceptType.CreatedOn = conceptType.CreatedOn;
            existingConceptType.ModifiedOn = conceptType.ModifiedOn;

            try
            {
                _conceptTypeRepository.Update(existingConceptType);
                await _unitOfWork.CompleteAsync();

                return new ConceptTypeResponse(existingConceptType);
            }
            catch (Exception ex)
            {
                return new ConceptTypeResponse($"An error ocurred while updating conceptType: {ex.Message}");
            }
        }
    }
}
