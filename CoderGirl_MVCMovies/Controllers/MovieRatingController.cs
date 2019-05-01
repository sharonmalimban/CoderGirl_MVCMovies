using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieRatingController : Controller
    {
        private IMovieRatingRepository repository = RepositoryFactory.GetMovieRatingRepository();

        private string htmlForm = @"
            <form method='post'>
                <input name='movieName' />
                <select name='rating'>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>                    
                </select>
                <button type='submit'>Rate it</button>
            </form>";

       public IActionResult Index()
        {
            List<int> ids = repository.GetIds();
            var movieRatings = ids.Select(id => repository.GetMovieNameById(id))
                                                              .Distinct()
                                                              .Select(name => new KeyValuePair<string, decimal>( name, repository.GetAverageRatingByMovieName(name)))
                                                              .ToList();
            ViewBag.MovieRatings = movieRatings;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = MovieController.movies;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string movieName, string rating)
        {
            repository.SaveRating(movieName, Convert.ToInt32(rating));
            return RedirectToAction(actionName: nameof(Details), routeValues: new { movieName, rating });
        }

        [HttpGet]
        public IActionResult Details(string movieName, string rating)
        {
            ViewBag.Movie = movieName;
            ViewBag.Rating = rating;
            return View();
        }
    }
}