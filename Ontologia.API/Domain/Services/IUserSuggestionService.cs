using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserSuggestionService
    {
        // General Methods
        Task<IEnumerable<UserSuggestion>> ListAsync();
        Task<UserSuggestionResponse> SaveAsync(UserSuggestion userSuggestion);
        Task<UserSuggestionResponse> GetById(Guid userSuggestionId);
        Task<UserSuggestionResponse> Update(Guid userSuggestionId, UserSuggestion userSuggestion);
        Task<UserSuggestionResponse> Delete(Guid userSuggestionId);

        // Methods for User Entity
        Task<IEnumerable<UserSuggestion>> ListByUserId(Guid userId);
        Task<UserSuggestionResponse> AssignUserSuggestion(Guid userId, Guid userSuggestionId);
        Task<UserSuggestionResponse> UnassignUserSuggestion(Guid userId, Guid userSuggestionId);

        // Methods for SuggestionType Entity

        Task<IEnumerable<UserSuggestion>> ListBySuggestionTypeId(Guid suggestionTypeId);
        Task<UserSuggestionResponse> AssignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId);
        Task<UserSuggestionResponse> UnassignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId);
    }
}
