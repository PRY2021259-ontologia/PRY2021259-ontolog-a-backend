using Ontologia.API.Domain.Models;


namespace Ontologia.API.Domain.Services.Communications
{
    public class PlantDiseaseResponse : BaseResponse<PlantDisease>
    {
        public PlantDiseaseResponse(PlantDisease resource) : base(resource)
        {
        }

        public PlantDiseaseResponse(string message) : base(message)
        {
        }
    }
}
