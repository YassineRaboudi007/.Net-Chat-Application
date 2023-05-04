using FluentValidation;

namespace ChatApplication.Application.Users.Commands.Login
{
    public class CreateMessageCommandValidtor : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidtor()
        {
            RuleFor(x => x.email).NotEmpty();
            RuleFor(x => x.email).EmailAddress();
            RuleFor(x => x.password).NotEmpty();
        }
    }
}
