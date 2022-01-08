using FluentValidation;
using MovieApp.Data.Dtos;

namespace MovieApp.Business.FluentValidators
{
    public class LoginValidator: AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
