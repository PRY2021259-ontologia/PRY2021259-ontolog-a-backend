using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserConceptResource
    {
        [Required]
        public string UserConceptTitle { get; set; }
        [Required]
        public string UserConceptDescription { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
