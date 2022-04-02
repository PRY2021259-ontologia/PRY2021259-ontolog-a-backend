using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserHistoryResponse : BaseResponse<UserHistory>
    {
        public UserHistoryResponse(UserHistory resource) : base(resource)
        {
        }

        public UserHistoryResponse(string message) : base(message)
        {
        }
    }
}
