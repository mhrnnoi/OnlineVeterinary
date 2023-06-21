using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Infrastructure.Services;

namespace OnlineVeterinary.Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        private readonly DbSettings _dbSettings;

        public DbSet<CareGiver> CareGivers { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
       
       public AppDbContext(IOptions<DbSettings> dbSettings)
       {
        _dbSettings = dbSettings.Value;
       }
        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DbSettings> dbSettings) : base(options)
        {
            _dbSettings = dbSettings.Value;
        }

        protected override void OnConfiguring (DbContextOptionsBuilder options)
        {
            
            if (options.IsConfigured == false)
            {
                options.UseNpgsql(_dbSettings.DefaultConnection);
            }
           
        }
      

    }
}
