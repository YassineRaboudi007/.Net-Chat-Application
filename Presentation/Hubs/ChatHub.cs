using ChatApplication.Application.Messages.Commands.CreateMessage;
using ChatApplication.Application.Users.Commands.Register;
using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Domain.Entities;
using ChatApplication.Domain.Shared;
using ChatApplication.Infrastructure.Jwt;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApplication.Application.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public IList<Guid> _connectedUsers;
        private readonly IJwtProvider _jwtProvider;

        public ChatHub(IMediator mediator, IList<Guid> connectedUsers,IJwtProvider jwtProvider)
        {
            _mediator = mediator;
            _connectedUsers = connectedUsers;
            _jwtProvider = jwtProvider;
        }
        public override Task OnConnectedAsync()
        {
            string token = Context.GetHttpContext().Request.Query["token"];
            Guid id = _jwtProvider.GetIdFromToken(token);
            _connectedUsers.Add(id);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string token = Context.GetHttpContext().Request.Query["token"];
            Guid id = _jwtProvider.GetIdFromToken(token);
            _connectedUsers.Remove(id);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageCommandDTO commandDTO)
        {
            CreateMessageCommand RegisterCmd = new CreateMessageCommand(commandDTO.userId, commandDTO.roomId, commandDTO.message);
            Result<bool> result = await _mediator.Send(RegisterCmd);
            if (result.isSuccess)
            {
                await Clients
                .GroupExcept(commandDTO.roomId.ToString(),Context.ConnectionId)
                .SendAsync("ReceiveMessage", commandDTO.userId, commandDTO.message);
            }
            else
            {
                await Clients
                .Client(commandDTO.userId.ToString())
                .SendAsync("Failed To Send Message",commandDTO.message);
            }
        }
    }
}
