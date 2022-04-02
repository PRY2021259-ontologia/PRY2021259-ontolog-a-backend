using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserHistoryResource
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string TextSearched { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
