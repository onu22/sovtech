using System;
namespace Open.API.config
{
    public class UrlsConfig
    {
        public string Chucknorris { get; set; }
        public string Swapi { get; set; }

        public class ChuckNorrisOperations
        {
            public static string GetAllCategories() => "/jokes/categories";
            public static string SearchJokes(string query) => $"/jokes/search?query={query}";
        }

        public class SwapAPIOperations
        {
            public static string GetPeople() => "/api/people/";
            public static string SearchPeople(string query) => $"/api/people/?search={query}";
        }

      
    }
}
