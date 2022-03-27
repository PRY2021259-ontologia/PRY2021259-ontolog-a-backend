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
    public class UserConceptsController : ControllerBase
    {
        private readonly IUserConceptService _userConceptService;
        private readonly IMapper _mapper;

        public UserConceptsController(IUserConceptService userConceptService, IMapper mapper)
        {
            _userConceptService = userConceptService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add new userConcept",
            Description = "Add new userConcept with initial data",
            OperationId = "AddUserConcept"
        )]
        [SwaggerResponse(200, "UserConcept Added", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<ActionResult> PostAsync([FromBody] SaveUserConceptResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userConcept = _mapper.Map<SaveUserConceptResource, UserConcept>(resource);
            var result = await _userConceptService.SaveAsync(userConcept);

            if (!result.Success)
                return BadRequest(result.Message);

            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(result.Resource);
            return Ok(userConceptResource);
        }

        [HttpGet("{userConceptId}")]
        [SwaggerOperation(
            Summary = "Get userConcept",
            Description = "Get userConcept In the Data Base by id",
            OperationId = "GetUserConcept"
        )]
        [SwaggerResponse(200, "Returned userConcept", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<ActionResult> GetActionAsync(int userConceptId)
        {
            var result = await _userConceptService.GetById(userConceptId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(result.Resource);
            return Ok(userConceptResource);
        }

        [HttpDelete("{userConceptId}")]
        [SwaggerOperation(
            Summary = "Delete userConcept",
            Description = "Delete UserConcept In the Data Base by id",
            OperationId = "DeleteUserConcept"
        )]
        [SwaggerResponse(200, "Deleted UserConcept", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(int userConceptId)
        {
            var result = await _userConceptService.Delete(userConceptId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(result.Resource);
            return Ok(userConceptResource);
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Get All userConcepts",
           Description = "Get All userConcepts In the Data Base by id",
           OperationId = "GetAllUserConcepts"
        )]
        [SwaggerResponse(200, "Returned All UserConcepts", typeof(IEnumerable<UserConceptResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserConceptResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<UserConceptResource>> GetAllAsync()
        {
            var userConcepts = await _userConceptService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserConcept>, IEnumerable<UserConceptResource>>(userConcepts);
            return resources;
        }

        [HttpPost("{userId}/userConcepts/{userConceptId}")]
        [SwaggerOperation(
            Summary = "Assign userConcet to user",
            Description = "Assign userConcet to user by userConceptId and userId",
            OperationId = "AssignUserConcept"
        )]
        [SwaggerResponse(200, "userConcet to user Assigned", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserConceptToUser(int userId, int userConceptId)
        {
            var result = await _userConceptService.AssignUserConcept(userId, userConceptId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userConcept = await _userConceptService.GetById(result.Resource.Id);
            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(userConcept.Resource);
            return Ok(userConceptResource);
        }

        [HttpDelete("{userId}/userConcepts/{userConceptId}")]
        [SwaggerOperation(
            Summary = "Unassign userConcet to user",
            Description = "Unassign userConcet to user by userConceptId and userId",
            OperationId = "UnassignUserConcept"
        )]
        [SwaggerResponse(200, "userConcet to user Unassigned", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserConceptToUser(int userId, int userConceptId)
        {
            var result = await _userConceptService.UnassignUserConcept(userId, userConceptId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userConcept = await _userConceptService.GetById(result.Resource.Id);
            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(userConcept.Resource);
            return Ok(userConceptResource);
        }

    }
}
