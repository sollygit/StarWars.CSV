using System.Collections.Generic;

namespace StarWars.Model
{
    public class Movie
    {
        public string? MovieId { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Poster { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();

        public override string ToString()
        {
            return $"{MovieId}:[{MovieRatings.Count} Ratings] {Title},{Year},{Price},{Poster}";
        }
    }
}
