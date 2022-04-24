using System.ComponentModel.DataAnnotations;

namespace Ontologia.API.Resources
{
    public class SaveUserTypeResource
    {
        [Required]
        public string UserTypeName { get; set; }
        [Required]
        public string UserTypeDescription { get; set; }
    }
}
