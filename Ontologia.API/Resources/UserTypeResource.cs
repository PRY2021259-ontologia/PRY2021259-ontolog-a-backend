namespace Ontologia.API.Resources
{
    public class UserTypeResource
    {
        public Guid Id { get; set; }
        public string UserTypeName { get; set; }
        public string UserTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
