using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserConceptPlantDiseaseService
    {
        Task<IEnumerable<UserConceptPlantDisease>> ListAsync();
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId);
        Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAndPlantDiseaseIdAsync(Guid userConceptId, Guid plantDiseaseId);
        Task<UserConceptPlantDiseaseResponse> AssignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId);
        Task<UserConceptPlantDiseaseResponse> UnassignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId);
    }
}
