using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Models;
using MovieReviewsAPI.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll()
        {
            var movies = _movieService.GetAll();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get([FromRoute] int id)
        {
            var movie = _movieService.Get(id);

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateMovieDto dto)
        {
            var id = _movieService.Create(dto);

            return Created($"/api/movies/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _movieService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateMovieDto dto, [FromRoute] int id)
        {
            _movieService.Update(dto, id);

            return Ok();
        }
    }
}