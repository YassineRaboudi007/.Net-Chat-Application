using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IList<Message>> GetRoomMessages(Guid roomId)
        {
            Room? room = await _context.Rooms
                .Include(room => room.Messages)
                .SingleOrDefaultAsync(room => room.Id == roomId);
                
            if (room == null)
            {
                return new List<Message>();
            }

            return room.Messages;
        }

        
    }
}
