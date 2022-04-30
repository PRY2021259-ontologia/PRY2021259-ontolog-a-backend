using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserConceptService : IUserConceptService
    {
        private readonly IUserConceptRepository _userConceptRepository;
        private readonly IUserConceptPlantDiseaseRepository _userConceptPlantDiseaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserConceptService(IUserConceptRepository userConceptRepository, IUnitOfWork unitOfWork, IUserConceptPlantDiseaseRepository userConceptPlantDiseaseRepository)
        {
            _userConceptRepository = userConceptRepository;
            _userConceptPlantDiseaseRepository = userConceptPlantDiseaseRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods
        public async Task<UserConceptResponse> SaveAsync(UserConcept userConcept)
        {
            try
            {
                await _userConceptRepository.AddAsync(userConcept);
                await _unitOfWork.CompleteAsync();

                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error while saving userConcept:{ex.Message}");
            }
        }

        public async Task<IEnumerable<UserConcept>> ListAsync()
        {
            var all = await _userConceptRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserConceptResponse> GetById(Guid userConceptId)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null || !existingUserConcept.IsActive)
                return new UserConceptResponse("UserConcept Not Found");
            return new UserConceptResponse(existingUserConcept);
        }

        public async Task<UserConceptResponse> Update(Guid userConceptId, UserConcept userConcept)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null)
                return new UserConceptResponse("UserConcept Not Found");

            existingUserConcept.UserConceptTitle = userConcept.UserConceptTitle;
            existingUserConcept.UserConceptDescription = userConcept.UserConceptDescription;
            existingUserConcept.Url = userConcept.Url;
            existingUserConcept.IsActive = userConcept.IsActive;
            existingUserConcept.CreatedOn = userConcept.CreatedOn;
            existingUserConcept.ModifiedOn = userConcept.ModifiedOn;

            try
            {
                _userConceptRepository.Update(existingUserConcept);
                await _unitOfWork.CompleteAsync();
                return new UserConceptResponse(existingUserConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error while updating userConcept: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> Delete(Guid userConceptId)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null)
                return new UserConceptResponse("UserConcept Not Found");
            try
            {
                existingUserConcept.IsActive = false;
                _userConceptRepository.Update(existingUserConcept);
                await _unitOfWork.CompleteAsync();
                return new UserConceptResponse(existingUserConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while deleting userConcept: {ex.Message}");
            }
        }


        // Methods for User Entity
        public async Task<IEnumerable<UserConcept>> ListByUserId(Guid userId)
        {
            return await _userConceptRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserConceptResponse> AssignUserConceptToUser(Guid userId, Guid userConceptId)
        {
            try
            {
                await _userConceptRepository.AssingUserConceptToUser(userId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while assigning userConcept to user: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> UnassignUserConceptToUser(Guid userId, Guid userConceptId)
        {
            try
            {
                await _userConceptRepository.UnassingUserConceptToUser(userId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while unassigning userConcept to user: {ex.Message}");
            }
        }

        // Methods for ConceptType Entity
        public async Task<IEnumerable<UserConcept>> ListByConceptTypeId(Guid conceptTypeId)
        {
            return await _userConceptRepository.ListByConceptTypeIdAsync(conceptTypeId);
        }

        public async Task<UserConceptResponse> AssignUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId)
        {
            try
            {
                await _userConceptRepository.AssingUserConceptToConceptType(conceptTypeId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while assigning userConcept to conceptType: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> UnassignUserConceptToConceptType(Guid conceptTypeId, Guid userConceptId)
        {
            try
            {
                await _userConceptRepository.UnassingUserConceptToConceptType(conceptTypeId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while unassigning userConcept to conceptType: {ex.Message}");
            }
        }

        // Methods for UserConceptPlantDisease Entity
        public async Task<IEnumerable<UserConcept>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId)
        {
            var userConceptsPlantDiseases = await _userConceptPlantDiseaseRepository.ListByPlantDiseaseIdAsync(plantDiseaseId);
            var userConcepts = userConceptsPlantDiseases.Select(pt => pt.UserConcept).ToList();
            return userConcepts;
        }
    }
}
