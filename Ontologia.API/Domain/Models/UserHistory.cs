namespace Ontologia.API.Domain.Models
{
    public class UserHistory
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string TextSearched { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Relationship with User Entity
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
