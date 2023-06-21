using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVeterinary.Application.Common.Interfaces.Services;

namespace OnlineVeterinary.Infrastructure.Services
{
    public class DatetimeProvider : IDateTimeProvider
    {
        public DateTime Utc => DateTime.UtcNow;
    }
}