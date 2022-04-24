using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveStatusTypeResource
    {
        [Required]
        public string StatusTypeTitle { get; set; }
        [Required]
        public string StatusTypeDescription { get; set; }
    }
}
