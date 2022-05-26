using Newtonsoft.Json;

namespace Ontologia.API.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T?> HttpResponseMessageToJson<T>(HttpResponseMessage response) where T : class
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
