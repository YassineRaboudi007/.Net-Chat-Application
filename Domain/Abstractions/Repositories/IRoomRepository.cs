using ChatApplication.Domain.Entities;

namespace ChatApplication.Domain.Abstractions.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IList<Message>> GetRoomMessages(Guid roomId);
    }
}
