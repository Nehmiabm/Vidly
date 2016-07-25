using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly2.Models;

namespace Vidly2.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _movieContext;

        public MovieController()
        {
            _movieContext=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
           _movieContext.Dispose();
        }

        // GET: Movie
        public ActionResult Index()
        {
            return View(_movieContext.Movie.Include(m => m.Genre).ToList());
        }

        public ActionResult Details(int id)
        {
            var movie = _movieContext.Movie.Include(m =>m.Genre).SingleOrDefault(m => m.MovieId == id);
            if (movie == null)
                return HttpNotFound();
            else
                return View(movie);
        }

        public ActionResult New()
        {
            var genresDb = _movieContext.Genres.ToList();
            var movieViewModel = new MovieFormViewModel()
            {
              Genres = genresDb
            };
            return View("MovieForm", movieViewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.MovieId == 0)
                _movieContext.Movie.Add(movie);
            else
            {
                //update
                var movieInDb = _movieContext.Movie.Single(m => m.MovieId == movie.MovieId);
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
            }
            _movieContext.SaveChanges();
            return RedirectToAction("Index","Movie");
        }

        public ActionResult Edit(int id)
        {
            var movieInDb = _movieContext.Movie.SingleOrDefault(m => m.MovieId == id);
            if (movieInDb == null)
                return HttpNotFound();
            var genresInDb = _movieContext.Genres;
            var movieViewModel = new MovieFormViewModel()
            {
              Genres = genresInDb,
              Movie = movieInDb
            };
            return View("MovieForm", movieViewModel);
        }
    }
}