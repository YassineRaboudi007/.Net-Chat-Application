using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Commands.Login
{
    public record CreateMessageCommand(string email,string password) : IRequest<Result<string>>;
}
