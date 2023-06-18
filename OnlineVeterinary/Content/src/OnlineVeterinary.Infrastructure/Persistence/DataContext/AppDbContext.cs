using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineVeterinary.Domain.CareGivers.Entities;
using OnlineVeterinary.Domain.Doctor.Entities;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.ReservedTime.Entities;

namespace OnlineVeterinary.Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {

        public DbSet<CareGiver> CareGivers { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<ReservedTime> ReservedTimes { get; set; } = null!;
       
       public AppDbContext()
       {
        
       }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring (DbContextOptionsBuilder options)
        {
            
            if (options.IsConfigured == false)
            {
                options.UseNpgsql("Server=127.0.0.1; Port =5432; User Id = postgres; password = Mehran123; database = OnlineVeterinary");
            }
           
        }
      

    }
}
