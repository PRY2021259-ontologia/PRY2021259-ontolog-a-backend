using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Domain.Services
{
    public interface IUserAuthService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
    }
}
