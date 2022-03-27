using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserConceptService
    {
        Task<IEnumerable<UserConcept>> ListAsync();
        Task<UserConceptResponse> SaveAsync(UserConcept userConcept);
        Task<IEnumerable<UserConcept>> ListByUserId(int userId);
        Task<UserConceptResponse> GetById(int userConceptId);
        Task<UserConceptResponse> Update(int userConceptId, UserConcept userConcept);
        Task<UserConceptResponse> Delete(int userConceptId);
        Task<UserConceptResponse> AssignUserConcept(int userId, int userConceptId);
        Task<UserConceptResponse> UnassignUserConcept(int userId, int userConceptId);
    }
}
