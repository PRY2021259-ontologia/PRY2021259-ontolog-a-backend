namespace Ontologia.API.Domain.Models
{
    public class StatusType
    {
        public Guid Id { get; set; }
        public string StatusTypeTitle { get; set; }
        public string StatusTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with SuggestionStatus
        public IList<SuggestionStatus> SuggestionStatuses { get; set; } = new List<SuggestionStatus>();
    }
}
