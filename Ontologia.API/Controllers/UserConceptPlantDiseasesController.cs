using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Services;
using Ontologia.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ontologia.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/userConcepts/{userConceptId}/plantDiseases")]
    public class UserConceptPlantDiseasesController : ControllerBase
    {
        private readonly IUserConceptService _userConceptService;
        private readonly IPlantDiseaseService _plantDiseaseService;
        private readonly IUserConceptPlantDiseaseService _userConceptPlantDiseaseService;
        private readonly IMapper _mapper;

        public UserConceptPlantDiseasesController(IUserConceptService userConceptService, IPlantDiseaseService plantDiseaseService, IUserConceptPlantDiseaseService userConceptPlantDiseaseService, IMapper mapper)
        {
            _userConceptService = userConceptService;
            _plantDiseaseService = plantDiseaseService;
            _userConceptPlantDiseaseService = userConceptPlantDiseaseService;
            _mapper = mapper;
        }

        [HttpGet("/api/plantDiseases/{plantDiseaseId}/userConcepts")]
        [SwaggerOperation(
          Summary = "List UserConcepts by PlantDiseaseId",
          Description = "List UserConcepts by PlantDiseaseId",
          OperationId = "UserConceptsByPlantDiseaseId"
        )]
        [SwaggerResponse(200, "UserConceptss Returned", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IEnumerable<UserConceptResource>> GetAllByPlantDiseaseIdAsync(Guid plantDiseaseId)
        {
            var userConcepts = await _userConceptService.ListByPlantDiseaseIdAsync(plantDiseaseId);
            var resources = _mapper.Map<IEnumerable<UserConcept>, IEnumerable<UserConceptResource>>(userConcepts);

            return resources;
        }

        [HttpGet]
        [SwaggerOperation(
          Summary = "List PlantDiseases by UserConceptId",
          Description = "List PlantDiseases by UserConceptId",
          OperationId = "PlantDiseasesByUserConceptId"
        )]
        [SwaggerResponse(200, "PlantDiseases Returned", typeof(PlantDiseaseResource))]
        [ProducesResponseType(typeof(PlantDiseaseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IEnumerable<PlantDiseaseResource>> GetAllByUserConceptIdAsync(Guid userConceptId)
        {
            var plantDiseases= await _plantDiseaseService.ListByUserConceptIdAsync(userConceptId);
            var resources = _mapper.Map<IEnumerable<PlantDisease>, IEnumerable<PlantDiseaseResource>>(plantDiseases);

            return resources;
        }

        [HttpPost("{plantDiseaseId}")]
        [SwaggerOperation(
        Summary = "Assign userConcept to PlantDisease",
        Description = "Assign userConcept to PlantDisease",
        OperationId = "AssignUserConceptToPlantDisease"
        )]
        [SwaggerResponse(200, "UserConcept Assigned", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> AssignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId)
        {
            var result = await _userConceptPlantDiseaseService.AssignUserConceptPlantDiseaseAsync(userConceptId, plantDiseaseId);

            if (!result.Success)
                return BadRequest(result.Message);

            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(result.Resource.UserConcept);

            return Ok(userConceptResource);
        }

        [HttpDelete("{plantDiseaseId}")]
        [SwaggerOperation(
        Summary = "Unassign userConcept to PlantDisease",
        Description = "Unassign userConcept to PlantDisease",
        OperationId = "UnssignUserConceptToPlantDisease"
        )]
        [SwaggerResponse(200, "UserConcept Unssigned", typeof(UserConceptResource))]
        [ProducesResponseType(typeof(UserConceptResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> UnassignUserConceptPlantDisease(Guid userConceptId, Guid plantDiseaseId)
        {
            var result = await _userConceptPlantDiseaseService.UnassignUserConceptPlantDiseaseAsync(userConceptId, plantDiseaseId);

            if (!result.Success)
                return BadRequest(result.Message);

            var userConcept = await _userConceptService.GetById(result.Resource.UserConceptId);

            var userConceptResource = _mapper.Map<UserConcept, UserConceptResource>(userConcept.Resource);
            return Ok(userConceptResource);
        }
    }
}
