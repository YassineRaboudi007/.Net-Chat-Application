using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ChatApplication.Infrastructure.Repositories
{
    public class UserRepostiroy : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepostiroy(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public IList<User> GetConnectedUsers(IList<Guid> ids, Guid userId)
        {
            IQueryable<User> allConnectedUsers = _context.Users.Where(user => ids.Contains(user.Id).Equals(true));

            return allConnectedUsers.Where(user => user.Id != userId).ToList();    
        }

        public Task<User?> GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public IList<User> GetUsersByIds(ICollection<Guid> ids)
        {
            return _context.Users.Where(user => ids.Contains(user.Id).Equals(true)).ToList();

        }

        public IList<ICollection<Room>> GetUsersWithRooms(Guid userId)
        {
            var rooms = _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Rooms)
                .ThenInclude(room => room.Users.Where(user => user.Id != userId))
                .Include(user => user.Rooms)
                .ThenInclude(room => room.Messages)
                .Select(user => user.Rooms);
            return rooms.ToList();
        }

        public IList<User> GetConnectedUsersWithRooms(IList<Guid> ids, Guid userId)
        {
            return _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Rooms)
                .ThenInclude(room => room.Users.Where(user => ids.Contains(user.Id).Equals(true)))
                .ToList();
        }

    }
}
