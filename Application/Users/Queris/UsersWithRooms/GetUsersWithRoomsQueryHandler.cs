using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.UsersWithRooms
{
    public class GetRoomMessagesQueryHandler : IRequestHandler<GetUsersWithRoomsQuery, Result<IList<User>>>
    {
        public IList<Guid> _connectedUsers;
        private readonly IUserRepository _userRepository;

        public GetRoomMessagesQueryHandler(IList<Guid> connectedUsers, IUserRepository userRepository)
        {
            _connectedUsers = connectedUsers;
            _userRepository = userRepository;
        }

        public async Task<Result<IList<User>>> Handle(GetUsersWithRoomsQuery request, CancellationToken cancellationToken)
        {
            IList<User> users = await _userRepository.GetUsersWithRooms(request.id);
            Result<IList<User>> res = new Result<IList<User>>(users, true,Error.None);
            return res;
        }
    }
}
