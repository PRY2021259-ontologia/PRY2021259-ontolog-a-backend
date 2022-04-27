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
    [ApiController]
    [Produces("application/json")]
    [Route("/api/")]
    public class SuggestionStatusesController : ControllerBase
    {
        private readonly ISuggestionStatusService _suggestionStatusService;
        private readonly IMapper _mapper;

        public SuggestionStatusesController(ISuggestionStatusService suggestionStatusService, IMapper mapper)
        {
            _suggestionStatusService = suggestionStatusService;
            _mapper = mapper;
        }

        // General HTTP Methods

        [HttpPost("SuggestionStatuses")]
        [SwaggerOperation(
            Summary = "Add new SuggestionStatus",
            Description = "Add new SuggestionStatus with initial data",
            OperationId = "AddSuggestionStatus"
        )]
        [SwaggerResponse(200, "SuggestionStatus Added", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveSuggestionStatusResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var suggestionStatus = _mapper.Map<SaveSuggestionStatusResource, SuggestionStatus>(resource);
            var result = await _suggestionStatusService.SaveAsync(suggestionStatus);

            if (!result.Success)
                return BadRequest(result.Message);

            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(result.Resource);
            return Ok(suggestionStatusResource);
        }

        [HttpGet("SuggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Get SuggestionStatus",
            Description = "Get SuggestionStatus In the Data Base by id",
            OperationId = "GetSuggestionStatus"
        )]
        [SwaggerResponse(200, "Returned SuggestionStatus", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetActionAsync(Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.GetById(suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(result.Resource);
            return Ok(suggestionStatusResource);
        }

        [HttpDelete("SuggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Delete SuggestionStatus",
            Description = "Delete SuggestionStatus In the Data Base by id",
            OperationId = "DeleteSuggestionStatus"
        )]
        [SwaggerResponse(200, "Deleted SuggestionStatus", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.Delete(suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(result.Resource);
            return Ok(suggestionStatusResource);
        }

        [HttpGet("SuggestionStatuses")]
        [SwaggerOperation(
           Summary = "Get All SuggestionStatuses",
           Description = "Get All SuggestionStatuses In the Data Base",
           OperationId = "GetAllSuggestionStatuses"
        )]
        [SwaggerResponse(200, "Returned All SuggestionStatuss", typeof(IEnumerable<SuggestionStatusResource>))]
        [ProducesResponseType(typeof(IEnumerable<SuggestionStatusResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<SuggestionStatusResource>> GetAllAsync()
        {
            var suggestionStatuses = await _suggestionStatusService.ListAsync();
            var resources = _mapper.Map<IEnumerable<SuggestionStatus>, IEnumerable<SuggestionStatusResource>>(suggestionStatuses);
            return resources;
        }

        // HTTP Methods for StatusType Entity

        [HttpGet("statusTypes/{statusTypeId}/suggestionStatuses")]
        [SwaggerOperation(
           Summary = "Get All SuggestionStatuses by StatusTypeId",
           Description = "Get All SuggestionStatuses In the DataBase by StatusTypeId",
           OperationId = "GetAllSuggestionStatusesByStatusTypeId"
        )]
        [SwaggerResponse(200, "Returned All SuggestionStatuses", typeof(IEnumerable<SuggestionStatus>))]
        [ProducesResponseType(typeof(IEnumerable<SuggestionStatusResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<SuggestionStatusResource>> GetAllByStatusTypeId(Guid statusTypeId)
        {
            var suggestionStatuses = await _suggestionStatusService.ListByStatusTypeId(statusTypeId);
            var resources = _mapper.Map<IEnumerable<SuggestionStatus>, IEnumerable<SuggestionStatusResource>>(suggestionStatuses);
            return resources;
        }

        [HttpPost("statusTypes/{statusTypeId}/suggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Assign SuggestionStatus to StatusType",
            Description = "Assign SuggestionStatus to StatusType by SuggestionStatusId and StatusTypeId",
            OperationId = "AssignSuggestionStatus to StatusType"
        )]
        [SwaggerResponse(200, "SuggestionStatus to StatusType Assigned", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.AssingSuggestionStatusToStatusType(statusTypeId, suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatus = await _suggestionStatusService.GetById(result.Resource.Id);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(suggestionStatus.Resource);
            return Ok(suggestionStatusResource);
        }

        [HttpDelete("statusTypes/{statusTypeId}/suggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Unassign SuggestionStatus to StatusType",
            Description = "Unassign SuggestionStatus to StatusType by SuggestionStatusId and StatusTypeId",
            OperationId = "UnassignSuggestionStatus to StatusType"
        )]
        [SwaggerResponse(200, "SuggestionStatus to StatusType Unassigned", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.UnassingSuggestionStatusToStatusType(statusTypeId, suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatus = await _suggestionStatusService.GetById(result.Resource.Id);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(suggestionStatus.Resource);
            return Ok(suggestionStatusResource);
        }

        // HTTP Methods for UserSuggestion Entity

        [HttpGet("userSuggestions/{userSuggestionId}/suggestionStatuses")]
        [SwaggerOperation(
           Summary = "Get All SuggestionStatuses by UserSuggestionId",
           Description = "Get All SuggestionStatuses In the DataBase by UserSuggestionId",
           OperationId = "GetAllSuggestionStatusesByUserSuggestionId"
        )]
        [SwaggerResponse(200, "Returned All SuggestionStatuses", typeof(IEnumerable<SuggestionStatus>))]
        [ProducesResponseType(typeof(IEnumerable<SuggestionStatusResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<SuggestionStatusResource>> GetAllByUserSuggestionId(Guid userSuggestionId)
        {
            var suggestionStatuses = await _suggestionStatusService.ListByUserSuggestionId(userSuggestionId);
            var resources = _mapper.Map<IEnumerable<SuggestionStatus>, IEnumerable<SuggestionStatusResource>>(suggestionStatuses);
            return resources;
        }

        [HttpPost("userSuggestions/{userSuggestionId}/suggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Assign SuggestionStatus to UserSuggestion",
            Description = "Assign SuggestionStatus to UserSuggestion by SuggestionStatusId and UserSuggestionId",
            OperationId = "AssignSuggestionStatus to UserSuggestion"
        )]
        [SwaggerResponse(200, "SuggestionStatus to UserSuggestion Assigned", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.AssingSuggestionStatusToUserSuggestion(userSuggestionId, suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatus = await _suggestionStatusService.GetById(result.Resource.Id);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(suggestionStatus.Resource);
            return Ok(suggestionStatusResource);
        }

        [HttpDelete("userSuggestions/{userSuggestionId}/suggestionStatuses/{suggestionStatusId}")]
        [SwaggerOperation(
            Summary = "Unassign SuggestionStatus to UserSuggestion",
            Description = "Unassign SuggestionStatus to UserSuggestion by SuggestionStatusId and UserSuggestionId",
            OperationId = "UnassignSuggestionStatus to UserSuggestion"
        )]
        [SwaggerResponse(200, "SuggestionStatus to UserSuggestion Unassigned", typeof(SuggestionStatusResource))]
        [ProducesResponseType(typeof(SuggestionStatusResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            var result = await _suggestionStatusService.UnassingSuggestionStatusToUserSuggestion(userSuggestionId, suggestionStatusId);
            if (!result.Success)
                return BadRequest(result.Message);
            var suggestionStatus = await _suggestionStatusService.GetById(result.Resource.Id);
            var suggestionStatusResource = _mapper.Map<SuggestionStatus, SuggestionStatusResource>(suggestionStatus.Resource);
            return Ok(suggestionStatusResource);
        }
    }
}
