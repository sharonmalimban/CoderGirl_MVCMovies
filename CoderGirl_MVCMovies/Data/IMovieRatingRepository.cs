using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public interface IMovieRatingRepository
    {
        int Save(MovieRating movieRating);

        List<MovieRating> GetMovieRatings();

        MovieRating GetById(int id);

        void Update(MovieRating movieRating);

        void Delete(int id);
    }
}
