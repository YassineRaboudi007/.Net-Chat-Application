using ChatApplication.Domain.Abstractions.Repositories;
using ChatApplication.Domain.Entities;

namespace ChatApplication.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

    }
}
