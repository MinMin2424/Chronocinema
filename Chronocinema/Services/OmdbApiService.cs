using Chronocinema.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Chronocinema
{
    public static class OmdbApiService
    {
        private const string ApiKey = "5548ada7";
        private const string BaseUrl = "https://www.omdbapi.com/";

        public static async Task<MediaItem> SearchMedia(string tite)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync($"{BaseUrl}?apikey={ApiKey}&t={Uri.EscapeDataString(tite)}");

                var json = JObject.Parse(response);

                if (json["Response"]?.ToString() == "False")
                {
                    return null;
                }

                return new MediaItem
                {
                    Title = json["Title"]?.ToString(),
                    Type = json["Type"]?.ToString() == "movie" ? MediaType.Movie : MediaType.Series,
                    Year = json["Year"]?.ToString(),
                    Genre = json["Genre"]?.ToString(),
                    Country = json["Country"]?.ToString(),
                    Language = json["Language"]?.ToString(),
                    PosterUrl = json["Poster"]?.ToString(),
                    Status = WatchingStatus.Planning,
                    Rating = 0.0,
                    StartDate = null,
                    EndDate = null,
                    Notes = string.Empty
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
