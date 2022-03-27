using AutoMapper;
using Ontologia.API.Domain.Models;
using Ontologia.API.Extensions;
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
        }
    }
}
