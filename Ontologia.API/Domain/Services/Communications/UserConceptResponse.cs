using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserConceptResponse : BaseResponse<UserConcept>
    {
        public UserConceptResponse(UserConcept resource) : base(resource)
        {
        }

        public UserConceptResponse(string message) : base(message)
        {
        }
    }
}
