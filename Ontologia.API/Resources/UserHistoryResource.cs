namespace Ontologia.API.Resources
{
    public class UserHistoryResource
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string TextSearched { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
