using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<PagedResult<MovieDto>> GetAll([FromQuery] SearchQuery query)
        {
            var movies = _movieService.GetAll(query);

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetById([FromRoute] int id)
        {
            var movie = _movieService.GetById(id);

            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Policy = "AtleastReviews")]
        public ActionResult Create([FromBody] CreateAndUpdateMovieDto dto)
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
        public ActionResult Update([FromBody] CreateAndUpdateMovieDto dto, [FromRoute] int id)
        {
            _movieService.Update(dto, id);

            return Ok();
        }
    }
}