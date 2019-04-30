using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        private static List<MovieRating> movieRatings = new List<MovieRating>();
        private static int nextId = 1;

        public decimal GetAverageRatingByMovieName(string movieName)
        {
            return (decimal)movieRatings
                .Where(m=> m.Movie == movieName)
                .Average(m => m.Rating);
        }

        public List<int> GetIds()
        {
            return movieRatings.Select(m => m.Id).ToList();
        }

        public string GetMovieNameById(int id)
        {
            return movieRatings.Where(r => r.Id == id).Select(r=> r.Movie).SingleOrDefault();
        }

        public int GetRatingById(int id)
        {
            return movieRatings
                .Where(m => m.Id == id)
                .Select(m=> m.Rating)
                .SingleOrDefault();
        }

        public int SaveRating(string movieName, int rating)
        {
            MovieRating movieRating = new MovieRating { Id = nextId, Movie = movieName, Rating = rating };
            movieRatings.Add(movieRating);
            nextId++;
            return movieRating.Id;
        }
    }

    class MovieRating
    {
        public int Id { get; set; }
        public string Movie { set; get; }
        public int Rating { get; set; }
    }
}
