using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using MediatR;

namespace ChatApplication.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByEmail(request.email);

            if (user == null)
            {
                Error err = new Error("400", "User Not Found");
                return Result.Failure<string>(default,err);
            }

            if(request.password != user.Password)
            {
                Error err = new Error("400", "Password is wrong");
                return Result.Failure<string>(default, err);
            }

            string token = _jwtProvider.Generate(user);

            return token;

        }
    }
}
