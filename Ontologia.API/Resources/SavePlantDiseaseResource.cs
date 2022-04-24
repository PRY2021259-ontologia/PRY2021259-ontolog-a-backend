using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SavePlantDiseaseResource
    {
        [Required]
        public string PlantDiseaseName { get; set; }
        [Required]
        public string PlantDiseaseDescription { get; set; }
    }
}
