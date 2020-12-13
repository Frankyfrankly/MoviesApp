using Microsoft.AspNetCore.Mvc;
using MoviesApp.Shared.Models;
using System.Threading.Tasks;

namespace MoviesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet("GetByTitle")]
        public Task<Movie> GetByTitle([FromQuery] string title) => _moviesService.GetByTitle(title);

        [HttpGet("SearchByTitle")]
        public Task<SearchResult> SearchByTitle([FromQuery] string title) => _moviesService.SearchByTitle(title);

        [HttpGet("GetByImdbId/{id}")]
        public Task<Movie> GetByImdbId(string id) => _moviesService.GetByImdbId(id);
    }
}
