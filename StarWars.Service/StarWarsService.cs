using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StarWars.Common;
using StarWars.Model;
using StarWars.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Service
{
    public interface IStarWarsService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<IEnumerable<MovieRating>> GetMoviesRatings();
        Task<Movie> Lookup(string movieID);
    }

    public class StarWarsService : IStarWarsService
    {
        readonly ILogger logger;
        readonly IConfiguration configuration;
        IEnumerable<Movie> movies = null!;
        IEnumerable<MovieRating> moviesRatings = null!;
        StarWarsSearchProvider SearchProvider = null!;

        public StarWarsService(ILogger<StarWarsService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            InitStarWars();
        }

        private void InitStarWars()
        {
            try
            {
                movies = Deserializer.FromCsv<Movie>(configuration["Movies"], new string[] { 
                    "MovieId", "Title", "Year", "Type", "Poster", "Price", "IsActive" });
                moviesRatings = Deserializer.FromCsv<MovieRating>(configuration["MovieRatings"], new string[] { 
                    "MovieId", "Rated", "Released", "Runtime", "Genre", "Director", "Language", "Metascore", "Ratings" })
                    .Where(o => o.Ratings > 0.5m);

                SearchProvider = new StarWarsSearchProvider(movies, moviesRatings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public Task<IEnumerable<Movie>> GetMovies()
        {
            return Task.FromResult(movies);
        }

        public Task<IEnumerable<MovieRating>> GetMoviesRatings()
        {
            return Task.FromResult(moviesRatings);
        }

        public Task<Movie> Lookup(string movieID)
        {
            return Task.FromResult(SearchProvider.Lookup(movieID));
        }
    }
}
