using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveSuggestionTypeResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
