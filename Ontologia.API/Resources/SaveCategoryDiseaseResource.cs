using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveCategoryDiseaseResource
    {
        [Required]
        public string CategoryDiseaseName { get; set; }
        [Required]
        public string CategoryDiseaseDescription { get; set; }
    }
}
