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
    public class SuggestionTypesController : ControllerBase
    {
        private readonly ISuggestionTypeService _suggestionTypeService;
        private readonly IMapper _mapper;

        public SuggestionTypesController(ISuggestionTypeService suggestionTypeService, IMapper mapper)
        {
            _suggestionTypeService = suggestionTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List all SuggestionTypes",
            Description = "List of SuggestionTypes",
            OperationId = "ListAllSuggestionTypes"
            )]
        [SwaggerResponse(200, "List of SuggestionTypes", typeof(IEnumerable<SuggestionTypeResource>))]
        [ProducesResponseType(typeof(IEnumerable<SuggestionTypeResource>), 200)]
        public async Task<IEnumerable<SuggestionTypeResource>> GetAllAsync()
        {
            var suggestionTypes = await _suggestionTypeService.ListAsync();

            var resources = _mapper.Map<IEnumerable<SuggestionType>, IEnumerable<SuggestionTypeResource>>(suggestionTypes);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get SuggestionTypes",
            Description = "Get SuggestionType By SuggestionType Id",
            OperationId = "GetSuggestionTypeById"
        )]
        [SwaggerResponse(200, "SuggestionTypes Returned", typeof(SuggestionTypeResource))]
        [ProducesResponseType(typeof(SuggestionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _suggestionTypeService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var suggestionTypeResource = _mapper.Map<SuggestionType, SuggestionTypeResource>(result.Resource);

            return Ok(suggestionTypeResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new SuggestionType",
           Description = "Add new SuggestionType with initial data",
           OperationId = "AddSuggestionType"
        )]
        [SwaggerResponse(200, "SuggestionType Added", typeof(SuggestionTypeResource))]
        [ProducesResponseType(typeof(SuggestionTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveSuggestionTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var suggestionType = _mapper.Map<SaveSuggestionTypeResource, SuggestionType>(resource);
            var result = await _suggestionTypeService.SaveAsync(suggestionType);

            if (!result.Success)
                return BadRequest(result.Message);

            var suggestionTypeResource = _mapper.Map<SuggestionType, SuggestionTypeResource>(result.Resource);

            return Ok(suggestionTypeResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update SuggestionType",
             Description = "Update SuggestionType By SuggestionType Id",
             OperationId = "UpdateSuggestionTypeById"
        )]
        [SwaggerResponse(200, "SuggestionType Updated", typeof(SuggestionTypeResource))]
        [ProducesResponseType(typeof(SuggestionTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveSuggestionTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var suggestionType = _mapper.Map<SaveSuggestionTypeResource, SuggestionType>(resource);
            var result = await _suggestionTypeService.UpdateAsync(id, suggestionType);

            if (!result.Success)
                return BadRequest(result.Message);

            var suggestionTypeResource = _mapper.Map<SuggestionType, SuggestionTypeResource>(result.Resource);

            return Ok(suggestionTypeResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete SuggestionType",
            Description = "Delete SuggestionType By SuggestionType Id",
            OperationId = "DeleteSuggestionTypeById"
        )]
        [SwaggerResponse(200, "SuggestionType Deleted", typeof(SuggestionTypeResource))]
        [ProducesResponseType(typeof(SuggestionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _suggestionTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var suggestionTypeResource = _mapper.Map<SuggestionType, SuggestionTypeResource>(result.Resource);

            return Ok(suggestionTypeResource);
        }

    }
}
