using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserTypeResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
