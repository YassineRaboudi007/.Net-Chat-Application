using ChatApplication.Application.Users.Queris.UsersWithRooms;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Rooms.Queris.GetRooms
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomsQuery, Result<IList<Room>>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Result<IList<Room>>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            IList<Room> rooms = await _roomRepository.GetAll();
            Result<IList<Room>> res = new Result<IList<Room>>(rooms, true, Error.None);
            return res;
        }
    }
}
