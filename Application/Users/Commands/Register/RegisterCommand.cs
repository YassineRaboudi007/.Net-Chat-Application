using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Commands.Register
{
    public record RegisterCommand(string username, string email, string password, string confirmPassword) : IRequest<Result<string>>;
}
