using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveSuggestionTypeResource
    {
        [Required]
        public string SuggestionTypeName { get; set; }
        [Required]
        public string SuggestionTypeDescription { get; set; }
    }
}
