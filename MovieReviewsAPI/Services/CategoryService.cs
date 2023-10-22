using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using MovieReviewsAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();

        CategoryDto GetById(int id);

        int Create(CreateAndUpdateCategoryDto dto);

        public void Delete(int id);

        void Update(CreateAndUpdateCategoryDto dto, int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(MovieReviewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _dbContext
                .Categories
                .ToList();

            var categorieDtos = _mapper.Map<List<CategoryDto>>(categories);

            return categorieDtos;
        }

        public CategoryDto GetById(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                CategoryNotFoundException();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public int Create(CreateAndUpdateCategoryDto dto)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Name == dto.Name);

            if (category != null)
                throw new BadRequestException("This category is now available");

            var newCategory = _mapper.Map<Category>(dto);
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            return newCategory.Id;
        }

        public void Delete(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                CategoryNotFoundException();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }

        public void Update(CreateAndUpdateCategoryDto dto, int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                CategoryNotFoundException();

            category.Name = dto.Name;
            category.Description = dto.Description;
            _dbContext.SaveChanges();
        }

        private void CategoryNotFoundException()
        {
            throw new NotFoundException("Category not found");
        }
    }
}