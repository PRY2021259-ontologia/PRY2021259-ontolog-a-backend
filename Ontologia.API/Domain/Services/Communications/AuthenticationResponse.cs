using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }

        // constructor
        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;
        }

        public AuthenticationResponse(string message)
        {
            Message = message;
        }

        public AuthenticationResponse() { }

    }
}
