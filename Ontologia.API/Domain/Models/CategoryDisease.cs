namespace Ontologia.API.Domain.Models
{
    public class CategoryDisease
    {
        public long Id { get; set; }
        public string CategoryDiseaseName { get; set; }
        public string CategoryDiseaseDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with PlantDisease
        public IList<PlantDisease> PlantDiseases { get; set; } = new List<PlantDisease>();

    }
}
