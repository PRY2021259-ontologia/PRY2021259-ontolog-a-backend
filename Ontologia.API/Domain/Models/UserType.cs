namespace Ontologia.API.Domain.Models
{
    public class UserType
    {
        public Guid Id { get; set; }
        public string UserTypeName { get; set; }
        public string UserTypeDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with User
        public IList<User> Users { get; set; } = new List<User>();
    }
}
