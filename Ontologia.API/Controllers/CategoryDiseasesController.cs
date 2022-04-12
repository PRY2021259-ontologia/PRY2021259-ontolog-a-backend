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
    public class CategoryDiseasesController : ControllerBase
    {
        private readonly ICategoryDiseaseService _categoryDiseaseService;
        private readonly IMapper _mapper;

        public CategoryDiseasesController(ICategoryDiseaseService categoryDiseaseService, IMapper mapper)
        {
            _categoryDiseaseService = categoryDiseaseService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List all CategoryDiseases",
            Description = "List of CategoryDiseases",
            OperationId = "ListAllCategoryDiseases"
            )]
        [SwaggerResponse(200, "List of CategoryDiseases", typeof(IEnumerable<CategoryDiseaseResource>))]
        [ProducesResponseType(typeof(IEnumerable<CategoryDiseaseResource>), 200)]
        public async Task<IEnumerable<CategoryDiseaseResource>> GetAllAsync()
        {
            var categoryDiseases = await _categoryDiseaseService.ListAsync();

            var resources = _mapper.Map<IEnumerable<CategoryDisease>, IEnumerable<CategoryDiseaseResource>>(categoryDiseases);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get CategoryDiseases",
            Description = "Get CategoryDisease By CategoryDisease Id",
            OperationId = "GetCategoryDiseaseById"
        )]
        [SwaggerResponse(200, "CategoryDiseases Returned", typeof(CategoryDiseaseResource))]
        [ProducesResponseType(typeof(CategoryDiseaseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _categoryDiseaseService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryDiseaseResource = _mapper.Map<CategoryDisease, CategoryDiseaseResource>(result.Resource);

            return Ok(categoryDiseaseResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new CategoryDisease",
           Description = "Add new CategoryDisease with initial data",
           OperationId = "AddCategoryDisease"
        )]
        [SwaggerResponse(200, "CategoryDisease Added", typeof(CategoryDiseaseResource))]
        [ProducesResponseType(typeof(CategoryDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryDiseaseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var categoryDisease = _mapper.Map<SaveCategoryDiseaseResource, CategoryDisease>(resource);
            var result = await _categoryDiseaseService.SaveAsync(categoryDisease);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryDiseaseResource = _mapper.Map<CategoryDisease, CategoryDiseaseResource>(result.Resource);

            return Ok(categoryDiseaseResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update CategoryDisease",
             Description = "Update CategoryDisease By CategoryDisease Id",
             OperationId = "UpdateCategoryDiseaseById"
        )]
        [SwaggerResponse(200, "CategoryDisease Updated", typeof(CategoryDiseaseResource))]
        [ProducesResponseType(typeof(CategoryDiseaseResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveCategoryDiseaseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var categoryDisease = _mapper.Map<SaveCategoryDiseaseResource, CategoryDisease>(resource);
            var result = await _categoryDiseaseService.UpdateAsync(id, categoryDisease);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryDiseaseResource = _mapper.Map<CategoryDisease, CategoryDiseaseResource>(result.Resource);

            return Ok(categoryDiseaseResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete CategoryDisease",
            Description = "Delete CategoryDisease By CategoryDisease Id",
            OperationId = "DeleteCategoryDiseaseById"
        )]
        [SwaggerResponse(200, "CategoryDisease Deleted", typeof(CategoryDiseaseResource))]
        [ProducesResponseType(typeof(CategoryDiseaseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _categoryDiseaseService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryDiseaseResource = _mapper.Map<CategoryDisease, CategoryDiseaseResource>(result.Resource);

            return Ok(categoryDiseaseResource);
        }

    }

}
