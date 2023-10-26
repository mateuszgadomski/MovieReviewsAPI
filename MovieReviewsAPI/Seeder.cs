using Microsoft.AspNetCore.Identity;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewsAPI
{
    public class Seeder
    {
        private readonly MovieReviewsDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Seeder(MovieReviewsDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.AddRange(roles);

                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Movies.Any() && !_dbContext.Categories.Any())
                {
                    var movies = GetMovies();
                    _dbContext.Movies.AddRange(movies);

                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);

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

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },

                new Role()
                {
                    Name = "Administrator"
                }
            };

            return roles;
        }

        private IEnumerable<User> GetUsers()
        {
            var userRole = _dbContext.Roles.FirstOrDefault(r => r.Id == 1);
            var adminRole = _dbContext.Roles.FirstOrDefault(r => r.Id == 2);

            if (userRole is null || adminRole is null)
                throw new NotFoundException("Roles not available");

            var users = new List<User>()
            {
                new User()
                {
                    Login = "Admin",
                    Email = "Admin@gmail.com",
                    Nationality = "Poland",
                    DateOfBirth = new DateTime(1994,01,01),
                    ReviewCount = 20,
                    Role = adminRole
                },

                new User()
                {
                    Login = "User",
                    Email = "User@gmail.com",
                    Nationality = "Poland",
                    DateOfBirth = new DateTime(1994,01,01),
                    Role = userRole
                }
            };

            foreach (var user in users)
            {
                var hashedPassword = _passwordHasher.HashPassword(user, "tNk1xZmLZ82=");
                user.PasswordHash = hashedPassword;
            }

            return users;
        }
    }
}