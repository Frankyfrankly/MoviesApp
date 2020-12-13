using MoviesApp.Shared.Exceptions;
using MoviesApp.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Shared
{
    public class MoviesService : IMoviesService
    {
        private const string JsonMediaType = "application/json";

        private readonly string _baseRelativeUrl;

        private readonly HttpClient _moviesApiHttpClient;

        public MoviesService(HttpClient moviesApiHttpClient, string apiKey)
        {
            _moviesApiHttpClient = moviesApiHttpClient;
            _baseRelativeUrl = $"?apikey={apiKey}&";
        }

        public MoviesService(string moviesApiBaseUrl, string apiKey)
        {
            _moviesApiHttpClient = new HttpClient
            {
                BaseAddress = new Uri($"{moviesApiBaseUrl}")
            };
            _baseRelativeUrl = $"?apikey={apiKey}&";
        }

        public Task<Movie> GetByTitle(string title) => SendRequestToMoviesApi<Movie>(HttpMethod.Get, GenerateGetByTitleUrl(title));

        public Task<SearchResult> SearchByTitle(string title) => SendRequestToMoviesApi<SearchResult>(HttpMethod.Get, GenerateSearchByTitleUrl(title));

        public Task<Movie> GetByImdbId(string imdbId) => SendRequestToMoviesApi<Movie>(HttpMethod.Get, GenerateSearchByImdbIdUrl(imdbId));

        private Task<T> SendRequestToMoviesApi<T>(HttpMethod httpMethod, string uri, object content = null) => SendRequestToMoviesApi<T>(CreateRequestMessage(httpMethod, uri, content));

        private async Task<T> SendRequestToMoviesApi<T>(HttpRequestMessage requestMessage)
        {
            var request = await _moviesApiHttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!request.IsSuccessStatusCode)
                throw new RequestToMoviesApiFailedException(request.StatusCode, request.ReasonPhrase, response);

            var result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod httpMethod, string uri, object content)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, uri);
            if (content != null)
            {
                var contentStr = JsonConvert.SerializeObject(content);
                requestMessage.Content = new StringContent(contentStr, Encoding.UTF8, JsonMediaType);
            }

            return requestMessage;
        }

        private string GenerateGetByTitleUrl(string title) => $"{_baseRelativeUrl}t={title}";
        private string GenerateSearchByTitleUrl(string title) => $"{_baseRelativeUrl}s={title}";
        private string GenerateSearchByImdbIdUrl(string imdbId) => $"{_baseRelativeUrl}i={imdbId}";
    }
}
