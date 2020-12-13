using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Shared;
using System;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(MoviesApp.Func.Startup))]
namespace MoviesApp.Func
{
    public class Startup : FunctionsStartup
    {
        private const string MoviesApiHttpClientName = "MoviesApiHttpClientName";

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var moviesApiApiUrl = Environment.GetEnvironmentVariable("MoviesApi.ApiUrl");
            if (string.IsNullOrEmpty(moviesApiApiUrl)) throw new ArgumentNullException(moviesApiApiUrl, "A url to Movies API was not provided");

            var moviesApiApiKey = Environment.GetEnvironmentVariable("MoviesApi.ApiKey");
            if (string.IsNullOrEmpty(moviesApiApiKey)) throw new ArgumentNullException(moviesApiApiUrl, "An API key to Movies API was not provided");

            builder.Services.AddHttpClient(MoviesApiHttpClientName, ctx => { ctx.BaseAddress = new Uri(moviesApiApiUrl); });

            //Then set up DI for the TypedClient
            builder.Services.AddSingleton<IMoviesService>(ctx =>
            {
                var clientFactory = ctx.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient(MoviesApiHttpClientName);

                return new MoviesService(httpClient, moviesApiApiKey);
            });
        }
    }
}
