using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;

namespace Vidly2.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View(GetMovies());
        }

        private IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>()
            {
               new Movie {Name = "Shrek"},
               new Movie {Name="Wall-e" }
            };
            return movies;
        }
    }
}