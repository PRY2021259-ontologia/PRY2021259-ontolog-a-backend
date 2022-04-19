using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class StatusTypeResponse : BaseResponse<StatusType>
    {
        public StatusTypeResponse(StatusType resource) : base(resource)
        {
        }

        public StatusTypeResponse(string message) : base(message)
        {
        }
    }
}
