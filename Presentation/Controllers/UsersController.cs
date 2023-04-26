using ChatApplication.Application.Users.Commands.Login;
using ChatApplication.Application.Users.Commands.Register;
using ChatApplication.Application.Users.Queris.ConnectedUsers;
using ChatApplication.Application.Users.Queris.UsersWithRooms;
using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using ChatApplication.Infrastructure.Jwt;
using ChatApplication.Presentation.HandleErrorHelper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IErrorHandlerController _errorHandler;
        private IMediator _mediator;
        private readonly IJwtProvider _jwtProvider;

        public UsersController(IMediator mediator,IErrorHandlerController errorHandler,IJwtProvider jwtProvider)
        {
            _errorHandler = errorHandler;
            _mediator = mediator;
            _jwtProvider = jwtProvider;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            RegisterCommand RegisterCmd = new RegisterCommand(userDto.username, userDto.email, userDto.password, userDto.confirmPassword);
            Result<string> result = await _mediator.Send(RegisterCmd);
            return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            LoginCommand loginCommand = new LoginCommand(loginDto.email, loginDto.password);
            Result<string> result = await _mediator.Send(loginCommand);
            return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);
        }


        [HttpGet]
        [Route("ConnectedUsersWithRooms")]
        public async Task<IActionResult> ConnectedUsersWithRooms()
        {
            if (Request.Headers[key: "Authorization"].SingleOrDefault() == null)
            {
                return BadRequest("No Token Provided");
            }
            else
            {
                string[] token = Request.Headers[key: "Authorization"].SingleOrDefault()!.Split(' ');
                Guid id = _jwtProvider.GetIdFromToken(token[1]);
                GetConnectedUsersQuery usersQuery = new GetConnectedUsersQuery(id);
                Result<IList<User>> result = await _mediator.Send(usersQuery);
                return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);
            }
        }

        [HttpGet]
        [Route("ConnectedUnknowUsers")]
        public async Task<IActionResult> ConnectedUnknowUsers()
        {
            if (Request.Headers[key: "Authorization"].SingleOrDefault() == null)
            {
                return BadRequest("No Token Provided");
            }
            else
            {
                string[] token = Request.Headers[key: "Authorization"].SingleOrDefault()!.Split(' ');
                Guid id = _jwtProvider.GetIdFromToken(token[1]);
                GetConnectedUsersQuery usersQuery = new GetConnectedUsersQuery(id);
                Result<IList<User>> result = await _mediator.Send(usersQuery);
                return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);
            }
        }

        [HttpGet]
        [Route("UsersWithRooms")]
        public async Task<IActionResult> UsersWithRooms()
        {
            if (Request.Headers[key: "Authorization"].SingleOrDefault() == null)
            {
                return BadRequest("No Token Provided");
            }
            else
            {
                string[] token = Request.Headers[key: "Authorization"].SingleOrDefault()!.Split(' ');
                Guid id = _jwtProvider.GetIdFromToken(token[1]);
                GetUsersWithRoomsQuery usersQuery = new GetUsersWithRoomsQuery(id);
                Result<IList<ICollection<Room>>> result = await _mediator.Send(usersQuery);
                return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);

            }

        }


    }
}
