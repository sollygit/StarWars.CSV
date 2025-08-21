using StarWars.Model;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Repository
{
    public class StarWarsSearchProvider
    {
        readonly IEnumerable<Movie> movies;
        readonly IEnumerable<MovieRating> movieRatings;

        public StarWarsSearchProvider(IEnumerable<Movie> movies, IEnumerable<MovieRating> movieRatings)
        {
            this.movies = movies;
            this.movieRatings = movieRatings;
        }

        public Movie Lookup(string movieID)
        {
            var item = movies.FirstOrDefault(m => m.MovieId == movieID);

            if (item == null) return null!;
            
            item.MovieRatings = movieRatings.Where(r => r.MovieId == movieID).ToList();
            
            return item;
        }
    }
}
