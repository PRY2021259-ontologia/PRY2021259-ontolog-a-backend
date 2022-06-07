using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserTypeResponse : BaseResponse<UserType>
    {
        public UserTypeResponse(UserType resource) : base(resource)
        {
        }

        public UserTypeResponse(string message) : base(message)
        {
        }
    }
}
