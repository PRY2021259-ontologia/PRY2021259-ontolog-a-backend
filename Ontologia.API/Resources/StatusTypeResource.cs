namespace Ontologia.API.Resources
{
    public class StatusTypeResource
    {
        public Guid Id { get; set; }
        public string StatusTypeTitle { get; set; }
        public string StatusTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
