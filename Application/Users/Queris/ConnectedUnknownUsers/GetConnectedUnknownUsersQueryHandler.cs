using ChatApplication.Application.Users.Queris.ConnectedUsers;
using ChatApplication.Application.Users.Queris.GetConnectedUnknownUsers;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris.GetConnectedUnknownUsers
{
    public class GetConnectedUnknownUsersQueryHandler : IRequestHandler<GetConnectedUnknownUsersQuery, Result<IList<User>>>
    {
        public IList<Guid> _connectedUsers;
        private readonly IUserRepository _userRepository;

        public GetConnectedUnknownUsersQueryHandler(IList<Guid> connectedUsers ,IUserRepository userRepository)
        {
            _connectedUsers = connectedUsers;
            _userRepository = userRepository;
        }

        public async Task<Result<IList<User>>> Handle(GetConnectedUnknownUsersQuery request, CancellationToken cancellationToken)
        {
            IList<Guid> _connectedUsersIds = _connectedUsers;
            IList<User> connectedUsers = await _userRepository.GetConnectedUsersWithoutRoom(_connectedUsersIds,request.id);
            Result<IList<User>> res = new Result<IList<User>>(connectedUsers, true, Error.None);
            return res;
        }
    }
}
