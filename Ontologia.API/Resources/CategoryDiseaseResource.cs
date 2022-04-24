namespace Ontologia.API.Resources
{
    public class CategoryDiseaseResource
    {
        public Guid Id { get; set; }
        public string CategoryDiseaseName { get; set; }
        public string CategoryDiseaseDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
