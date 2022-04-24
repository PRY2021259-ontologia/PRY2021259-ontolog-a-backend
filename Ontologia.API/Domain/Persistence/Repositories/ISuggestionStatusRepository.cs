using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface ISuggestionStatusRepository
    {
        // General Methods
        Task AddAsync(SuggestionStatus suggestionStatus);
        Task<IEnumerable<SuggestionStatus>> ListAsync();
        Task<SuggestionStatus> GetById(Guid suggestionStatustId);
        void Update(SuggestionStatus suggestionStatus);
        void Remove(SuggestionStatus suggestionStatus);

        // Methods for StatusType Entity
        Task<IEnumerable<SuggestionStatus>> ListByStatusTypeIdAsync(Guid statusTypeId);
        Task AssingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId);
        Task UnassingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId);

        // Methods for UserSuggestion Entity
        Task<IEnumerable<SuggestionStatus>> ListByUserSuggestionIdAsync(Guid userSuggestionId);
        Task AssingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId);
        Task UnassingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId);
    }
}
