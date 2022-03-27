using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUserSuggestionRepository
    {
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task AddAsync(UserSuggestion userSuggestion);
        Task<IEnumerable<UserSuggestion>> ListByUserIdAsync(int userId);
        Task<UserSuggestion> GetById(int userSuggestionId);
        Task AssingUserSuggestion(int userId, int userSuggestionId);
        Task UnassingUserSuggestion(int userId, int userSuggestionId);
        void Update(UserSuggestion userSuggestion);
        void Remove(UserSuggestion userSuggestion);
    }
}
