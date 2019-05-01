using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    // TODO: Implement this interface
    public interface IMovieRatingRepository
    {        
        /// <summary>
        /// Given a movieName and rating, saves the rating and returns a unique id > 0.
        /// If the movie name and/or rating are null or empty, nothing should be saved and it should return 0
        /// </summary>
        /// <param name="movieName"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        int Save(string movieName, int rating);      

        /// <summary>
        /// Given a movie name, returns the average rating of of the movie.
        /// If there are no ratings for the movie, returns an empty list.
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns></returns>
        decimal GetAverageRatingByMovieName(string movieName);

        /// <summary>
        /// Returns a list of all the ids of saved movie ratings
        /// </summary>
        /// <returns></returns>
        List<int> GetMovieRatings();
    }
}
