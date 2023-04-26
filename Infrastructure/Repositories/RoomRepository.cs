using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;

namespace ChatApplication.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
    
    }
}
