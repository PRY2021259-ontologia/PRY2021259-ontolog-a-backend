using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly IUserHistoryRepository _userHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserHistoryService(IUserHistoryRepository userHistoryRepository, IUnitOfWork unitOfWork)
        {
            _userHistoryRepository = userHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserHistoryResponse> AssignUserHistory(Guid userId, Guid userHistoryId)
        {
            try
            {
                await _userHistoryRepository.AssingUserHistory(userId, userHistoryId);
                await _unitOfWork.CompleteAsync();
                UserHistory userHistory = await _userHistoryRepository.GetById(userHistoryId);
                return new UserHistoryResponse(userHistory);
            }
            catch (Exception ex)
            {
                return new UserHistoryResponse($"An error ocurrend while assigning userHistory to user: {ex.Message}");
            }
        }

        public async Task<UserHistoryResponse> Delete(Guid userHistoryId)
        {
            var existingUserHistory = await _userHistoryRepository.GetById(userHistoryId);
            if (existingUserHistory == null)
                return new UserHistoryResponse("UserHistory Not Found");
            try
            {
                existingUserHistory.IsActive = false;
                _userHistoryRepository.Update(existingUserHistory);
                await _unitOfWork.CompleteAsync();
                return new UserHistoryResponse(existingUserHistory);
            }
            catch (Exception ex)
            {
                return new UserHistoryResponse($"An error ocurrend while deleting userHistory: {ex.Message}");
            }
        }

        public async Task<UserHistoryResponse> GetById(Guid userHistoryId)
        {
            var existingUserHistory = await _userHistoryRepository.GetById(userHistoryId);
            if (existingUserHistory == null || !existingUserHistory.IsActive)
                return new UserHistoryResponse("UserHistory Not Found");
            return new UserHistoryResponse(existingUserHistory);
        }

        public async Task<IEnumerable<UserHistory>> ListAsync()
        {
            var all = await _userHistoryRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<IEnumerable<UserHistory>> ListByUserId(Guid userId)
        {
            return await _userHistoryRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserHistoryResponse> SaveAsync(UserHistory userHistory)
        {
            try
            {
                await _userHistoryRepository.AddAsync(userHistory);
                await _unitOfWork.CompleteAsync();
                return new UserHistoryResponse(userHistory);
            }
            catch (Exception ex)
            {
                return new UserHistoryResponse($"An error while saving userHistory:{ex.Message}");
            }
        }

        public async Task<UserHistoryResponse> UnassignUserHistory(Guid userId, Guid userHistoryId)
        {
            try
            {
                await _userHistoryRepository.UnassingUserHistory(userId, userHistoryId);
                await _unitOfWork.CompleteAsync();
                UserHistory userHistory = await _userHistoryRepository.GetById(userHistoryId);
                return new UserHistoryResponse(userHistory);
            }
            catch (Exception ex)
            {
                return new UserHistoryResponse($"An error ocurrend while unassigning userHistory to user: {ex.Message}");
            }
        }

        public async Task<UserHistoryResponse> Update(Guid userHistoryId, UserHistory userHistory)
        {
            var existingUserHistory = await _userHistoryRepository.GetById(userHistoryId);
            if (existingUserHistory == null)
                return new UserHistoryResponse("UserHistory not found");

            existingUserHistory.Url = userHistory.Url;
            existingUserHistory.TextSearched = userHistory.TextSearched;
            existingUserHistory.IsActive = userHistory.IsActive;
            existingUserHistory.CreatedOn = userHistory.CreatedOn;
            existingUserHistory.ModifiedOn = userHistory.ModifiedOn;

            try
            {
                _userHistoryRepository.Update(existingUserHistory);
                await _unitOfWork.CompleteAsync();
                return new UserHistoryResponse(existingUserHistory);
            }
            catch (Exception ex)
            {
                return new UserHistoryResponse($"An error while updating userHistory: {ex.Message}");
            }
        }
    }
}
