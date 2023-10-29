using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Authorization;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using MovieReviewsAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MovieReviewsAPI.Services
{
    public interface IReviewService
    {
        PagedResult<ReviewDto> GetAll([FromQuery] SearchQuery query);

        ReviewDto GetById(int id);

        int Create(CreateReviewDto dto);

        void Delete(int id);

        void Update(UpdateReviewDto dto, int id);
    }

    public class ReviewService : IReviewService
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ReviewService(MovieReviewsDbContext context, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = context;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public PagedResult<ReviewDto> GetAll([FromQuery] SearchQuery query)
        {
            var baseQuery = _dbContext
                .Reviews
                .Include(r => r.Movie)
                 .Where(r => query.SearchPhrase == null || r.IsWorth.ToString().ToLower().Contains(query.SearchPhrase.ToLower())
                || r.CreatedBy.Login.ToLower().Contains(query.SearchPhrase.ToLower()));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Review, object>>>
                {
                    {nameof(Review.IsWorth), r => r.IsWorth },
                    {nameof(Review.CreatedBy.Login), r => r.CreatedBy.Login },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var reviews = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);

            var result = new PagedResult<ReviewDto>(reviewDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public ReviewDto GetById(int id)
        {
            var review = _dbContext
                .Reviews
                .Include(r => r.Movie)
                .FirstOrDefault(r => r.Id == id);

            var reviewDto = _mapper.Map<ReviewDto>(review);

            return reviewDto;
        }

        public int Create(CreateReviewDto dto)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == dto.MovieId);

            if (movie is null)
                throw new NotFoundException("Movie not found");

            var newReview = _mapper.Map<Review>(dto);
            newReview.MovieId = movie.Id;
            newReview.CreatedById = _userContextService.GetUserId;
            _dbContext.Reviews.Add(newReview);
            _dbContext.SaveChanges();

            return newReview.Id;
        }

        public void Delete(int id)
        {
            var review = _dbContext
                .Reviews
                .FirstOrDefault(r => r.Id == id);

            if (review is null)
                ReviewNotFoundException();

            if (!_userContextService.IsAdministrator)
            {
                var authorizationResult = _authorizationService.AuthorizeAsync
                    (_userContextService.User, review, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

                if (!authorizationResult.Succeeded)
                    throw new ForbidException();
            }

            _dbContext.Reviews.Remove(review);
            _dbContext.SaveChanges();
        }

        public void Update(UpdateReviewDto dto, int id)
        {
            var review = _dbContext
                .Reviews
                .FirstOrDefault(r => r.Id == id);

            if (review is null)
                ReviewNotFoundException();

            if (!_userContextService.IsAdministrator)
            {
                var authorizationResult = _authorizationService.AuthorizeAsync
                    (_userContextService.User, review, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

                if (!authorizationResult.Succeeded)
                {
                    throw new ForbidException();
                }
            }

            review.Content = dto.Content;
            review.IsWorth = dto.IsWorth;
            review.UpdatedDate = DateTime.Now.ToString("dd/MM/yyy");
            _dbContext.SaveChanges();
        }

        private void ReviewNotFoundException()
        {
            throw new NotFoundException("Review not found");
        }
    }
}