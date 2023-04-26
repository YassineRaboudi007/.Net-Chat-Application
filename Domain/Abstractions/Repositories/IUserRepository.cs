
using ChatApplication.Domain.Entities;

namespace ChatApplication.Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> GetUserByEmail(string email);
        IList<User> GetConnectedUsers(IList<Guid> ids, Guid userId);
        IList<User> GetUsersByIds(ICollection<Guid> ids);
        IList<ICollection<Room>> GetUsersWithRooms(Guid userId);
        IList<User> GetConnectedUsersWithRooms(IList<Guid> ids, Guid userId);


    }
}
