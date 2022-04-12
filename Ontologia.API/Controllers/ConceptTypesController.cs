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
    public class ConceptTypesController : ControllerBase
    {
        private readonly IConceptTypeService _conceptTypeService;
        private readonly IMapper _mapper;

        public ConceptTypesController(IConceptTypeService conceptTypeService, IMapper mapper)
        {
            _conceptTypeService = conceptTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List all ConceptTypes",
            Description = "List of ConceptTypes",
            OperationId = "ListAllConceptTypes"
            )]
        [SwaggerResponse(200, "List of ConceptTypes", typeof(IEnumerable<ConceptTypeResource>))]
        [ProducesResponseType(typeof(IEnumerable<ConceptTypeResource>), 200)]
        public async Task<IEnumerable<ConceptTypeResource>> GetAllAsync()
        {
            var ConceptTypes = await _conceptTypeService.ListAsync();

            var resources = _mapper.Map<IEnumerable<ConceptType>, IEnumerable<ConceptTypeResource>>(ConceptTypes);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get ConceptTypes",
            Description = "Get ConceptType By ConceptType Id",
            OperationId = "GetConceptTypeById"
        )]
        [SwaggerResponse(200, "ConceptTypes Returned", typeof(ConceptTypeResource))]
        [ProducesResponseType(typeof(ConceptTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _conceptTypeService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var ConceptTypeResource = _mapper.Map<ConceptType, ConceptTypeResource>(result.Resource);

            return Ok(ConceptTypeResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new ConceptType",
           Description = "Add new ConceptType with initial data",
           OperationId = "AddConceptType"
        )]
        [SwaggerResponse(200, "ConceptType Added", typeof(ConceptTypeResource))]
        [ProducesResponseType(typeof(ConceptTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveConceptTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ConceptType = _mapper.Map<SaveConceptTypeResource, ConceptType>(resource);
            var result = await _conceptTypeService.SaveAsync(ConceptType);

            if (!result.Success)
                return BadRequest(result.Message);

            var ConceptTypeResource = _mapper.Map<ConceptType, ConceptTypeResource>(result.Resource);

            return Ok(ConceptTypeResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update ConceptType",
             Description = "Update ConceptType By ConceptType Id",
             OperationId = "UpdateConceptTypeById"
        )]
        [SwaggerResponse(200, "ConceptType Updated", typeof(ConceptTypeResource))]
        [ProducesResponseType(typeof(ConceptTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveConceptTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ConceptType = _mapper.Map<SaveConceptTypeResource, ConceptType>(resource);
            var result = await _conceptTypeService.UpdateAsync(id, ConceptType);

            if (!result.Success)
                return BadRequest(result.Message);

            var ConceptTypeResource = _mapper.Map<ConceptType, ConceptTypeResource>(result.Resource);

            return Ok(ConceptTypeResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete ConceptType",
            Description = "Delete ConceptType By ConceptType Id",
            OperationId = "DeleteConceptTypeById"
        )]
        [SwaggerResponse(200, "ConceptType Deleted", typeof(ConceptTypeResource))]
        [ProducesResponseType(typeof(ConceptTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _conceptTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var ConceptTypeResource = _mapper.Map<ConceptType, ConceptTypeResource>(result.Resource);

            return Ok(ConceptTypeResource);
        }

    }


}
