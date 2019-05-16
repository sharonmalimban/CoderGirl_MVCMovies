﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        static IMovieRespository movieRepository = RepositoryFactory.GetMovieRepository();
        static IDirectorRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        public IActionResult Index()
        {
            List<Movie> movies = movieRepository.GetMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Directors = directorRepository.GetDirectors();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
			if (string.IsNullOrWhiteSpace(movie.Name))
			{
				ModelState.AddModelError("Name", "Name must be included");
			}

			if (movie.Year < 1888 || movie.Year > DateTime.Now.Year)
			{
				ModelState.AddModelError("Year", "Year is not valid");				
			}

			if (ModelState.ErrorCount > 0)
			{
				ViewBag.Directors = directorRepository.GetDirectors();
				return View(movie);
			}
            movieRepository.Save(movie);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Movie movie = movieRepository.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(int id, Movie movie)
        {
            //since id is not part of the edit form, it isn't included in the model, thus it needs to be set from the route value
            //there are alternative patterns for doing this - for one, you could include the id in the form but make it hidden
            //feel free to experiment - the tests wont' care as long as you preserve the id correctly in some manner
            movie.Id = id; 
            movieRepository.Update(movie);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            movieRepository.Delete(id);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}