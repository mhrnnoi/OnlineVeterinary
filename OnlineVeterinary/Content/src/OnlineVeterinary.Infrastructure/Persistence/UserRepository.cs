using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Application.Common.Interfaces.Persistence;
using OnlineVeterinary.Domain.Users.Entities;
using OnlineVeterinary.Infrastructure.Persistence.DataContext;

namespace OnlineVeterinary.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<User> _userDbSet;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userDbSet = _context.Set<User>();
        }

        public void Add(User entity)
        {
            _userDbSet.Add(entity);
        }

        public void Remove(User user)
        {
            _userDbSet.Remove(user);
            
        }



        public async Task<List<User>> GetAllAsync()
        {
            return await _userDbSet.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await  _userDbSet.SingleOrDefaultAsync(x => x.Email == email);
          
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userDbSet.SingleOrDefaultAsync(x => x.Id == id);
             
        }


        public void Update(User entity )
        {
            _userDbSet.Update(entity);
        }
    }
}
