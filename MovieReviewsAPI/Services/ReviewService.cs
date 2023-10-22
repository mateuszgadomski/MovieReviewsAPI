using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using MovieReviewsAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI.Services
{
    public interface IReviewService
    {
        IEnumerable<ReviewDto> GetAll();

        ReviewDto GetById(int id);

        int Create(CreateReviewDto dto);

        void Delete(int id);

        void Update(UpdateReviewDto dto, int id);
    }

    public class ReviewService : IReviewService
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReviewService(MovieReviewsDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public IEnumerable<ReviewDto> GetAll()
        {
            var reviews = _dbContext
                .Reviews
                .Include(r => r.Movie)
                .ToList();

            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);

            return reviewDtos;
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