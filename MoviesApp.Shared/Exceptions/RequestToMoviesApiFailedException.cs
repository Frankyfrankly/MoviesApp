using System;
using System.Net;

namespace MoviesApp.Shared.Exceptions
{
    public class RequestToMoviesApiFailedException : Exception
    {
        private static readonly string _defaultMessage = "Failed to send request to Movies API.";
        public HttpStatusCode HttpStatusCode { get; }
        public string ReasonPhrase { get; }
        public string MoviesApiErrorMessage { get; }
        public RequestToMoviesApiFailedException(HttpStatusCode httpStatusCode, string reasonPhrase, string moviesApiErrorMessage) : base(_defaultMessage)
        {
            HttpStatusCode = httpStatusCode;
            ReasonPhrase = reasonPhrase;
            MoviesApiErrorMessage = moviesApiErrorMessage;
        }
    }
}
