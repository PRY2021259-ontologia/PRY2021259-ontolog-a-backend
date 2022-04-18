using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserLoginResponse : BaseResponse<UserLogin>
    {
        public UserLoginResponse(UserLogin resource) : base(resource)
        {
        }

        public UserLoginResponse(string message) : base(message)
        {
        }
    }
}
