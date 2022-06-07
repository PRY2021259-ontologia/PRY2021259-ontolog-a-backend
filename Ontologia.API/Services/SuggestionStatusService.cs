using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class SuggestionStatusService : ISuggestionStatusService
    {
        private readonly ISuggestionStatusRepository _suggestionStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SuggestionStatusService(ISuggestionStatusRepository suggestionStatusRepository, IUnitOfWork unitOfWork)
        {
            _suggestionStatusRepository = suggestionStatusRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods
        public async Task<SuggestionStatusResponse> SaveAsync(SuggestionStatus suggestionStatus)
        {
            try
            {
                await _suggestionStatusRepository.AddAsync(suggestionStatus);
                await _unitOfWork.CompleteAsync();

                return new SuggestionStatusResponse(suggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error while saving SuggestionStatus:{ex.Message}");
            }
        }

        public async Task<IEnumerable<SuggestionStatus>> ListAsync()
        {
            var all = await _suggestionStatusRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<SuggestionStatusResponse> GetById(Guid suggestionStatusId)
        {
            var existingSuggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
            if (existingSuggestionStatus == null || !existingSuggestionStatus.IsActive)
                return new SuggestionStatusResponse("SuggestionStatus Not Found");
            return new SuggestionStatusResponse(existingSuggestionStatus);
        }

        public async Task<SuggestionStatusResponse> Update(Guid suggestionStatusId, SuggestionStatus suggestionStatus)
        {
            var existingSuggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
            if (existingSuggestionStatus == null)
                return new SuggestionStatusResponse("SuggestionStatus Not Found");

            existingSuggestionStatus.SuggestionStatusTitle = suggestionStatus.SuggestionStatusTitle;
            existingSuggestionStatus.SuggestionStatusDescription = suggestionStatus.SuggestionStatusDescription;
            existingSuggestionStatus.Url = suggestionStatus.Url;
            existingSuggestionStatus.IsProcessed = suggestionStatus.IsProcessed;
            existingSuggestionStatus.IsActive = suggestionStatus.IsActive;
            existingSuggestionStatus.CreatedOn = suggestionStatus.CreatedOn;
            existingSuggestionStatus.ModifiedOn = suggestionStatus.ModifiedOn;

            try
            {
                _suggestionStatusRepository.Update(existingSuggestionStatus);
                await _unitOfWork.CompleteAsync();
                return new SuggestionStatusResponse(existingSuggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error while updating SuggestionStatus: {ex.Message}");
            }
        }

        public async Task<SuggestionStatusResponse> Delete(Guid suggestionStatusId)
        {
            var existingSuggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
            if (existingSuggestionStatus == null)
                return new SuggestionStatusResponse("SuggestionStatus Not Found");
            try
            {
                existingSuggestionStatus.IsActive = false;
                _suggestionStatusRepository.Update(existingSuggestionStatus);
                await _unitOfWork.CompleteAsync();
                return new SuggestionStatusResponse(existingSuggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error ocurrend while deleting SuggestionStatus: {ex.Message}");
            }
        }

        // Methods for StatusType Entity
        public async Task<IEnumerable<SuggestionStatus>> ListByStatusTypeId(Guid statusTypeId)
        {
            return await _suggestionStatusRepository.ListByStatusTypeIdAsync(statusTypeId);
        }

        public async Task<SuggestionStatusResponse> AssingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            try
            {
                await _suggestionStatusRepository.AssingSuggestionStatusToStatusType(statusTypeId, suggestionStatusId);
                await _unitOfWork.CompleteAsync();
                SuggestionStatus suggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
                return new SuggestionStatusResponse(suggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error ocurrend while assigning SuggestionStatus to StatusType: {ex.Message}");
            }
        }

        public async Task<SuggestionStatusResponse> UnassingSuggestionStatusToStatusType(Guid statusTypeId, Guid suggestionStatusId)
        {
            try
            {
                await _suggestionStatusRepository.UnassingSuggestionStatusToStatusType(statusTypeId, suggestionStatusId);
                await _unitOfWork.CompleteAsync();
                SuggestionStatus suggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
                return new SuggestionStatusResponse(suggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error ocurrend while unassigning SuggestionStatus to StatusType: {ex.Message}");
            }
        }

        // Methods for UserSuggestion Entity
        public async Task<IEnumerable<SuggestionStatus>> ListByUserSuggestionId(Guid userSuggestionId)
        {
            return await _suggestionStatusRepository.ListByUserSuggestionIdAsync(userSuggestionId);
        }

        public async Task<SuggestionStatusResponse> AssingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            try
            {
                await _suggestionStatusRepository.AssingSuggestionStatusToUserSuggestion(userSuggestionId, suggestionStatusId);
                await _unitOfWork.CompleteAsync();
                SuggestionStatus suggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
                return new SuggestionStatusResponse(suggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error ocurrend while assigning SuggestionStatus to UserSuggestion: {ex.Message}");
            }
        }

        public async Task<SuggestionStatusResponse> UnassingSuggestionStatusToUserSuggestion(Guid userSuggestionId, Guid suggestionStatusId)
        {
            try
            {
                await _suggestionStatusRepository.UnassingSuggestionStatusToUserSuggestion(userSuggestionId, suggestionStatusId);
                await _unitOfWork.CompleteAsync();
                SuggestionStatus suggestionStatus = await _suggestionStatusRepository.GetById(suggestionStatusId);
                return new SuggestionStatusResponse(suggestionStatus);
            }
            catch (Exception ex)
            {
                return new SuggestionStatusResponse($"An error ocurrend while unassigning SuggestionStatus to UserSuggestion: {ex.Message}");
            }
        }
    }
}
