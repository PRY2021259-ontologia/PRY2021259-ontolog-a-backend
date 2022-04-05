namespace Ontologia.API.Domain.Models
{
    public class CategoryDisease
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with PlantDisease
        public IList<PlantDisease> PlantDiseases { get; set; } = new List<PlantDisease>();

    }
}
