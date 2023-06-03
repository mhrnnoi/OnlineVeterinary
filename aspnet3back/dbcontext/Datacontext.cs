using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet3back.models;
using Microsoft.EntityFrameworkCore;

namespace aspnet3back.dbcontext
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Games> Games { get; set; }
    }
}