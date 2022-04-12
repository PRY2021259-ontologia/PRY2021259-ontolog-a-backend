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
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        private readonly IMapper _mapper;

        public UserTypesController(IUserTypeService UserTypeService, IMapper mapper)
        {
            _userTypeService = UserTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List all UserTypes",
            Description = "List of UserTypes",
            OperationId = "ListAllUserTypes"
            )]
        [SwaggerResponse(200, "List of UserTypes", typeof(IEnumerable<UserTypeResource>))]
        [ProducesResponseType(typeof(IEnumerable<UserTypeResource>), 200)]
        public async Task<IEnumerable<UserTypeResource>> GetAllAsync()
        {
            var userTypes = await _userTypeService.ListAsync();

            var resources = _mapper.Map<IEnumerable<UserType>, IEnumerable<UserTypeResource>>(userTypes);

            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get UserTypes",
            Description = "Get UserType By UserType Id",
            OperationId = "GetUserTypeById"
        )]
        [SwaggerResponse(200, "UserTypes Returned", typeof(UserTypeResource))]
        [ProducesResponseType(typeof(UserTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _userTypeService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);

            return Ok(userTypeResource);
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Add new UserType",
           Description = "Add new UserType with initial data",
           OperationId = "AddUserType"
        )]
        [SwaggerResponse(200, "UserType Added", typeof(UserTypeResource))]
        [ProducesResponseType(typeof(UserTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userType = _mapper.Map<SaveUserTypeResource, UserType>(resource);
            var result = await _userTypeService.SaveAsync(userType);

            if (!result.Success)
                return BadRequest(result.Message);

            var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);

            return Ok(userTypeResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
             Summary = "Update UserType",
             Description = "Update UserType By UserType Id",
             OperationId = "UpdateUserTypeById"
        )]
        [SwaggerResponse(200, "UserType Updated", typeof(UserTypeResource))]
        [ProducesResponseType(typeof(UserTypeResource), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveUserTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userType = _mapper.Map<SaveUserTypeResource, UserType>(resource);
            var result = await _userTypeService.UpdateAsync(id, userType);

            if (!result.Success)
                return BadRequest(result.Message);

            var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);

            return Ok(userTypeResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete UserType",
            Description = "Delete UserType By UserType Id",
            OperationId = "DeleteUserTypeById"
        )]
        [SwaggerResponse(200, "UserType Deleted", typeof(UserTypeResource))]
        [ProducesResponseType(typeof(UserTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userTypeResource = _mapper.Map<UserType, UserTypeResource>(result.Resource);

            return Ok(userTypeResource);
        }

    }
}

