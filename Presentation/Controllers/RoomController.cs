using ChatApplication.Application.Rooms.Commands.CreateRoom;
using ChatApplication.Application.Users.Commands.Register;
using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using ChatApplication.Infrastructure.Jwt;
using ChatApplication.Presentation.HandleErrorHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IErrorHandlerController _errorHandler;
        private IMediator _mediator;
        private readonly IJwtProvider _jwtProvider;

        public RoomController(IMediator mediator, IErrorHandlerController errorHandler,IJwtProvider jwtProvider)
        {
            _errorHandler = errorHandler;
            _mediator = mediator;
            _jwtProvider = jwtProvider;
        }


        [HttpPost]
        [Route("CreateRoom")]
        public async Task<IActionResult> CreateRoom(CreateRoomDto roomDto)
        {

            if (Request.Headers[key: "Authorization"].SingleOrDefault() == null)
            {
                return BadRequest("No Token Provided");
            }

            string[] token = Request.Headers[key: "Authorization"].SingleOrDefault()!.Split(' ');
            Guid id = _jwtProvider.GetIdFromToken(token[1]);
            IList<Guid> userIds = new List<Guid>() { roomDto.userId, id };
            CreateRoomCommand createRoomCommand = new CreateRoomCommand(userIds);
            Result<Room> result = await _mediator.Send(createRoomCommand);
            return result.isSuccess ? Ok(result.Value()) : _errorHandler.HandleFailure(result);
        }
    }
}
