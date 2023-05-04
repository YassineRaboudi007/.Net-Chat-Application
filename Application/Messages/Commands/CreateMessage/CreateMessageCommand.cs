using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Messages.Commands.CreateMessage
{
    public record CreateMessageCommand(Guid userId, Guid roomId, string message) : IRequest<Result<bool>>;
}
