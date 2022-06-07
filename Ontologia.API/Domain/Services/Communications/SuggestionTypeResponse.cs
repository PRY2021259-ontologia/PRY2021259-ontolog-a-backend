using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class SuggestionTypeResponse : BaseResponse<SuggestionType>
    {
        public SuggestionTypeResponse(SuggestionType resource) : base(resource)
        {
        }

        public SuggestionTypeResponse(string message) : base(message)
        {
        }
    }
}
