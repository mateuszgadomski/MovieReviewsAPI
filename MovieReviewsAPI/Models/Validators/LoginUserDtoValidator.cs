using FluentValidation;
using MovieReviewsAPI.Entities;

namespace MovieReviewsAPI.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password address is required");
        }
    }
}