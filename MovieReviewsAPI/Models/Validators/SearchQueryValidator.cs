using FluentValidation;
using MovieReviewsAPI.Entities;
using System.Linq;

namespace MovieReviewsAPI.Models.Validators
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        private int[] allowedPageSizes = new[] { 10, 20, 30 };

        public SearchQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}