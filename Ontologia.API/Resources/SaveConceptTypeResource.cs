using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveConceptTypeResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
