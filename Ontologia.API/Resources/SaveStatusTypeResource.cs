using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveStatusTypeResource
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
