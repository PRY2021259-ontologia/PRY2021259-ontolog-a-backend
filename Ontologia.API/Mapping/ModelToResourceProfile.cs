using AutoMapper;
using Ontologia.API.Domain.Models;
using Ontologia.API.Resources;

namespace Ontologia.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<UserConcept, UserConceptResource>();
            CreateMap<UserSuggestion, UserSuggestionResource>();
            CreateMap<UserHistory, UserHistoryResource>();
            CreateMap<ConceptType, ConceptTypeResource>();
            CreateMap<UserType, UserTypeResource>();
            CreateMap<SuggestionType, SuggestionTypeResource>();
            CreateMap<CategoryDisease, CategoryDiseaseResource>();
            CreateMap<PlantDisease, PlantDiseaseResource>();
            CreateMap<UserLogin, UserLoginResource>();
            CreateMap<SuggestionStatus, SuggestionStatusResource>();
            CreateMap<StatusType, StatusTypeResource>();
        }
    }
}
