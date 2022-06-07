namespace Ontologia.API.Resources
{
    public class SuggestionTypeResource
    {
        public Guid Id { get; set; }
        public string SuggestionTypeName { get; set; }
        public string SuggestionTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
