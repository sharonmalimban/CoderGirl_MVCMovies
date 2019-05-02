using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public interface IMovieRespository
    {
        int Save(Movie movie);

        List<Movie> GetMovies();

        Movie GetById(int id);

        void Update(Movie movie);

        void Delete(int id);
    }
}
