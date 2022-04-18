using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginService(IUserLoginRepository userLoginRepository, IUnitOfWork unitOfWork)
        {
            _userLoginRepository = userLoginRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

        public async Task<IEnumerable<UserLogin>> ListAsync()
        {
            return await _userLoginRepository.ListAsync();
        }

        public async Task<UserLoginResponse> DeleteAsync(Guid id)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null)
                return new UserLoginResponse("UserLogin Not Found");

            try
            {
                _userLoginRepository.Remove(existingUserLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(existingUserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while deleting UserLogin: {ex.Message}");
            }

        }

        public async Task<UserLoginResponse> GetByIdAsync(Guid id)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null)
                return new UserLoginResponse("UserLogin Not Found");

            return new UserLoginResponse(existingUserLogin);
        }

        public async Task<UserLoginResponse> SaveAsync(UserLogin userLogin)
        {
            try
            {
                await _userLoginRepository.AddAsync(userLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(userLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while saving the UserLogin: {ex.Message}");
            }
        }

        public async Task<UserLoginResponse> UpdateAsync(Guid id, UserLogin userLogin)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null)
                return new UserLoginResponse("UserLogin Not Found");

            existingUserLogin.Username = userLogin.Username;
            existingUserLogin.Password = userLogin.Password;
            existingUserLogin.IsActive = userLogin.IsActive;
            existingUserLogin.CreatedOn = userLogin.CreatedOn;
            existingUserLogin.ModifiedOn = userLogin.ModifiedOn;

            try
            {
                _userLoginRepository.Update(existingUserLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(existingUserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while updating UserLogin: {ex.Message}");
            }
        }

        // Methods for UserLoginType Entity
        public async Task<UserLogin> GetByUserId(Guid userId)
        {
            return await _userLoginRepository.GetByUserIdAsync(userId);
        }

        public async Task<UserLoginResponse> AssingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            try
            {
                await _userLoginRepository.AssingUserToUserLogin(userId, userLoginId);
                await _unitOfWork.CompleteAsync();
                UserLogin UserLogin = await _userLoginRepository.FindById(userLoginId);
                return new UserLoginResponse(UserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurrend while assigning User to UserLogin: {ex.Message}");
            }
        }

        public async Task<UserLoginResponse> UnassingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            try
            {
                await _userLoginRepository.UnassingUserToUserLogin(userId, userLoginId);
                await _unitOfWork.CompleteAsync();
                UserLogin UserLogin = await _userLoginRepository.FindById(userLoginId);
                return new UserLoginResponse(UserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurrend while unassigning User to UserLogin: {ex.Message}");
            }
        }
    }
}
