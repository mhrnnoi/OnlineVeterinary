using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}
