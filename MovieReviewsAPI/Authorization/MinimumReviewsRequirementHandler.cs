using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MovieReviewsAPI.Authorization
{
    public class MinimumReviewsRequirementHandler : AuthorizationHandler<AtleastReviewsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtleastReviewsRequirement requirement)
        {
            var reviewsCount = int.Parse(context.User.FindFirst(c => c.Type == "ReviewsCount").Value);

            if (reviewsCount >= requirement.MinimumReviews)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}