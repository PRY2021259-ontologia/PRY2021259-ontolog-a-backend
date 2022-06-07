namespace Ontologia.API.Domain.Models
{
    public class SuggestionStatus
    {
        public Guid Id { get; set; }
        public string SuggestionStatusTitle { get; set; }
        public string SuggestionStatusDescription { get; set; }
        public string Url { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }


        // Relationship with UserSuggestion Entity
        public Guid? UserSuggestionId { get; set; }
        public UserSuggestion? UserSuggestion { get; set; }

        // Relationship with StatusType Entity
        public Guid? StatusTypeId { get; set; }
        public StatusType? StatusType { get; set; }
    }
}
