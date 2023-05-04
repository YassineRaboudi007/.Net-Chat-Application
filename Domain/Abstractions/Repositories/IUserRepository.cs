
using ChatApplication.Domain.Entities;

namespace ChatApplication.Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User?> GetUserByEmail(string email);
        Task<List<User>> GetConnectedUsers(IList<Guid> ids, Guid userId);
        Task<List<User>> GetUsersByIds(ICollection<Guid> ids);
        Task<List<User>> GetUsersWithRooms(Guid userId);
        Task<List<User>> GetConnectedUsersWithRooms(IList<Guid> ids, Guid userId);
        Task<List<User>> GetConnectedUsersWithoutRoom(IList<Guid> ids, Guid userId);

    }
}
