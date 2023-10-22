using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using MovieReviewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieDto> GetAll();

        MovieDto GetById(int id);

        int Create(CreateMovieDto dto);

        void Delete(int id);

        void Update(UpdateMovieDto dto, int id);
    }

    public class MovieService : IMovieService
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieService(MovieReviewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<MovieDto> GetAll()
        {
            var movies = _dbContext
                .Movies
                .Include(m => m.Category)
                .Include(m => m.Reviews)
                .ToList();

            var movieDtos = _mapper.Map<List<MovieDto>>(movies);

            return movieDtos;
        }

        public MovieDto GetById(int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie is null)
                MovieNotFoundException();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        public int Create(CreateMovieDto dto)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Name == dto.CategoryName);

            if (category is null)
                CategoryNotFoundException();

            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Title == dto.Title);

            if (movie != null)
                throw new BadRequestException("This video is now available");

            var newMovie = _mapper.Map<Movie>(dto);
            newMovie.Category = category;
            _dbContext.Movies.Add(newMovie);
            _dbContext.SaveChanges();

            return newMovie.Id;
        }

        public void Delete(int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie is null)
                MovieNotFoundException();

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }

        public void Update(UpdateMovieDto dto, int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie is null)
                MovieNotFoundException();

            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Name == dto.CategoryName);

            if (category is null)
                CategoryNotFoundException();

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Author = dto.Author;
            movie.Category = category;

            _dbContext.SaveChanges();
        }

        private void MovieNotFoundException()
        {
            throw new NotFoundException("Movie not found");
        }

        private void CategoryNotFoundException()
        {
            throw new NotFoundException("Category not found");
        }
    }
}