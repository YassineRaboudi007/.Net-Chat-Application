using ChatApplication.Application.Users.Queris.ConnectedUsers;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris
{
    public class GetConnectedUsersQueryHandler : IRequestHandler<GetConnectedUsersQuery, Result<IList<User>>>
    {
        public IList<Guid> _connectedUsers;
        private readonly IUserRepository _userRepository;

        public GetConnectedUsersQueryHandler(IList<Guid> connectedUsers ,IUserRepository userRepository)
        {
            _connectedUsers = connectedUsers;
            _userRepository = userRepository;
        }

        public async Task<Result<IList<User>>> Handle(GetConnectedUsersQuery request, CancellationToken cancellationToken)
        {
            IList<Guid> _connectedUsersIds = _connectedUsers;
            IList<User> connectedUsers = _userRepository.GetConnectedUsersWithRooms(_connectedUsersIds,request.id);
            Result<IList<User>> res = new Result<IList<User>>(connectedUsers, true, Error.None);
            return res;
        }
    }
}
