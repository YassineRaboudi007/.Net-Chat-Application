using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.UsersWithRooms
{
    public record GetUsersWithRoomsQuery(Guid id) : IRequest<Result<IList<User>>>
    {
    }
}
