namespace Ontologia.API.Resources
{
    public class UserConceptResource
    {
        public Guid Id { get; set; }
        public string UserConceptTitle { get; set; }
        public string UserConceptDescription { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
