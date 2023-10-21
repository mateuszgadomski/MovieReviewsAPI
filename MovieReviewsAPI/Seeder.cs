using MovieReviewsAPI.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI
{
    public class Seeder
    {
        private readonly MovieReviewsDbContext _dbContext;

        public Seeder(MovieReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Movies.Any() && !_dbContext.Categories.Any())
                {
                    var movies = GetMovies();
                    _dbContext.Movies.AddRange(movies);

                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Movie> GetMovies()
        {
            var dramaCategory = new Category()
            {
                Name = "Drama",
                Description = "In film and television, drama is a category or genre of narrative fiction (or semi-fiction) " +
                "intended to be more serious than humorous in tone."
            };

            var reviews = new List<Review>()
            {
                new Review()
                {
                    Content = "A great movie worth seeing!",
                    IsWorth = true,
                },

                new Review()
                {
                    Content = "I don't know if I liked this movie myself...",
                    IsWorth = false,
                },
            };

            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "The Godfather",
                    Description = "A story about a New York Mafia family. The aging Don Corleone wants to pass power to his son.",
                    Author = "Francis Ford Coppola",
                    Category = dramaCategory,
                    Reviews = reviews
                },

                new Movie()
                {
                    Title = "Forrest Gump",
                    Description = "The life story of Forrest, a low IQ boy with limb paresis who becomes a billionaire and a hero of the Vietnam War.",
                    Author = "Robert Zemeckis",
                    Category = dramaCategory
                }
            };

            return movies;
        }
    }
}