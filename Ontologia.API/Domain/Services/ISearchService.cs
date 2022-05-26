using Ontologia.API.Resources;

namespace Ontologia.API.Domain.Services
{
    public interface ISearchService
    {
        Task<PlantDiseaseByOntologyResource?> GetPlantDiseaseByOntologyId(string id);
    }
}

