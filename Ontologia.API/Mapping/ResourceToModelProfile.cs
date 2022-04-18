using AutoMapper;
using Ontologia.API.Domain.Models;
using Ontologia.API.Resources;

namespace Ontologia.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveUserConceptResource, UserConcept>();
            CreateMap<SaveUserSuggestionResource, UserSuggestion>();
            CreateMap<SaveUserHistoryResource, UserHistory>();
            CreateMap<SaveConceptTypeResource, ConceptTypeResource>();
            CreateMap<SaveUserTypeResource, UserTypeResource>();
            CreateMap<SaveSuggestionTypeResource, SuggestionTypeResource>();
            CreateMap<SaveCategoryDiseaseResource, CategoryDiseaseResource>();
            CreateMap<SavePlantDiseaseResource, PlantDiseaseResource>();
            CreateMap<SaveUserLoginResource, UserLoginResource>();
        }
    }
}
