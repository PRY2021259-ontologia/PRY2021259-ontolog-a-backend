using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface ISuggestionTypeRepository
    {
        Task<IEnumerable<SuggestionType>> ListAsync();
        Task AddAsync(SuggestionType suggestionType);
        Task<SuggestionType> FindById(Guid id);
        void Update(SuggestionType suggestionType);
        void Remove(SuggestionType suggestionType);
    }
}
