using FluentValidation;

namespace ChatApplication.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidtor : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidtor()
        {
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.roomId).NotEmpty();
            RuleFor(x => x.message).NotEmpty();
        }
    }
}
