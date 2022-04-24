using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserSuggestionResource
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public string OptionalEmail { get; set; }
    }
}
