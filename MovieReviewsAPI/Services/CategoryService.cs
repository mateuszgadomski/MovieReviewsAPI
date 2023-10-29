using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using MovieReviewsAPI.Middleware;
using MovieReviewsAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MovieReviewsAPI.Services
{
    public interface ICategoryService
    {
        PagedResult<CategoryDto> GetAll([FromQuery] SearchQuery query);

        CategoryDto GetById(int id);

        int Create(CreateAndUpdateCategoryDto dto);

        public void Delete(int id);

        void Update(CreateAndUpdateCategoryDto dto, int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CategoryService(MovieReviewsDbContext dbContext, IMapper mapper, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public PagedResult<CategoryDto> GetAll([FromQuery] SearchQuery query)
        {
            var baseQuery = _dbContext
                .Categories
                .Where(c => query.SearchPhrase == null || c.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                || c.Description.ToLower().Contains(query.SearchPhrase.ToLower()));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Category, object>>>
                {
                    {nameof(Category.Name), c => c.Name },
                    {nameof(Category.Description), c => c.Description },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var categories = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var categorieDtos = _mapper.Map<List<CategoryDto>>(categories);

            var result = new PagedResult<CategoryDto>(categorieDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
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
            newCategory.CreatedById = _userContextService.GetUserId;
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