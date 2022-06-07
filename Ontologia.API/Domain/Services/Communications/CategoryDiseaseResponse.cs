using Ontologia.API.Domain.Models;

namespace Ontologia.API.Domain.Services.Communications
{
    public class CategoryDiseaseResponse : BaseResponse<CategoryDisease>
    {
        public CategoryDiseaseResponse(CategoryDisease resource) : base(resource)
        {
        }

        public CategoryDiseaseResponse(string message) : base(message)
        {
        }
    }
}
