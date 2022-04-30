using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeService(IUserTypeRepository userTypeRepository, IUnitOfWork unitOfWork)
        {
            _userTypeRepository = userTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserTypeResponse> DeleteAsync(Guid id)
        {
            var existingUserType = await _userTypeRepository.FindById(id);

            if (existingUserType == null)
                return new UserTypeResponse("UserType Not Found");

            try
            {
                existingUserType.IsActive = false;
                _userTypeRepository.Update(existingUserType);
                await _unitOfWork.CompleteAsync();

                return new UserTypeResponse(existingUserType);
            }
            catch (Exception ex)
            {
                return new UserTypeResponse($"An error ocurred while deleting UserType: {ex.Message}");
            }
        }

        public async Task<UserTypeResponse> GetByIdAsync(Guid id)
        {
            var existingUserType = await _userTypeRepository.FindById(id);

            if (existingUserType == null || !existingUserType.IsActive)
                return new UserTypeResponse("UserType Not Found");

            return new UserTypeResponse(existingUserType);
        }

        public async Task<IEnumerable<UserType>> ListAsync()
        {
            var all = await _userTypeRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserTypeResponse> SaveAsync(UserType userType)
        {
            try
            {
                await _userTypeRepository.AddAsync(userType);
                await _unitOfWork.CompleteAsync();

                return new UserTypeResponse(userType);
            }
            catch (Exception ex)
            {
                return new UserTypeResponse($"An error ocurred while saving the UserType: {ex.Message}");
            }
        }

        public async Task<UserTypeResponse> UpdateAsync(Guid id, UserType userType)
        {
            var existingUserType = await _userTypeRepository.FindById(id);

            if (existingUserType == null)
                return new UserTypeResponse("UserType Not Found");

            existingUserType.UserTypeName = userType.UserTypeName;
            existingUserType.UserTypeDescription = userType.UserTypeDescription;
            existingUserType.IsActive = userType.IsActive;
            existingUserType.CreatedOn = userType.CreatedOn;
            existingUserType.ModifiedOn = userType.ModifiedOn;

            try
            {
                _userTypeRepository.Update(existingUserType);
                await _unitOfWork.CompleteAsync();

                return new UserTypeResponse(existingUserType);
            }
            catch (Exception ex)
            {
                return new UserTypeResponse($"An error ocurred while updating UserType: {ex.Message}");
            }
        }
    }
}
