using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IPlantDiseaseService
    {
        // General Methods
        Task<PlantDiseaseResponse> SaveAsync(PlantDisease plantDisease);
        Task<IEnumerable<PlantDisease>> ListAsync();
        Task<PlantDiseaseResponse> GetById(Guid plantDiseaseId);
        Task<PlantDiseaseResponse?> GetByOntologyId(string ontologyId);
        Task<PlantDiseaseResponse> Update(Guid plantDiseaseId, PlantDisease plantDisease);
        Task<PlantDiseaseResponse> Delete(Guid plantDiseaseId);

        // Methods for CategoryDisease Entity
        Task<IEnumerable<PlantDisease>> ListByConceptTypeId(long categoryDiseaseId);
        Task<PlantDiseaseResponse> AssingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId);
        Task<PlantDiseaseResponse> UnassingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId);


        // Methods for UserConceptPlantDisease Entity
        Task<IEnumerable<PlantDisease>> ListByUserConceptIdAsync(Guid userConceptId);
    }
}
