using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Rooms.Queris.GetRooms
{
    public record GetRoomsQuery() : IRequest<Result<IList<Room>>>
    {
    }
}
