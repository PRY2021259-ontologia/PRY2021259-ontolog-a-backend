using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class SuggestionStatusResponse : BaseResponse<SuggestionStatus>
    {
        public SuggestionStatusResponse(SuggestionStatus resource) : base(resource)
        {
        }

        public SuggestionStatusResponse(string message) : base(message)
        {
        }
    }
}
