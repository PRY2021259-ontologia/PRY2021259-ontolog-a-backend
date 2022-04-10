using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;
using Ontologia.API.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net;

namespace Ontologia.API.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserAuthService(IOptions<AppSettings> appSettings, IUserRepository userRepository,
                               IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            // finbyEmail
            AuthenticationResponse response;
            var users = _userRepository.ListAsync();
            var user = users.Result.SingleOrDefault(x => x.Email == request.Email);

            if (user == null)
            {
                return new AuthenticationResponse("This email doesn't correspond to any user");
            }
            else
            {
                if (user.IsActive)
                {
                    var usersList = _userRepository.ListAsync();
                    var userFound = usersList.Result.SingleOrDefault(x => x.Email == request.Email);
                    if (!BCryptNet.BCrypt.Verify(request.Password, userFound.Password))
                    {
                        return new AuthenticationResponse("Invalid password for this user");
                    }
                    response = _mapper.Map<User, AuthenticationResponse>(userFound);
                }
                else
                {
                    if (!BCryptNet.BCrypt.Verify(request.Password, user.Password))
                    {
                        return new AuthenticationResponse("Invalid password for this user");
                    }

                    response = _mapper.Map<User, AuthenticationResponse>(user);
                }
                response.Token = GenerateJwtToken(response.Id.ToString());
                return response;

            }
        }

      
        private string GenerateJwtToken(string value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, value)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
