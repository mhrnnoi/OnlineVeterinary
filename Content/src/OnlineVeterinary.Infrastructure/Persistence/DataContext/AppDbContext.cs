using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Domain.Users.Entities;

namespace OnlineVeterinary.Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options )
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (options.IsConfigured == false)
            {
                options.UseNpgsql("Server=database; Port =5432; User Id = postgres; password = postgres; database = postgres");
            }

        }


    }
}
