using ChatApplication.Application.Users.Commands.Register;
using FluentValidation;

namespace ChatApplication.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Users).NotEmpty().Must(x => x.Count() >= 2);
        }
    }
}
