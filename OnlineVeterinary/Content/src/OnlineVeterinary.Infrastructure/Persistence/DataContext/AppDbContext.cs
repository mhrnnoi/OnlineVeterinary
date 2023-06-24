using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OnlineVeterinary.Application.Common.Interfaces;
using OnlineVeterinary.Domain.Pet.Entities;
using OnlineVeterinary.Domain.Reservation.Entities;
using OnlineVeterinary.Domain.Users.Entities;
using OnlineVeterinary.Infrastructure.Services;

namespace OnlineVeterinary.Infrastructure.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        // private readonly DbSettings _dbSettings;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (options.IsConfigured == false)
            {
                options.UseNpgsql("Server=127.0.0.1; Port =5432; User Id = postgres; password = Mehran123; database = OnlineVeterinary");
            }

        }


    }
}
