using FluentValidation;

namespace ChatApplication.Application.Users.Commands.Register
{
    public class RegisterCommadnValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommadnValidator()
        {
            RuleFor(x => x.username).NotNull();
            RuleFor(x => x.email).NotNull().EmailAddress();
            RuleFor(x => x.password).NotNull().Equal(x => x.confirmPassword);
            RuleFor(x => x.confirmPassword).NotNull();
        }
    }
}
