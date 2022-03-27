using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserSuggestionService
    {
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task<UserSuggestionResponse> SaveAsync(UserSuggestion userSuggestion);
        Task<IEnumerable<UserSuggestion>> ListByUserId(int userId);
        Task<UserSuggestionResponse> GetById(int userSuggestionId);
        Task<UserSuggestionResponse> Update(int userSuggestionId, UserSuggestion userSuggestion);
        Task<UserSuggestionResponse> Delete(int userSuggestionId);
        Task<UserSuggestionResponse> AssignUserSuggestion(int userId, int userSuggestionId);
        Task<UserSuggestionResponse> UnassignUserSuggestion(int userId, int userSuggestionId);
    }
}
