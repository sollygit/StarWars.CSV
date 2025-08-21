using System;

namespace StarWars.Model
{
    public class MovieRating
    {
        public string? MovieId { get; set; }
        public string? Rated { get; set; }
        public DateTime Released { get; set; }
        public string? Runtime { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Language { get; set; }
        public int Metascore { get; set; }
        public decimal Ratings { get; set; }

        public override string ToString()
        {
            return $"{MovieId},{Rated},{Released},{Runtime},{Genre},{Director},{Language},{Metascore},{Ratings}";
        }
    }
}
