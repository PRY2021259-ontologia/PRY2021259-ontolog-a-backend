using Ontologia.API.Domain.Services;
using Ontologia.API.Extensions;
using Ontologia.API.Resources;

namespace Ontologia.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IRestClientExtensions _restClientExtensions;
        private readonly IConfiguration _configuration;

        public SearchService(IRestClientExtensions restClientExtensions, IConfiguration configuration)
        {
            _restClientExtensions = restClientExtensions;
            _configuration = configuration;
        }

        public async Task<PlantDiseaseByOntologyResource?> GetPlantDiseaseByOntologyId(string id)
        {
            var baseUri = _configuration.GetSection("Ontology:BaseUrl").Value;
            var routeGetOrdenDataUri =
                _configuration.GetSection("Ontology:GetInfeccion").Value;
            routeGetOrdenDataUri = routeGetOrdenDataUri.Replace("{{infeccionId}}", id);
            var requestUri = baseUri + routeGetOrdenDataUri;
            var response = await _restClientExtensions.GetAsync(requestUri);
            var responseModel = await HttpExtensions.HttpResponseMessageToJson<PlantDiseaseByOntologyResource>(response);
            return responseModel;
        }
    }
}
