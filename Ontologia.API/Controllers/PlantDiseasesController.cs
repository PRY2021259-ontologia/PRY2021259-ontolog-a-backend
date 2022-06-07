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
    public class PlantDiseasesController : ControllerBase
    {
        private readonly IPlantDiseaseService _plantDiseaseService;
        private readonly IMapper _mapper;

        public PlantDiseasesController(IPlantDiseaseService plantDiseaseService, IMapper mapper)
        {
            _plantDiseaseService = plantDiseaseService;
            _mapper = mapper;
        }

        // General HTTP Methods

        [HttpPost("PlantDiseases")]
        [SwaggerOperation(
            Summary = "Add new PlantDisease",
            Description = "Add new PlantDisease with initial data",
            OperationId = "AddPlantDisease"
        )]
        [SwaggerResponse(200, "PlantDisease Added", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SavePlantDiseaseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var plantDisease = _mapper.Map<SavePlantDiseaseResource, PlantDisease>(resource);
            var result = await _plantDiseaseService.SaveAsync(plantDisease);

            if (!result.Success)
                return BadRequest(result.Message);

            var plantDiseaseResource = _mapper.Map<PlantDisease, PlantDiseaseResource>(result.Resource);
            return Ok(plantDiseaseResource);
        }

        [HttpGet("PlantDiseases/{ontologyId}")]
        [SwaggerOperation(
            Summary = "Get PlantDisease",
            Description = "Get PlantDisease In the Data Base by Ontology Id",
            OperationId = "GetPlantDisease"
        )]
        [SwaggerResponse(200, "Returned PlantDisease", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetActionAsync(string ontologyId)
        {
            var result = await _plantDiseaseService.GetByOntologyId(ontologyId);
            if (!result.Success)
                return BadRequest(result.Message);
            var plantDiseaseResource = _mapper.Map<PlantDisease, PlantDiseaseResource>(result.Resource);
            return Ok(plantDiseaseResource);
        }

        [HttpDelete("PlantDiseases/{plantDiseaseId}")]
        [SwaggerOperation(
            Summary = "Delete PlantDisease",
            Description = "Delete PlantDisease In the Data Base by id",
            OperationId = "DeletePlantDisease"
        )]
        [SwaggerResponse(200, "Deleted PlantDisease", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid plantDiseaseId)
        {
            var result = await _plantDiseaseService.Delete(plantDiseaseId);
            if (!result.Success)
                return BadRequest(result.Message);
            var plantDiseaseResource = _mapper.Map<PlantDisease, PlantDiseaseResource>(result.Resource);
            return Ok(plantDiseaseResource);
        }

        [HttpGet("PlantDiseases")]
        [SwaggerOperation(
           Summary = "Get All PlantDiseases",
           Description = "Get All PlantDiseases In the Data Base",
           OperationId = "GetAllPlantDiseases"
        )]
        [SwaggerResponse(200, "Returned All PlantDiseases", typeof(IEnumerable<PlantDiseaseResource>))]
        [ProducesResponseType(typeof(IEnumerable<PlantDiseaseResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<PlantDiseaseResource>> GetAllAsync()
        {
            var plantDiseases = await _plantDiseaseService.ListAsync();
            var resources = _mapper.Map<IEnumerable<PlantDisease>, IEnumerable<PlantDiseaseResource>>(plantDiseases);
            return resources;
        }


        // HTTP Methods for CategoryDisease Entity

        [HttpGet("categoryDiseases/{categoryDiseaseId}/plantDiseases")]
        [SwaggerOperation(
           Summary = "Get All PlantDiseases by CategoryDisease Id",
           Description = "Get All PlantDiseases In the Data Base by CategoryDisease Id",
           OperationId = "GetAllPlantDiseasesByCategoryDiseaseId"
        )]
        [SwaggerResponse(200, "Returned All PlantDiseases", typeof(IEnumerable<PlantDiseaseResource>))]
        [ProducesResponseType(typeof(IEnumerable<PlantDiseaseResource>), 200)]
        [Produces("application/json")]
        public async Task<IEnumerable<PlantDiseaseResource>> GetAllByCategoryDiseaseId(long categoryDiseaseId)
        {
            var plantDiseases = await _plantDiseaseService.ListByConceptTypeId(categoryDiseaseId);
            var resources = _mapper.Map<IEnumerable<PlantDisease>, IEnumerable<PlantDiseaseResource>>(plantDiseases);
            return resources;
        }


        [HttpPost("categoryDiseases/{categoryDiseaseId}/plantDiseases/{plantDiseaseId}")]
        [SwaggerOperation(
            Summary = "Assign plantDisease to categoryDisease",
            Description = "Assign plantDisease to categoryDisease by PlantDiseaseId and categoryDiseaseId",
            OperationId = "AssignPlantDisease to categoryDisease"
        )]
        [SwaggerResponse(200, "plantDisease to categoryDisease Assigned", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignPlantDiseaseToConceptType(long categoryDiseaseId, Guid plantDiseaseId)
        {
            var result = await _plantDiseaseService.AssingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
            if (!result.Success)
                return BadRequest(result.Message);
            var plantDisease = await _plantDiseaseService.GetById(result.Resource.Id);
            var plantDiseaseResource = _mapper.Map<PlantDisease, PlantDiseaseResource>(plantDisease.Resource);
            return Ok(plantDiseaseResource);
        }

        [HttpDelete("categoryDiseases/{categoryDiseaseId}/plantDiseases/{plantDiseaseId}")]
        [SwaggerOperation(
            Summary = "Unassign plantDisease to categoryDisease",
            Description = "Unassign plantDisease to categoryDisease by PlantDiseaseId and categoryDiseaseId",
            OperationId = "UnassignPlantDisease to categoryDisease"
        )]
        [SwaggerResponse(200, "plantDisease to categoryDisease Unassigned", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignPlantDiseaseToConceptType(long categoryDiseaseId, Guid plantDiseaseId)
        {
            var result = await _plantDiseaseService.UnassingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
            if (!result.Success)
                return BadRequest(result.Message);
            var plantDisease = await _plantDiseaseService.GetById(result.Resource.Id);
            var plantDiseaseResource = _mapper.Map<PlantDisease, PlantDiseaseResource>(plantDisease.Resource);
            return Ok(plantDiseaseResource);
        }
    }
}
