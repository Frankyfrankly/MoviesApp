namespace MoviesApp.Api
{
    public class AppSettings
    {
        public MoviesApiSettings MoviesApi { get; set; }
    }

    public class MoviesApiSettings
    {
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
