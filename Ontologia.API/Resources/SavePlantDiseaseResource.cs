using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SavePlantDiseaseResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
