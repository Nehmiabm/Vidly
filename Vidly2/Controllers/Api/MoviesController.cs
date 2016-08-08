using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = ApplicationDbContext.Create();
        }
        //GET: api/movies
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDto> movieDtos = _context.Movie.Include(m =>m.Genre).ToList().Select(Mapper.Map<Movie,MovieDto>);
            return Ok(movieDtos);
        }
        //GET: api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.MovieId == id);
            if(movie==null)
             return NotFound();
            var movieDto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }
        //POST: api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movie.Add(movie);
            _context.SaveChanges();
            movieDto.MovieId = movie.MovieId;
            return Created(Request.RequestUri + "/" + movie.MovieId, movieDto);
        }
        //PUT: api/movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movieInDb = _context.Movie.SingleOrDefault(m => m.MovieId == id);
            if (movieInDb == null)
                return NotFound();
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();

        }
        //DELETE: api/movies/id
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.MovieId == id);
            if (movie == null)
               return NotFound();
            _context.Movie.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
