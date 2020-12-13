using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MoviesApp.Func
{
    public class MoviesFunction
    {
        private readonly IMoviesService _moviesService;

        public MoviesFunction(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [FunctionName("GetByTitle")]
        public async Task<IActionResult> GetByTitle([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string title = req.Query["title"];
            if (string.IsNullOrEmpty(title)) return new BadRequestResult();

            var result = await _moviesService.GetByTitle(title).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [FunctionName("SearchByTitle")]
        public async Task<IActionResult> SearchByTitle([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string title = req.Query["title"];
            if (string.IsNullOrEmpty(title)) return new BadRequestResult();

            var result = await _moviesService.SearchByTitle(title).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [FunctionName("GetByImdbId")]
        public async Task<IActionResult> GetByImdbId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            string id = req.Query["id"];
            if (string.IsNullOrEmpty(id)) return new BadRequestResult();

            var result = await _moviesService.GetByImdbId(id).ConfigureAwait(false);
            return new OkObjectResult(result);
        }
    }
}
