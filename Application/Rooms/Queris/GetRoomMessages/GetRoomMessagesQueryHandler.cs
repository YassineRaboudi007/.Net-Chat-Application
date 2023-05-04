using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Rooms.Queris.GetRooms
{
    public class GetRoomMessagesQueryHandler : IRequestHandler<GetRoomMessagesQuery, Result<IList<Message>>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomMessagesQueryHandler(IList<Guid> connectedUsers, IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Result<IList<Message>>> Handle(GetRoomMessagesQuery request, CancellationToken cancellationToken)
        {
            IList<Message> users = await _roomRepository.GetRoomMessages(request.roomId);
            Result<IList<Message>> res = new Result<IList<Message>>(users, true,Error.None);
            return res;
        }
    }
}
