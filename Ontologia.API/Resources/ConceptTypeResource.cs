namespace Ontologia.API.Resources
{
    public class ConceptTypeResource
    {
        public Guid Id { get; set; }
        public string ConceptTypeName { get; set; }
        public string ConceptTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
