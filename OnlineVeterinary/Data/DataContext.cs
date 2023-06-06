using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineVeterinary.Models;

namespace OnlineVeterinary.Data
{
    public class DataContext :  IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
    
        
    
}