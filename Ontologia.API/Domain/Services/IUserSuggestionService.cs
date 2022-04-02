using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserSuggestionService
    {
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task<UserSuggestionResponse> SaveAsync(UserSuggestion userSuggestion);
        Task<IEnumerable<UserSuggestion>> ListByUserId(Guid userId);
        Task<UserSuggestionResponse> GetById(Guid userSuggestionId);
        Task<UserSuggestionResponse> Update(Guid userSuggestionId, UserSuggestion userSuggestion);
        Task<UserSuggestionResponse> Delete(Guid userSuggestionId);
        Task<UserSuggestionResponse> AssignUserSuggestion(Guid userId, Guid userSuggestionId);
        Task<UserSuggestionResponse> UnassignUserSuggestion(Guid userId, Guid userSuggestionId);
    }
}
