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
        Task<PlantDiseaseResponse> Update(Guid plantDiseaseId, PlantDisease plantDisease);
        Task<PlantDiseaseResponse> Delete(Guid plantDiseaseId);

        // Methods for CategoryDisease Entity
        Task<IEnumerable<PlantDisease>> ListByConceptTypeId(Guid categoryDiseaseId);
        Task<PlantDiseaseResponse> AssingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);
        Task<PlantDiseaseResponse> UnassingPlantDiseaseToCategoryDisease(Guid categoryDiseaseId, Guid plantDiseaseId);
    }
}
