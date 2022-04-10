using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;
using Ontologia.API.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace Ontologia.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class UserAuthsController : ControllerBase
    {
        private IUserAuthService _userAuthService;

        public UserAuthsController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Summary = "Verify credentials")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var response = _userAuthService.Authenticate(request);

            if (response.Message != null)
                return BadRequest(new { message = response.Message });

            return Ok(response);
        }
    }
}
