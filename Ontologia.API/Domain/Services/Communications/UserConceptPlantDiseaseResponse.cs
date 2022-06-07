using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserConceptPlantDiseaseResponse : BaseResponse<UserConceptPlantDisease>
    {
        public UserConceptPlantDiseaseResponse(UserConceptPlantDisease resource) : base(resource)
        {
        }

        public UserConceptPlantDiseaseResponse(string message) : base(message)
        {
        }
    }
}
