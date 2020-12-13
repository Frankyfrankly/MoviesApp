using MoviesApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp
{
    public interface IMoviesService
    {
        Task<Movie> GetByTitle(string title);
        Task<SearchResult> SearchByTitle(string title);
        Task<Movie> GetByImdbId(string imdbId);
    }
}
