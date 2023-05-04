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

        public Task<List<User>> GetConnectedUsers(IList<Guid> ids, Guid userId)
        {
            IQueryable<User> allConnectedUsers = _context.Users.Where(user => ids.Contains(user.Id).Equals(true));

            return allConnectedUsers.Where(user => user.Id != userId).ToListAsync();    
        }

        public Task<User?> GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<List<User>> GetUsersByIds(ICollection<Guid> ids)
        {
            return _context.Users.Where(user => ids.Contains(user.Id).Equals(true)).ToListAsync();

        }

        public Task<List<User>> GetUsersWithRooms(Guid userId)
        {
            var rooms = _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Rooms)
                .ThenInclude(room => room.Users.All(user => user.Id != userId));
            return rooms.ToListAsync();
        }

        public Task<List<User>> GetConnectedUsersWithRooms(IList<Guid> ids, Guid userId)
        {
            return _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Rooms)
                .ThenInclude(room => room.Users.Where(user => ids.Contains(user.Id).Equals(true)))
                .ToListAsync();
        }

        public Task<List<User>> GetConnectedUsersWithoutRoom(IList<Guid> ids, Guid userId)
        {
            User? currentUser = _context.Users.Where(user => user.Id == userId).FirstOrDefault();

            if (currentUser == null)
            {
                return Task.FromResult(new List<User>());
            }
            
            return _context.Users
                .Where(user => ids.Contains(user.Id).Equals(true))
                .Include(user => user.Rooms)
                .Where(user => user.Rooms.All(room => room.Users.Contains(currentUser).Equals(true)))
                .ToListAsync();
        }
    }
}
