using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveSuggestionStatusResource
    {
        [Required]
        public string SuggestionStatusTitle { get; set; }
        [Required]
        public string SuggestionStatusDescription { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public bool IsProcessed { get; set; }
    }
}
