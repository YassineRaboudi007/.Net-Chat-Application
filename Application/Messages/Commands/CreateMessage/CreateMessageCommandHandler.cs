using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using ChatApplication.Infrastructure.Repositories;
using MediatR;

namespace ChatApplication.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<bool>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository, IRoomRepository roomRepository, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _messageRepository = messageRepository;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Result<bool>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Room? room = _roomRepository.Find(room => room.Id == request.roomId).FirstOrDefault();
            User? user = _userRepository.Find(user => user.Id == request.userId).FirstOrDefault();


            if (user == null)
            {
                Error err = new Error("400", "User Not Found");
                return Result.Failure<bool>(default, err);
            }

            if (room == null)
            {
                Error err = new Error("400", "User Not Found");
                return Result.Failure<bool>(default, err);
            }

            Message msg = Message.Create(request.message, room, user);
            bool res = await _messageRepository.Add(msg);
            return res;

        }
    }
}
