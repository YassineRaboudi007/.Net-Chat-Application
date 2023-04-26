using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Abstractions.UnitOfWork;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using ChatApplication.Infrastructure.Repositories;
using ChatApplication.Infrastructure.UntiOfWork;
using MediatR;

namespace ChatApplication.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<Room>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(IRoomRepository roomRepository, IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            ICollection<User> users = _userRepository.GetUsersByIds(request.Users);
            Room newRoom = Room.CreateRoom(users);

            using (_unitOfWork)
            {
                _roomRepository.Add(newRoom);
                await _unitOfWork.Complete();
            }

            return newRoom;
        }
    }
}
