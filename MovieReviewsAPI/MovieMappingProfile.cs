using AutoMapper;
using MovieReviewsAPI.Entities;
using MovieReviewsAPI.Models;

namespace MovieReviewsAPI
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(m => m.Category, c => c.MapFrom(s => s.Category.Name));

            CreateMap<Review, ReviewDto>()
                .ForMember(r => r.Movie, m => m.MapFrom(s => s.Movie.Title));
            CreateMap<CreateAndUpdateMovieDto, Movie>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateAndUpdateCategoryDto, Category>();
            CreateMap<CreateReviewDto, Review>()
                .ForMember(r => r.MovieId, m => m.MapFrom(s => s.MovieId));
        }
    }
}