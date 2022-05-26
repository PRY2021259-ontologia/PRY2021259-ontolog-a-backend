using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IPlantDiseaseRepository
    {
        // General Methods
        Task AddAsync(PlantDisease plantDisease);
        Task<IEnumerable<PlantDisease>> ListAsync();
        Task<PlantDisease> GetById(Guid plantDiseasetId);
        Task<PlantDisease?> GetByOntologyId(string ontologyId);
        void Update(PlantDisease plantDisease);
        void Remove(PlantDisease plantDisease);

        // Methods for CategoryDisease Entity
        Task<IEnumerable<PlantDisease>> ListByCategoryDiseaseIdAsync(long categoryDiseaseId);
        Task AssingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId);
        Task UnassingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId);

    }
}
