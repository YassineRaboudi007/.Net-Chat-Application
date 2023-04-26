using ChatApplication.Domain.Abstractions.Auth;
using ChatApplication.Infrastructure.Jwt;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApplication.Application.Hubs
{
    public class ChatHub : Hub
    {
        public IList<Guid> _connectedUsers;
        private readonly IJwtProvider _jwtProvider;

        public ChatHub(IList<Guid> connectedUsers,IJwtProvider jwtProvider)
        {
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

        public async Task SendMessage(string userId,string room,string message)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", userId, message);
        }


    }
}
