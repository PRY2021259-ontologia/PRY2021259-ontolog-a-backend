using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User resource) : base(resource)
        {
        }

        public UserResponse(string message) : base(message)
        {
        }
    }
}
