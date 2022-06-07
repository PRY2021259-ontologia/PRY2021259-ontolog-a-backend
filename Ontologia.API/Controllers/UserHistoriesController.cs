using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class UserHistoriesController : ControllerBase
    {
        private readonly IUserHistoryService _userHistoryService;
        private readonly IMapper _mapper;

        public UserHistoriesController(IUserHistoryService userHistoryService, IMapper mapper)
        {
            _userHistoryService = userHistoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add new userHistory",
            Description = "Add new userHistory with initial data",
            OperationId = "AddUserHistory"
        )]
        [SwaggerResponse(200, "UserHistory Added", typeof(UserHistoryResource))]
        [ProducesResponseType(typeof(UserHistoryResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userHistory = _mapper.Map<SaveUserHistoryResource, UserHistory>(resource);
            var result = await _userHistoryService.SaveAsync(userHistory);

            if (!result.Success)
                return BadRequest(result.Message);

            var userHistoryResource = _mapper.Map<UserHistory, UserHistoryResource>(result.Resource);
            return Ok(userHistoryResource);
        }

        [HttpGet("{userHistoryId}")]
        [SwaggerOperation(
            Summary = "Get userHistory",
            Description = "Get userHistory In the Data Base by id",
            OperationId = "GetUserHistory"
        )]
        [SwaggerResponse(200, "Returned userHistory", typeof(UserHistoryResource))]
        [ProducesResponseType(typeof(UserHistoryResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetActionAsync(Guid userHistoryId)
        {
            var result = await _userHistoryService.GetById(userHistoryId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userHistoryResource = _mapper.Map<UserHistory, UserHistoryResource>(result.Resource);
            return Ok(userHistoryResource);
        }

        [HttpDelete("{userHistoryId}")]
        [SwaggerOperation(
            Summary = "Delete userHistory",
            Description = "Delete UserHistory In the Data Base by id",
            OperationId = "DeleteUserHistory"
        )]
        [SwaggerResponse(200, "Deleted UserHistory", typeof(UserHistoryResource))]
        [ProducesResponseType(typeof(UserHistoryResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid userHistoryId)
        {
            var result = await _userHistoryService.Delete(userHistoryId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userHistoryResource = _mapper.Map<UserHistory, UserHistoryResource>(result.Resource);
            return Ok(userHistoryResource);
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Get All userHistorys",
           Description = "Get All userHistorys In the Data Base by id",
           OperationId = "GetAllUserHistorys"
        )]
        [SwaggerResponse(200, "Returned All UserHistories", typeof(IEnumerable<UserHistoryResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserHistoryResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<UserHistoryResource>> GetAllAsync()
        {
            var userHistorys = await _userHistoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserHistory>, IEnumerable<UserHistoryResource>>(userHistorys);
            return resources;
        }

        // Methods for User Entity

        [HttpGet("users/{userId}/userHistories")]
        [SwaggerOperation(
          Summary = "Get All UserHistories by UserId",
          Description = "Get All UserHistories In the DataBase by UserId",
          OperationId = "GetAllUserHistoriesByUserId"
        )]
        [SwaggerResponse(200, "Returned All UserHistories", typeof(IEnumerable<UserHistory>))]
        [ProducesResponseType(typeof(IEnumerable<UserHistoryResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<UserHistoryResource>> GetAllByUserId(Guid userId)
        {
            var userHistories = await _userHistoryService.ListByUserId(userId);
            var resources = _mapper.Map<IEnumerable<UserHistory>, IEnumerable<UserHistoryResource>>(userHistories);
            return resources;
        }

        [HttpPost("{userHistoryId}/userHistories/{userId}")]
        [SwaggerOperation(
            Summary = "Assign userHistory to user",
            Description = "Assign userHistory to user by userHistoryId and userId",
            OperationId = "AssignUserHistory"
        )]
        [SwaggerResponse(200, "userHistory to user Assigned", typeof(UserHistoryResource))]
        [ProducesResponseType(typeof(UserHistoryResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserHistoryToUser(Guid userId, Guid userHistoryId)
        {
            var result = await _userHistoryService.AssignUserHistory(userId, userHistoryId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userHistory = await _userHistoryService.GetById(result.Resource.Id);
            var userHistoryResource = _mapper.Map<UserHistory, UserHistoryResource>(userHistory.Resource);
            return Ok(userHistoryResource);
        }

        [HttpDelete("{userHistoryId}/userHistories/{userId}")]
        [SwaggerOperation(
            Summary = "Unassign userHistory to user",
            Description = "Unassign userHistory to user by userHistoryId and userId",
            OperationId = "UnassignUserHistory"
        )]
        [SwaggerResponse(200, "userHistory to user Unassigned", typeof(UserHistoryResource))]
        [ProducesResponseType(typeof(UserHistoryResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserHistoryToUser(Guid userId, Guid userHistoryId)
        {
            var result = await _userHistoryService.UnassignUserHistory(userId, userHistoryId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userHistory = await _userHistoryService.GetById(result.Resource.Id);
            var userHistoryResource = _mapper.Map<UserHistory, UserHistoryResource>(userHistory.Resource);
            return Ok(userHistoryResource);
        }

    }
}
