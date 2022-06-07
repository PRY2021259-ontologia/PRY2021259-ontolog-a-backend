using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveConceptTypeResource
    {
        [Required]
        public string ConceptTypeName { get; set; }
        [Required]
        public string ConceptTypeDescription { get; set; }
    }
}
