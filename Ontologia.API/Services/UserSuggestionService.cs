using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserSuggestionService : IUserSuggestionService
    {
        private readonly IUserSuggestionRepository _userSuggestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserSuggestionService(IUserSuggestionRepository userSuggestionRepository, IUnitOfWork unitOfWork)
        {
            _userSuggestionRepository = userSuggestionRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

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

        public async Task<UserSuggestionResponse> Delete(Guid userSuggestionId)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null)
                return new UserSuggestionResponse("UserSuggestion Not Found");
            try
            {
                existingUserSuggestion.IsActive = false;
                _userSuggestionRepository.Update(existingUserSuggestion);
                await _unitOfWork.CompleteAsync();
                return new UserSuggestionResponse(existingUserSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while deleting userSuggestion: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> GetById(Guid userSuggestionId)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null || !existingUserSuggestion.IsActive)
                return new UserSuggestionResponse("UserSuggestion Not Found");
            return new UserSuggestionResponse(existingUserSuggestion);
        }

        public async Task<IEnumerable<UserSuggestion>> ListAsync()
        {
            var all = await _userSuggestionRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserSuggestionResponse> Update(Guid userSuggestionId, UserSuggestion userSuggestion)
        {
            var existingUserSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
            if (existingUserSuggestion == null)
                return new UserSuggestionResponse("UserSuggestion Not Found");

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

        // Methods for User Entity
        public async Task<IEnumerable<UserSuggestion>> ListByUserId(Guid userId)
        {
            return await _userSuggestionRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserSuggestionResponse> AssignUserSuggestion(Guid userId, Guid userSuggestionId)
        {
            try
            {
                await _userSuggestionRepository.AssingUserSuggestion(userId, userSuggestionId);
                await _unitOfWork.CompleteAsync();
                UserSuggestion userSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while assigning userSuggestion to user: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> UnassignUserSuggestion(Guid userId, Guid userSuggestionId)
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

        // Methods for SuggestionType Entity
        public async Task<IEnumerable<UserSuggestion>> ListBySuggestionTypeId(Guid suggestionTypeId)
        {
            return await _userSuggestionRepository.ListBySuggestionTypeIdAsync(suggestionTypeId);
        }

        public async Task<UserSuggestionResponse> AssignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            try
            {
                await _userSuggestionRepository.AssingUserSuggestionToSuggestionType(suggestionTypeId, userSuggestionId);
                await _unitOfWork.CompleteAsync();
                UserSuggestion userSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while assigning userSuggestion to suggestionType: {ex.Message}");
            }
        }

        public async Task<UserSuggestionResponse> UnassignUserSuggestionToSuggestionType(Guid suggestionTypeId, Guid userSuggestionId)
        {
            try
            {
                await _userSuggestionRepository.UnassingUserSuggestionToSuggestionType(suggestionTypeId, userSuggestionId);
                await _unitOfWork.CompleteAsync();
                UserSuggestion userSuggestion = await _userSuggestionRepository.GetById(userSuggestionId);
                return new UserSuggestionResponse(userSuggestion);
            }
            catch (Exception ex)
            {
                return new UserSuggestionResponse($"An error ocurrend while unassigning userSuggestion to suggestionType: {ex.Message}");
            }
        }
    }
}
