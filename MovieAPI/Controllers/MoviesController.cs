using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;

            if (!_context.Movies.Any())
            {
                _context.Movies.Add(
                    new Movie
                    {
                        id = "1",
                        Title = "Blade Runner 2049",
                        ReleaseDate = DateTime.Parse("2017-10-5"),
                        Length = "2h 44min",
                        Description = "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.",
                        Director = new Director
                        {
                            Id = "1",
                            Name = " Denis Villeneuve"
                        },
                        Rating = 87,
                        Cast = new List<Actor>
                        {
                            new Actor
                            {
                                Id = "1",
                                Name = "Ryan Gosling"
                            },

                            new Actor
                            {
                                Id = "2",
                                Name = "Harrison Ford"
                            }
                        }
                    }
                );

                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets all Movies from the database
        /// </summary>
        /// <returns>All Movies in Database</returns>
        /// <response code="201">Returns all movies from the database</response>
        /// <response code="400">If no movies where retrieved</response>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        /// <summary>
        /// Gets movies with entered name
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /GetMovieByTitle
        ///     {
        ///        "title": "Blade Runner 2049"
        ///     }
        ///
        /// </remarks>
        /// <param name="searchString"></param>
        /// <returns>Movie with entered name</returns>
        /// <response code="201">Returns Movie with entered name</response>
        /// <response code="400">If no movie was retrieved</response>
        [HttpGet("{searchString}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByTitle(string searchString)
        {
            var movies = await _context.Movies.Where(t => t.Title.ToUpper().Contains(searchString.ToUpper())).ToListAsync();

            if (movies.Count.Equals(0))
            {
                return NotFound();
            }
            else if (searchString == null)
            {
                movies = await _context.Movies.ToListAsync();
            }

            return movies;
        }
    }
}
