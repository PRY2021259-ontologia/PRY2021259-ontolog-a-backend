using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

        public async Task<IEnumerable<User>> ListAsync()
        {
            var all = await _userRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserResponse> DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User Not Found");

            try
            {
                existingUser.IsActive = false;
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting user: {ex.Message}");
            }

        }

        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null || !existingUser.IsActive)
                return new UserResponse("User Not Found");

            return new UserResponse(existingUser);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(Guid id, User user)
        {
            var existingUser= await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User Not Found");

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Occupation = user.Occupation;
            existingUser.IsActive = user.IsActive;
            existingUser.CreatedOn = user.CreatedOn;
            existingUser.ModifiedOn = user.ModifiedOn;

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating user: {ex.Message}");
            }
        }

        // Methods for UserType Entity
        public async Task<IEnumerable<User>> ListByUserTypeId(Guid userTypeId)
        {
            return await _userRepository.ListByUserTypeIdAsync(userTypeId);
        }

        public async Task<UserResponse> AssignUserToUserType(Guid userTypeId, Guid userId)
        {
            try
            {
                await _userRepository.AssingUserToUserType(userTypeId, userId);
                await _unitOfWork.CompleteAsync();
                User user = await _userRepository.FindById(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurrend while assigning user to userType: {ex.Message}");
            }
        }

        public async Task<UserResponse> UnassignUserToUserType(Guid userTypeId, Guid userId)
        {
            try
            {
                await _userRepository.UnassingUserToUserType(userTypeId, userId);
                await _unitOfWork.CompleteAsync();
                User user = await _userRepository.FindById(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurrend while unassigning user to userType: {ex.Message}");
            }
        }

    }
}
