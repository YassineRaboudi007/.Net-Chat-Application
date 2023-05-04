using ChatApplication.Application.Users.Queris.ConnectedUsers;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Queris
{
    public class GetAllConnectedUsersQueryHandler : IRequestHandler<GetAllConnectedUsersQuery, Result<IList<User>>>
    {
        public IList<Guid> _connectedUsers;
        private readonly IUserRepository _userRepository;

        public GetAllConnectedUsersQueryHandler(IList<Guid> connectedUsers ,IUserRepository userRepository)
        {
            _connectedUsers = connectedUsers;
            _userRepository = userRepository;
        }

        public async Task<Result<IList<User>>> Handle(GetAllConnectedUsersQuery request, CancellationToken cancellationToken)
        {
            IList<Guid> _connectedUsersIds = _connectedUsers;
            List<User> connectedUsers = await _userRepository.GetConnectedUsersWithRooms(_connectedUsersIds,request.id);
            Result<IList<User>> res = new Result<IList<User>>(connectedUsers, true, Error.None);
            return res;
        }
    }
}
