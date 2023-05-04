using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Rooms.Queris.GetRooms
{
    public record GetRoomMessagesQuery(Guid roomId) : IRequest<Result<IList<Message>>>
    {
    }
}
