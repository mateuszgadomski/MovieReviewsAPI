namespace MovieReviewsAPI.Models
{
    public class UpdateMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
    }
}