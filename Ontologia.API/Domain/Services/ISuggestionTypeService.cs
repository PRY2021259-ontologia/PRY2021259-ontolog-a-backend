using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface ISuggestionTypeService
    {
        Task<IEnumerable<SuggestionType>> ListAsync();
        Task<SuggestionTypeResponse> GetByIdAsync(Guid id);
        Task<SuggestionTypeResponse> SaveAsync(SuggestionType suggestionType);
        Task<SuggestionTypeResponse> UpdateAsync(Guid id, SuggestionType suggestionType);
        Task<SuggestionTypeResponse> DeleteAsync(Guid id);
    }
}
