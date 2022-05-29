namespace Ontologia.API.Resources
{
    public class PlantDiseaseResource
    {
        public Guid Id { get; set; }
        public string OntologyId { get; set; }
        public string PlantDiseaseName { get; set; }
        public string PlantDiseaseDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
