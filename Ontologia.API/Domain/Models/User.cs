namespace Ontologia.API.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with UserConcept | UseSuggestions Entities
        public IList<UserConcept> UserConcepts { get; set; } = new List<UserConcept>();
        public IList<UserSuggestion> UserSuggestions { get; set; } = new List<UserSuggestion>();
        public IList<UserHistory> UserHistories { get; set; } = new List<UserHistory>();
    }
}
