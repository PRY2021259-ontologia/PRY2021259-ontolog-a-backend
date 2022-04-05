using AutoMapper;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services;
using Ontologia.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Ontologia.API.Extensions;

namespace Ontologia.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserSuggestionsController : ControllerBase
    {
        private readonly IUserSuggestionService _userSuggestionService;
        private readonly IMapper _mapper;

        public UserSuggestionsController(IUserSuggestionService userSuggestionService, IMapper mapper)
        {
            _userSuggestionService = userSuggestionService;
            _mapper = mapper;
        }

        // General HTTP Methods

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add new userSugegestion",
            Description = "Add new userSuggestion with initial data",
            OperationId = "AddUserSuggestion"
        )]
        [SwaggerResponse(200, "UserSuggestion Added", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserSuggestionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userSuggestion = _mapper.Map<SaveUserSuggestionResource, UserSuggestion>(resource);
            var result = await _userSuggestionService.SaveAsync(userSuggestion);

            if (!result.Success)
                return BadRequest(result.Message);

            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(result.Resource);
            return Ok(userSuggestionResource);
        }

        [HttpGet("{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Get userSuggestion",
            Description = "Get userSuggestion In the Data Base by id",
            OperationId = "GetUserSuggestion"
        )]
        [SwaggerResponse(200, "Returned userSuggestion", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<ActionResult> GetActionAsync(Guid userSuggestionId)
        {
            var result = await _userSuggestionService.GetById(userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(result.Resource);
            return Ok(userSuggestionResource);
        }

        [HttpDelete("{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Delete userSuggestion",
            Description = "Delete UserSuggestion In the Data Base by id",
            OperationId = "DeleteUserSuggestion"
        )]
        [SwaggerResponse(200, "Deleted UserSuggestion", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid userSuggestionId)
        {
            var result = await _userSuggestionService.Delete(userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(result.Resource);
            return Ok(userSuggestionResource);
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Get All userSuggestions",
           Description = "Get All userSuggestions In the Data Base by id",
           OperationId = "GetAllUserSuggestions"
        )]
        [SwaggerResponse(200, "Returned All UserSuggestions", typeof(IEnumerable<UserSuggestionResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserSuggestionResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<UserSuggestionResource>> GetAllAsync()
        {
            var userSuggestions = await _userSuggestionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserSuggestion>, IEnumerable<UserSuggestionResource>>(userSuggestions);
            return resources;
        }

        // HTTP Methods for User Entity

        [HttpPost("{userId}/userSuggestions/{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Assign userSuggestion to user",
            Description = "Assign userSuggestion to user by userSuggestionId and userId",
            OperationId = "AssignUserSuggestion"
        )]
        [SwaggerResponse(200, "userSuggestion to user Assigned", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserSuggestionToUser(Guid userId, Guid userSuggestionId)
        {
            var result = await _userSuggestionService.AssignUserSuggestion(userId, userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestion = await _userSuggestionService.GetById(result.Resource.Id);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(userSuggestion.Resource);
            return Ok(userSuggestionResource);
        }

        [HttpDelete("{userId}/userSuggestions/{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Unassign userSuggestion to user",
            Description = "Unassign userSuggestion to user by userSuggestionId and userId",
            OperationId = "UnassignUserSuggestion"
        )]
        [SwaggerResponse(200, "userSuggestion to user Unassigned", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserSuggestionToUser(Guid userId, Guid userSuggestionId)
        {
            var result = await _userSuggestionService.UnassignUserSuggestion(userId, userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestion = await _userSuggestionService.GetById(result.Resource.Id);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(userSuggestion.Resource);
            return Ok(userSuggestionResource);
        }

        // HTTP Methods for SuggestionType Entity

        [HttpPost("suggestionTypes/{suggestionTypeId}/userSuggestions/{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Assign userSuggestion to SuggestionType",
            Description = "Assign userSuggestion to SuggestionType by userSuggestionId and SuggestionTypeId",
            OperationId = "AssignUserSuggestion to SuggestionType"
        )]
        [SwaggerResponse(200, "userConcet to user Assigned", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            var result = await _userSuggestionService.AssignUserSuggestionToSuggestionType(suggestionTypeId, userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestion = await _userSuggestionService.GetById(result.Resource.Id);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(userSuggestion.Resource);
            return Ok(userSuggestionResource);
        }

        [HttpDelete("suggestionTypes/{suggestionTypeId}/userSuggestions/{userSuggestionId}")]
        [SwaggerOperation(
            Summary = "Unassign userSuggestion to SuggestionType",
            Description = "Unassign userSuggestion to SuggestionType by userSuggestionId and SuggestionTypeId",
            OperationId = "UnassignUserSuggestion to SuggestionType"
        )]
        [SwaggerResponse(200, "userConcet to user Unassigned", typeof(UserSuggestionResource))]
        [ProducesResponseType(typeof(UserSuggestionResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            var result = await _userSuggestionService.UnassignUserSuggestionToSuggestionType(suggestionTypeId, userSuggestionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userSuggestion = await _userSuggestionService.GetById(result.Resource.Id);
            var userSuggestionResource = _mapper.Map<UserSuggestion, UserSuggestionResource>(userSuggestion.Resource);
            return Ok(userSuggestionResource);
        }

    }
}
