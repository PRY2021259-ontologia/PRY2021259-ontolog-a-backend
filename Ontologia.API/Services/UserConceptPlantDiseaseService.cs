using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserConceptPlantDiseaseService : IUserConceptPlantDiseaseService
    {
        private readonly IUserConceptPlantDiseaseRepository _userConceptPlantDiseaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserConceptPlantDiseaseService(IUserConceptPlantDiseaseRepository userConceptPlantDiseaseRepository, IUnitOfWork unitOfWork)
        {
            _userConceptPlantDiseaseRepository = userConceptPlantDiseaseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListAsync()
        {
            return await _userConceptPlantDiseaseRepository.ListAsync();
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByPlantDiseaseIdAsync(Guid plantDiseaseId)
        {
            return await _userConceptPlantDiseaseRepository.ListByPlantDiseaseIdAsync(plantDiseaseId);
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAsync(Guid userConceptId)
        {
            return await _userConceptPlantDiseaseRepository.ListByUserConceptIdAsync(userConceptId);
        }

        public async Task<IEnumerable<UserConceptPlantDisease>> ListByUserConceptIdAndPlantDiseaseIdAsync(Guid userConceptId, Guid plantDiseaseId)
        {
            return await _userConceptPlantDiseaseRepository.ListByUserConceptIdAndPlantDiseaseIdAsync(userConceptId, plantDiseaseId);
        }

        public async Task<UserConceptPlantDiseaseResponse> AssignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId)
        {
            try
            {
                await _userConceptPlantDiseaseRepository.AssignUserConceptPlantDisease(userConceptId, plantDiseaseId);
                await _unitOfWork.CompleteAsync();
                UserConceptPlantDisease userConceptPlantDisease = await _userConceptPlantDiseaseRepository.FindByUserConceptIdAndPlantDiseaseId(userConceptId, plantDiseaseId);
                return new UserConceptPlantDiseaseResponse(userConceptPlantDisease);

            }
            catch (Exception ex)
            {
                return new UserConceptPlantDiseaseResponse($"An error ocurred while assigning UserConcept to PlantDisease: {ex.Message}");
            }
        }

        public async Task<UserConceptPlantDiseaseResponse> UnassignUserConceptPlantDiseaseAsync(Guid userConceptId, Guid plantDiseaseId)
        {
            try
            {
                UserConceptPlantDisease userConceptPlantDisease = await _userConceptPlantDiseaseRepository.FindByUserConceptIdAndPlantDiseaseId(userConceptId, plantDiseaseId);

                _userConceptPlantDiseaseRepository.Remove(userConceptPlantDisease);
                await _unitOfWork.CompleteAsync();

                return new UserConceptPlantDiseaseResponse(userConceptPlantDisease);

            }
            catch (Exception ex)
            {
                return new UserConceptPlantDiseaseResponse($"An error ocurred while assigning UserConcept to PlantDisease: {ex.Message}");
            }
        }

    }
}
