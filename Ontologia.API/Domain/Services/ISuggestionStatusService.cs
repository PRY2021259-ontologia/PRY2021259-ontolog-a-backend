using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface ISuggestionStatusService
    {
        // General Methods
        Task<SuggestionStatusResponse> SaveAsync(SuggestionStatus SuggestionStatus);
        Task<IEnumerable<SuggestionStatus>> ListAsync();
        Task<SuggestionStatusResponse> GetById(Guid suggestionStatusId);
        Task<SuggestionStatusResponse> Update(Guid suggestionStatusId, SuggestionStatus suggestionStatus);
        Task<SuggestionStatusResponse> Delete(Guid suggestionStatusId);

        // Methods for StatusType Entity
        Task<IEnumerable<SuggestionStatus>> ListByStatusTypeId(Guid statusTypeId);
        Task<SuggestionStatusResponse> AssingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId);
        Task<SuggestionStatusResponse> UnassingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId);

        // Methods for UserSuggestion Entity
        Task<IEnumerable<SuggestionStatus>> ListByUserSuggestionId(Guid userSuggestionId);
        Task<SuggestionStatusResponse> AssingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId);
        Task<SuggestionStatusResponse> UnassingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId);
    }
}
