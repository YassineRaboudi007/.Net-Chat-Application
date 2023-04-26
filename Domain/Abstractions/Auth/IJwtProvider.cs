using ChatApplication.Domain.Entities;

namespace ChatApplication.Domain.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string Generate(User user);
        Guid GetIdFromToken(string token);

    }
}
