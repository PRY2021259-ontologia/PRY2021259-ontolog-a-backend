using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserSuggestionService : IUserSuggestionService
    {
        public readonly IUserSuggestionRepository _userSuggestionRepository;
        public readonly IUnitOfWork _unitOfWork;

        public UserSuggestionService(IUserSuggestionRepository userSuggestionRepository, IUnitOfWork unitOfWork)
        {
            _userSuggestionRepository = userSuggestionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserSuggestionResponse> AssignUserSuggestion(int userId, int userSuggestionId)
        {
            try
            {
                await _userSuggestionRepository.AssingUserSuggestion(userId, userSuggestionId);
                await _unitOfWork.CompleteAsync();
                UserSuggestion userSuggestion= await _userSuggestionRepository.GetById(userSuggestionId);
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while assigning userSuggestion to user: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> Delete(int userSuggestionId)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null)
                return new UserSuggestionResponse("UserSuggestion not found");
            try
            {
                _userSuggestionRepository.Remove(existingUserSuggestion);
                await _unitOfWork.CompleteAsync();
                return new UserSuggestionResponse(existingUserSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while deleting userSuggestion: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> GetById(int userSuggestionId)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null)
                return new UserSuggestionResponse("UserSuggestion not found");
            return new UserSuggestionResponse(existingUserSuggestion);
        }

        public async Task<IEnumerable<UserSuggestion>> ListAsync()
        {
            return await _userSuggestionRepository.ListAsync();
        }

        public async Task<IEnumerable<UserSuggestion>> ListByUserId(int userId)
        {
            return await _userSuggestionRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserSuggestionResponse> SaveAsync(UserSuggestion userSuggestion)
        {
            try
            {
                await _userSuggestionRepository.AddAsync(userSuggestion);
                await _unitOfWork.CompleteAsync();
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error while saving userSuggestion:{ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> UnassignUserSuggestion(int userId, int userSuggestionId)
        {
            try
            {
                await _userSuggestionRepository.UnassingUserSuggestion(userId, userSuggestionId);
                await _unitOfWork.CompleteAsync();
                UserSuggestion userSuggestion= await _userSuggestionRepository.GetById(userSuggestionId);
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while unassigning userSuggestion to user: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> Update(int userSuggestionId, UserSuggestion userSuggestion)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null)
                return new UserSuggestionResponse("UserSuggestion not found");

            existingUserSuggestion.Comment = userSuggestion.Comment;
            existingUserSuggestion.OptionalEmail = userSuggestion.OptionalEmail;
            existingUserSuggestion.IsActive = userSuggestion.IsActive;
            existingUserSuggestion.CreatedOn = userSuggestion.CreatedOn;
            existingUserSuggestion.ModifiedOn = userSuggestion.ModifiedOn;

            try
            {
                _userSuggestionRepository.Update(existingUserSuggestion);
                await _unitOfWork.CompleteAsync();
                return new UserSuggestionResponse(existingUserSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error while updating userSuggestion: {ex.Message}");
            }
        }
    }
}
