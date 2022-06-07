namespace Ontologia.API.Domain.Models
{
    public class UserConcept
    {
        public Guid Id { get; set; }
        public string UserConceptTitle { get; set; }
        public string UserConceptDescription { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with User Entity
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        // Relationship with ConceptType Entity
        public Guid? ConceptTypeId { get; set; }
        public ConceptType? ConceptType { get; set; }

        // Relationship with UserConceptPlantDisease Entity
        public List<UserConceptPlantDisease> UserConceptPlantDiseases { get; set; }
    }
}
