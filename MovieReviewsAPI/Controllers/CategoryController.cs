using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Models;
using MovieReviewsAPI.Services;
using System.Collections.Generic;

namespace MovieReviewsAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<PagedResult<CategoryDto>> GetAll([FromQuery] SearchQuery query)
        {
            var categories = _categoryService.GetAll(query);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<CategoryDto> GetById([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);

            return Ok(category);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateAndUpdateCategoryDto dto)
        {
            var id = _categoryService.Create(dto);

            return Created($"/api/categories/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _categoryService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateAndUpdateCategoryDto dto, [FromRoute] int id)
        {
            _categoryService.Update(dto, id);

            return Ok();
        }
    }
}