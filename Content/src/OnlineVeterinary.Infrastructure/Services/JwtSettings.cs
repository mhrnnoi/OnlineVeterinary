using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Infrastructure.Services
{
    public class JwtSettings
    {
       public const string  SectionName = "JwtSettings";
       public string Secret { get; set; } = string.Empty;
       public string Audience { get; set; } = string.Empty;
       public string Issuer { get; set; } = string.Empty;
       public int ExpiryMinutes { get; set; }
       
    }
}