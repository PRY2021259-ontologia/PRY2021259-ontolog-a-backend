using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveCategoryDiseaseResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
