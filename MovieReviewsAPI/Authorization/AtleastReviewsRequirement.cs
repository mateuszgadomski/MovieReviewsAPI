using Microsoft.AspNetCore.Authorization;

namespace MovieReviewsAPI.Authorization
{
    public class AtleastReviewsRequirement : IAuthorizationRequirement
    {
        public int MinimumReviews { get; }

        public AtleastReviewsRequirement(int minimumReviews)
        {
            MinimumReviews = minimumReviews;
        }
    }
}