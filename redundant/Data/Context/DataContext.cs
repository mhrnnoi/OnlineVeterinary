using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Data.Entity;
using OnlineVeterinary.Models;
using OnlineVeterinary.Models.Identity;

namespace OnlineVeterinary.Data
{
    public class DataContext :  IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<CareGiverPet> CareGiverPet { get; set; }
        public DbSet<ReservedTimes> ReservedTimes { get; set; }
        
    }
    
        
    
}