using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IConceptTypeService
    {
        Task<IEnumerable<ConceptType>> ListAsync();
        Task<ConceptTypeResponse> GetByIdAsync(Guid id);
        Task<ConceptTypeResponse> SaveAsync(ConceptType conceptType);
        Task<ConceptTypeResponse> UpdateAsync(Guid id, ConceptType conceptType);
        Task<ConceptTypeResponse> DeleteAsync(Guid id);
    }
}
