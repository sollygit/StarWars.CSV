using StarWars.Model;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Repository
{
    public class StarWarsSearchProvider
    {
        readonly IEnumerable<Movie> movies;
        readonly IEnumerable<MovieRating> moviesRatings;

        public StarWarsSearchProvider(IEnumerable<Movie> movies, IEnumerable<MovieRating> moviesRatings)
        {
            this.movies = movies;
            this.moviesRatings = moviesRatings;

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
