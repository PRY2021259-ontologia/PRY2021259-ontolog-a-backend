using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class ConceptTypeResponse : BaseResponse<ConceptType>
    {
        public ConceptTypeResponse(ConceptType resource) : base(resource)
        {
        }

        public ConceptTypeResponse(string message) : base(message)
        {
        }
    }
}
