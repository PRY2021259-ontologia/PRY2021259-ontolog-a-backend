using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class StatusTypeService : IStatusTypeService
    {
        private readonly IStatusTypeRepository _statusTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StatusTypeService(IStatusTypeRepository statusTypeRepository, IUnitOfWork unitOfWork)
        {
            _statusTypeRepository = statusTypeRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

        public async Task<IEnumerable<StatusType>> ListAsync()
        {
            var all = await _statusTypeRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<StatusTypeResponse> DeleteAsync(Guid id)
        {
            var existingStatusType = await _statusTypeRepository.FindById(id);

            if (existingStatusType == null)
                return new StatusTypeResponse("StatusType Not Found");

            try
            {
                existingStatusType.IsActive = false;
                _statusTypeRepository.Update(existingStatusType);
                await _unitOfWork.CompleteAsync();

                return new StatusTypeResponse(existingStatusType);
            }
            catch (Exception ex)
            {
                return new StatusTypeResponse($"An error ocurred while deleting StatusType: {ex.Message}");
            }

        }

        public async Task<StatusTypeResponse> GetByIdAsync(Guid id)
        {
            var existingStatusType = await _statusTypeRepository.FindById(id);

            if (existingStatusType == null || !existingStatusType.IsActive)
                return new StatusTypeResponse("StatusType Not Found");

            return new StatusTypeResponse(existingStatusType);
        }

        public async Task<StatusTypeResponse> SaveAsync(StatusType statusType)
        {
            try
            {
                await _statusTypeRepository.AddAsync(statusType);
                await _unitOfWork.CompleteAsync();

                return new StatusTypeResponse(statusType);
            }
            catch (Exception ex)
            {
                return new StatusTypeResponse($"An error ocurred while saving the StatusType: {ex.Message}");
            }
        }

        public async Task<StatusTypeResponse> UpdateAsync(Guid id, StatusType statusType)
        {
            var existingStatusType = await _statusTypeRepository.FindById(id);

            if (existingStatusType == null)
                return new StatusTypeResponse("StatusType Not Found");

            existingStatusType.StatusTypeTitle = statusType.StatusTypeTitle;
            existingStatusType.StatusTypeDescription = statusType.StatusTypeDescription;
            existingStatusType.IsActive = statusType.IsActive;
            existingStatusType.CreatedOn = statusType.CreatedOn;
            existingStatusType.ModifiedOn = statusType.ModifiedOn;


            try
            {
                _statusTypeRepository.Update(existingStatusType);
                await _unitOfWork.CompleteAsync();

                return new StatusTypeResponse(existingStatusType);
            }
            catch (Exception ex)
            {
                return new StatusTypeResponse($"An error ocurred while updating StatusType: {ex.Message}");
            }
        }

    }
}
