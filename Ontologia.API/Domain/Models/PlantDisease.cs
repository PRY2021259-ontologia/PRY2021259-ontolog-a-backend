namespace Ontologia.API.Domain.Models
{
    public class PlantDisease
    {
        public Guid Id { get; set; }
        public string OntologyId { get; set; }
        public string PlantDiseaseName { get; set; }
        public string PlantDiseaseDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with CategoryDisease Entity
        public long? CategoryDiseaseId { get; set; }
        public CategoryDisease? CategoryDisease { get; set; }

        // Relationship with UserConceptPlantDisease Entity
        public List<UserConceptPlantDisease> UserConceptPlantDiseases { get; set; }
    }
}
