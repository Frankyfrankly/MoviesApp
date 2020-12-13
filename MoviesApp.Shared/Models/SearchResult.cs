using System.Collections.Generic;

namespace MoviesApp.Shared.Models
{
    public class SearchResult
    {
        public List<MovieBase> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
