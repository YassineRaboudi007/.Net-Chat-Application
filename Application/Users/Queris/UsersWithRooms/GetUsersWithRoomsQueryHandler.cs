using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.UsersWithRooms
{
    public class GetUsersWithRoomsQueryHandler : IRequestHandler<GetUsersWithRoomsQuery, Result<IList<ICollection<Room>>>>
    {
        public IList<Guid> _connectedUsers;
        private readonly IUserRepository _userRepository;

        public GetUsersWithRoomsQueryHandler(IList<Guid> connectedUsers, IUserRepository userRepository)
        {
            _connectedUsers = connectedUsers;
            _userRepository = userRepository;
        }

        public async Task<Result<IList<ICollection<Room>>>> Handle(GetUsersWithRoomsQuery request, CancellationToken cancellationToken)
        {
            IList<ICollection<Room>> users = _userRepository.GetUsersWithRooms(request.id);
            Result<IList<ICollection<Room>>> res = new Result<IList<ICollection<Room>>>(users, true,Error.None);
            return res;
        }
    }
}
