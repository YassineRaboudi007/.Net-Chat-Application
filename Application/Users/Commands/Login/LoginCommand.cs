using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Commands.Login
{
    public record LoginCommand(string email,string password) : IRequest<Result<string>>;
}
