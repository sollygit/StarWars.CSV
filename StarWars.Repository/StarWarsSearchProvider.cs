using StarWars.Model;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Repository
{
    public class StarWarsSearchProvider
    {
        readonly IEnumerable<Movie> movies;

        public StarWarsSearchProvider(IEnumerable<Movie> movies, IEnumerable<MovieRating> moviesRatings)
        {
            this.movies = movies;

            // Populate MovieRatings
            foreach (var movie in movies!)
            {
                movie.MovieRatings = moviesRatings.Where(r => r.MovieId == movie.MovieId).ToList();
            }
        }

        public Movie Lookup(string movieID)
        {
            return movies.FirstOrDefault(m => m.MovieId == movieID);
        }
    }
}
