using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserConceptService
    {
        Task<IEnumerable<UserConcept>> ListAsync();
        Task<UserConceptResponse> SaveAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListByUserId(Guid userId);
        Task<UserConceptResponse> GetById(Guid userConceptId);
        Task<UserConceptResponse> Update(Guid userConceptId, UserConcept userConcept);
        Task<UserConceptResponse> Delete(Guid userConceptId);
        Task<UserConceptResponse> AssignUserConcept(Guid userId, Guid userConceptId);
        Task<UserConceptResponse> UnassignUserConcept(Guid userId, Guid userConceptId);
    }
}
