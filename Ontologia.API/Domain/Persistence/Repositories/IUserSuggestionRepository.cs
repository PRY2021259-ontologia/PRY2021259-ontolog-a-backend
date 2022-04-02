using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserSuggestionRepository
    {
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task AddAsync(UserSuggestion userSuggestion);
        Task<IEnumerable<UserSuggestion>> ListByUserIdAsync(Guid userId);
        Task<UserSuggestion> GetById(Guid userSuggestionId);
        Task AssingUserSuggestion(Guid userId, Guid userSuggestionId);
        Task UnassingUserSuggestion(Guid userId, Guid userSuggestionId);
        void Update(UserSuggestion userSuggestion);
        void Remove(UserSuggestion userSuggestion);
    }
}
