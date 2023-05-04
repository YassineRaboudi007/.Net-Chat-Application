using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.GetConnectedUnknownUsers
{
    public record GetConnectedUnknownUsersQuery(Guid id) : IRequest<Result<IList<User>>>;
}
