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

            CreateMap<Review, ReviewDto>();
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}