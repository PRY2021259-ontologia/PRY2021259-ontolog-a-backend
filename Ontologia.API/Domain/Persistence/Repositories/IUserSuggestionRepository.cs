using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserSuggestionRepository
    {
        // General Methods
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task AddAsync(UserSuggestion userSuggestion);
        Task<UserSuggestion> GetById(Guid userSuggestionId);
        void Update(UserSuggestion userSuggestion);
        void Remove(UserSuggestion userSuggestion);

        // Methods for User Entity
        Task<IEnumerable<UserSuggestion>> ListByUserIdAsync(Guid userId);
        Task AssingUserSuggestion(Guid userId, Guid userSuggestionId);
        Task UnassingUserSuggestion(Guid userId, Guid userSuggestionId);

        // Methods for SuggestionType Entity
        Task<IEnumerable<UserSuggestion>> ListBySuggestionTypeIdAsync(Guid suggestionTypeId);
        Task AssingUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId);
        Task UnassingUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId);

    }
}
