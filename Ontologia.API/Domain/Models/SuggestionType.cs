namespace Ontologia.API.Domain.Models
{
    public class SuggestionType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with UserSuggestion
        public IList<UserSuggestion> UserSuggestions { get; set; } = new List<UserSuggestion>();
    }
}
