using AutoMapper;
using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;
using Ontologia.API.Resources;

namespace Ontologia.API.Services
{
    public class PlantDiseaseService : IPlantDiseaseService
    {
        private readonly IPlantDiseaseRepository _plantDiseaseRepository;
        private readonly IUserConceptPlantDiseaseRepository _userConceptPlantDiseaseRepository;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlantDiseaseService(IPlantDiseaseRepository plantDiseaseRepository, IUnitOfWork unitOfWork, IUserConceptPlantDiseaseRepository userConceptPlantDiseaseRepository, ISearchService searchService, IMapper mapper)
        {
            _plantDiseaseRepository = plantDiseaseRepository;
            _userConceptPlantDiseaseRepository = userConceptPlantDiseaseRepository;
            _searchService = searchService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // General Methods
        public async Task<PlantDiseaseResponse> SaveAsync(PlantDisease plantDisease)
        {
            try
            {
                await _plantDiseaseRepository.AddAsync(plantDisease);
                await _unitOfWork.CompleteAsync();

                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error while saving PlantDisease:{ex.Message}");
            }
        }

        public async Task<IEnumerable<PlantDisease>> ListAsync()
        {
            var all = await _plantDiseaseRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<PlantDiseaseResponse?> GetByOntologyId(string ontologyId)
        {
            try
            {
                ontologyId = ontologyId.ToLower();
                var existingPlantDisease = await _plantDiseaseRepository.GetByOntologyId(ontologyId);
                if (existingPlantDisease != null) return new PlantDiseaseResponse(existingPlantDisease);

                var plantDiseaseByOntology = await _searchService.GetPlantDiseaseByOntologyId(ontologyId);
                if (plantDiseaseByOntology == null) return new PlantDiseaseResponse("PlantDisease Not Found");
                var plantDiseaseToRegister = new SavePlantDiseaseResource()
                {
                    PlantDiseaseName = plantDiseaseByOntology.NombreComun,
                    PlantDiseaseDescription = plantDiseaseByOntology.Descripcion
                };
                var plantDisease = _mapper.Map<SavePlantDiseaseResource, PlantDisease>(plantDiseaseToRegister);
                plantDisease.OntologyId = ontologyId;
                var plantRegistered = await SaveAsync(plantDisease);
                existingPlantDisease = plantRegistered.Resource;
                return new PlantDiseaseResponse(existingPlantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error while getting PlantDisease: {ex.Message}");
            }

        }
        public async Task<PlantDiseaseResponse> GetById(Guid plantDiseaseId)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null || !existingPlantDisease.IsActive)
                return new PlantDiseaseResponse("PlantDisease Not Found");
            return new PlantDiseaseResponse(existingPlantDisease);
        }

        public async Task<PlantDiseaseResponse> Update(Guid plantDiseaseId, PlantDisease plantDisease)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null)
                return new PlantDiseaseResponse("PlantDisease Not Found");

            existingPlantDisease.PlantDiseaseName = plantDisease.PlantDiseaseName;
            existingPlantDisease.PlantDiseaseDescription = plantDisease.PlantDiseaseDescription;
            existingPlantDisease.IsActive = plantDisease.IsActive;
            existingPlantDisease.CreatedOn = plantDisease.CreatedOn;
            existingPlantDisease.ModifiedOn = plantDisease.ModifiedOn;

            try
            {
                _plantDiseaseRepository.Update(existingPlantDisease);
                await _unitOfWork.CompleteAsync();
                return new PlantDiseaseResponse(existingPlantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error while updating PlantDisease: {ex.Message}");
            }
        }

        public async Task<PlantDiseaseResponse> Delete(Guid plantDiseaseId)
        {
            var existingPlantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
            if (existingPlantDisease == null)
                return new PlantDiseaseResponse("PlantDisease Not Found");
            try
            {
                existingPlantDisease.IsActive = false;
                _plantDiseaseRepository.Update(existingPlantDisease);
                await _unitOfWork.CompleteAsync();
                return new PlantDiseaseResponse(existingPlantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while deleting PlantDisease: {ex.Message}");
            }
        }

        // Methods for CategoryDisease Entity
        public async Task<IEnumerable<PlantDisease>> ListByConceptTypeId(long categoryDiseaseId)
        {
            return await _plantDiseaseRepository.ListByCategoryDiseaseIdAsync(categoryDiseaseId);
        }

        public async Task<PlantDiseaseResponse> AssingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId)
        {
            try
            {
                await _plantDiseaseRepository.AssingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
                await _unitOfWork.CompleteAsync();
                PlantDisease plantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while assigning PlantDisease to conceptType: {ex.Message}");
            }
        }

        public async Task<PlantDiseaseResponse> UnassingPlantDiseaseToCategoryDisease(long categoryDiseaseId, Guid plantDiseaseId)
        {
            try
            {
                await _plantDiseaseRepository.UnassingPlantDiseaseToCategoryDisease(categoryDiseaseId, plantDiseaseId);
                await _unitOfWork.CompleteAsync();
                PlantDisease plantDisease = await _plantDiseaseRepository.GetById(plantDiseaseId);
                return new PlantDiseaseResponse(plantDisease);
            }
            catch (Exception ex)
            {
                return new PlantDiseaseResponse($"An error ocurrend while unassigning PlantDisease to conceptType: {ex.Message}");
            }
        }

        // Methods for UserConceptPlantDisease Entity
        public async Task<IEnumerable<PlantDisease>> ListByUserConceptIdAsync(Guid userConceptId)
        {
            var userConceptsPlantDiseases = await _userConceptPlantDiseaseRepository.ListByUserConceptIdAsync(userConceptId);
            var plantDiseases = userConceptsPlantDiseases.Select(pt => pt.PlantDisease).ToList();
            return plantDiseases;
        }
    }
}
