using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Ontologia.API.Extensions
{
    public interface IRestClientExtensions
    {
        Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null);
        Task<HttpResponseMessage> PostAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;

        Task<HttpResponseMessage> PostFormAsync(string requestUri, MultipartFormDataContent multipartRequest, Dictionary<string, string> additionalHeaders = null);
        Task<HttpResponseMessage> DeleteAsync(string requestUri, Dictionary<string, string> additionalHeaders = null);
        Task<HttpResponseMessage> PutAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;
        Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;
    }

    public sealed class RestClientExtensions
 : IRestClientExtensions
    {
        public async Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null)
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);
            return await httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);
            var httpContent = new StringContent(JsonConvert.SerializeObject(request))
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            return await httpClient.PostAsync(requestUri, httpContent);
        }

        public async Task<HttpResponseMessage> PostFormAsync(string requestUri, MultipartFormDataContent multipartRequest, Dictionary<string, string> additionalHeaders = null)
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);
            return await httpClient.PostAsync(requestUri, multipartRequest);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, Dictionary<string, string> additionalHeaders = null)
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);
            return await httpClient.DeleteAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await httpClient.PutAsync(requestUri, httpContent);
        }

        public async Task<HttpResponseMessage> PatchAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class
        {
            using HttpClientHandler httpClientHandler = new HttpClientHandler();
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            AddHeaders(httpClient, additionalHeaders);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await httpClient.PatchAsync(requestUri, httpContent);
        }

        private void AddHeaders(HttpClient httpClient, Dictionary<string, string> additionalHeaders)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            if (additionalHeaders == null) return;

            foreach (KeyValuePair<string, string> current in additionalHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(current.Key, current.Value);
            }
        }
    }
}
