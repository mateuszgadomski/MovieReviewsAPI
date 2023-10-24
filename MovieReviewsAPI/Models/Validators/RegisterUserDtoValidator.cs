using FluentValidation;
using MovieReviewsAPI.Entities;
using System.Linq;

namespace MovieReviewsAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(MovieReviewsDbContext dbContext)
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("Email address is required")
                .Custom((value, context) =>
                {
                    var loginInUse = dbContext.Users.Any(u => u.Login == value);
                    if (loginInUse)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }
                });

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required")
                     .Custom((value, context) =>
                     {
                         var emailInUse = dbContext.Users.Any(u => u.Email == value);
                         if (emailInUse)
                         {
                             context.AddFailure("Email", "That email is taken");
                         }
                     });

            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
        }
    }
}