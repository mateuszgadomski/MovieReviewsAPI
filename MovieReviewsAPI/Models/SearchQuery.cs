using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieReviewsAPI.Models
{
    public class SearchQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}