using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Models;
using MovieReviewsAPI.Services;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace MovieReviewsAPI.Controllers
{
    [Route("api/review")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> GetAll()
        {
            var reviews = _reviewService.GetAll();

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public ActionResult<ReviewDto> GetById([FromRoute] int id)
        {
            var review = _reviewService.GetById(id);

            return Ok(review);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateReviewDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int id = _reviewService.Create(dto);

            return Created($"/api/reviews/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _reviewService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateReviewDto dto, [FromRoute] int id)
        {
            _reviewService.Update(dto, id);

            return NoContent();
        }
    }
}