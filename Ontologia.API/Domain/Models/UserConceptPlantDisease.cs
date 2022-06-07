namespace Ontologia.API.Domain.Models
{
    public class UserConceptPlantDisease
    {
        public Guid UserConceptId { get; set; }
        public UserConcept UserConcept { get; set; }
        public Guid PlantDiseaseId { get; set; }
        public PlantDisease PlantDisease { get; set; }
    }
}
