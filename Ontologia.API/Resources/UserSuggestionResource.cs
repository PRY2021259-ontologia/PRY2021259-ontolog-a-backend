namespace Ontologia.API.Resources
{
    public class UserSuggestionResource
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public string OptionalEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
