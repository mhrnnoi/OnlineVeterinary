using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Infrastructure.Services
{
    public class DbSettings
    {
       public const string  SectionName = "ConnectionStrings";
       public string DefaultConnection { get; set; } = string.Empty;
     
    }
}