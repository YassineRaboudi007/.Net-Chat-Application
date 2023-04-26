using AutoMapper;
using MediatR;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Abstractions.UnitOfWork;
using ChatApplication.Domain.Shared;
using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Infrastructure.Jwt;

namespace ChatApplication.Application.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByEmail(request.email);

            if (user != null)
            {
                Error err = new Error("400", "Email Already Taken");
                return Result.Failure<string>(default, err);
            }

            User createdUser = User.Create(request.username,request.email,request.password);

            using (_unitOfWork)
            {
                _userRepository.Add(createdUser);
                await _unitOfWork.Complete(); 
            }

            string token = _jwtProvider.Generate(createdUser);

            return token;
        }
    }
}
