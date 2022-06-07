namespace Ontologia.API.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with UserType Entity
        public Guid? UserTypeId { get; set; }
        public UserType? UserType { get; set; }

        // Relationship with UserConcept | UserSuggestion | UserHistory
        public IList<UserConcept> UserConcepts { get; set; } = new List<UserConcept>();
        public IList<UserSuggestion> UserSuggestions { get; set; } = new List<UserSuggestion>();
        public IList<UserHistory> UserHistories { get; set; } = new List<UserHistory>();

        // Relationship with UserLogin Entity
        public Guid? UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }
    }
}
