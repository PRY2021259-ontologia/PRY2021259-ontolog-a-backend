namespace Ontologia.API.Domain.Models
{
    public class UserSuggestion
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public string OptionalEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with User Entity
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        // Relationship with SuggestionType Entity
        public Guid? SuggestionTypeId { get; set; }
        public SuggestionType? SuggestionType{ get; set; }

        // Relationship with SuggestionStatus
        public IList<SuggestionStatus> SuggestionStatuses { get; set; } = new List<SuggestionStatus>();
    }
}
