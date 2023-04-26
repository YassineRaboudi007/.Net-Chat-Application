using FluentValidation;

namespace ChatApplication.Application.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.email).NotEmpty();
            RuleFor(x => x.email).EmailAddress();
            RuleFor(x => x.password).NotEmpty();
        }
    }
}
