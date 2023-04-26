using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(ICollection<Guid> Users)
        : IRequest<Result<Room>>;

}