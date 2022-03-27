using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserConceptResource
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
