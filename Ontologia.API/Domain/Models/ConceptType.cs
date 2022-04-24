namespace Ontologia.API.Domain.Models
{
    public class ConceptType
    {
        public Guid Id { get; set; }
        public string ConceptTypeName { get; set; }
        public string ConceptTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with UserConcept
        public IList<UserConcept> UserConcepts { get; set; } = new List<UserConcept>();
    }
}
