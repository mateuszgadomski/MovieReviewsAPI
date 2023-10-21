using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI.Controllers
{
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieController(MovieReviewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll()
        {
            var movies = _dbContext
                .Movies
                .Include(m => m.Category)
                .Include(m => m.Reviews)
                .ToList();

            var movieDtos = _mapper.Map<List<MovieDto>>(movies);

            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get([FromRoute] int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie is null)
                return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateMovieDto dto)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Name == dto.CategoryName);

            if (category is null)
                return NotFound();

            var movie = _mapper.Map<Movie>(dto);
            movie.Category = category;
            _dbContext.Movies.Add(movie);

            _dbContext.SaveChanges();

            return Created($"/api/movies/{movie.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie is null)
                return NotFound();

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}