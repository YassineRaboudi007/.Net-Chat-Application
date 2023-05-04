using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.ConnectedUsers
{
    public record GetAllConnectedUsersQuery(Guid id) : IRequest<Result<IList<User>>>;
}
