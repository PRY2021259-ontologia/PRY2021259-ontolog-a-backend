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
            CreateMap<SaveConceptTypeResource, ConceptType>();
            CreateMap<SaveUserTypeResource, UserType>();
            CreateMap<SaveSuggestionTypeResource, SuggestionType>();
            CreateMap<SaveCategoryDiseaseResource, CategoryDisease>();
            CreateMap<SavePlantDiseaseResource, PlantDisease>();
            CreateMap<SaveUserLoginResource, UserLogin>();
            CreateMap<SaveSuggestionStatusResource, SuggestionStatus>();
            CreateMap<SaveStatusTypeResource, StatusType>();
        }
    }
}
