using Microsoft.Extensions.DependencyInjection;
using StarWars.Service;
using StarWars.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StarWars.CSV
{
    static class StarWars
    {
        static readonly IStarWarsService starwarsService = Program.ServiceProvider.GetService<IStarWarsService>()!;

        public static async Task Movies()
        {
            var movies = await starwarsService.GetMovies();

            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
        }

        public static async Task MoviesRatings()
        {
            var ratings = await starwarsService.GetMoviesRatings();

            foreach (var rating in ratings)
            {
                Console.WriteLine(rating);
            }
        }

        public static async Task Lookup()
        {
            string movieID;
            Console.Write("Please enter a MovieID:");
            while (string.IsNullOrEmpty(movieID = Console.ReadLine()!.Trim()))
            {
                Console.WriteLine("Your input cannot be empty or whitespace, please try again:");
            }

            var item = await starwarsService.Lookup(movieID);

            if (item == null)
            {
                Console.WriteLine($"No movie found with ID:{movieID}");
                return;
            }

            var json = JToken.Parse(item.ToJson()).ToString(Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
