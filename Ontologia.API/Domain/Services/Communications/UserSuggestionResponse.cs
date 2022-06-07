using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserSuggestionResponse : BaseResponse<UserSuggestion>
    {
        public UserSuggestionResponse(UserSuggestion resource) : base(resource)
        {
        }

        public UserSuggestionResponse(string message) : base(message)
        {
        }
    }
}
