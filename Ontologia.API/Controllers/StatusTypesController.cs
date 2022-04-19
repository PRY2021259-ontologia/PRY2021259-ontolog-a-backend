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
    public class StatusTypesController : ControllerBase
    {
        private readonly IStatusTypeService _statusTypeService;
        private readonly IMapper _mapper;

        public StatusTypesController(IStatusTypeService statusTypeService, IMapper mapper)
        {
            _statusTypeService = statusTypeService;
            _mapper = mapper;
        }

        // General HTTP Methods
        [HttpGet]
        [SwaggerOperation(
            Summary = "List all StatusTypes",
            Description = "List of StatusTypes",
            OperationId = "ListAllStatusTypes"
            )]
        [SwaggerResponse(200, "List of StatusTypes", typeof(IEnumerable<StatusTypeResource>))]
        [ProducesResponseType(typeof(IEnumerable<StatusTypeResource>), 200)]
        public async Task<IEnumerable<StatusTypeResource>> GetAllAsync()
        {
            var statusTypes = await _statusTypeService.ListAsync();

            var resources = _mapper.Map<IEnumerable<StatusType>, IEnumerable<StatusTypeResource>>(statusTypes);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get StatusTypes",
            Description = "Get StatusType By StatusType Id",
            OperationId = "GetStatusTypeById"
        )]
        [SwaggerResponse(200, "StatusTypes Returned", typeof(StatusTypeResource))]
        [ProducesResponseType(typeof(StatusTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _statusTypeService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var statusTypeResource = _mapper.Map<StatusType, StatusTypeResource>(result.Resource);

            return Ok(statusTypeResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new StatusType",
           Description = "Add new StatusType with initial data",
           OperationId = "AddStatusType"
        )]
        [SwaggerResponse(200, "StatusType Added", typeof(StatusTypeResource))]
        [ProducesResponseType(typeof(StatusTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveStatusTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var statusType = _mapper.Map<SaveStatusTypeResource, StatusType>(resource);
            var result = await _statusTypeService.SaveAsync(statusType);

            if (!result.Success)
                return BadRequest(result.Message);

            var statusTypeResource = _mapper.Map<StatusType, StatusTypeResource>(result.Resource);

            return Ok(statusTypeResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update StatusType",
             Description = "Update StatusType By StatusType Id",
             OperationId = "UpdateStatusTypeById"
        )]
        [SwaggerResponse(200, "StatusType Updated", typeof(StatusTypeResource))]
        [ProducesResponseType(typeof(StatusTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveStatusTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var statusType = _mapper.Map<SaveStatusTypeResource, StatusType>(resource);
            var result = await _statusTypeService.UpdateAsync(id, statusType);

            if (!result.Success)
                return BadRequest(result.Message);

            var statusTypeResource = _mapper.Map<StatusType, StatusTypeResource>(result.Resource);

            return Ok(statusTypeResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete StatusType",
            Description = "Delete StatusType By StatusType Id",
            OperationId = "DeleteStatusTypeById"
        )]
        [SwaggerResponse(200, "StatusType Deleted", typeof(StatusTypeResource))]
        [ProducesResponseType(typeof(StatusTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _statusTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var statusTypeResource = _mapper.Map<StatusType, StatusTypeResource>(result.Resource);

            return Ok(statusTypeResource);
        }
    }
}
