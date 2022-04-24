namespace Ontologia.API.Resources
{
    public class SuggestionStatusResource
    {
        public Guid Id { get; set; }
        public string SuggestionStatusTitle { get; set; }
        public string SuggestionStatusDescription { get; set; }
        public string Url { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
