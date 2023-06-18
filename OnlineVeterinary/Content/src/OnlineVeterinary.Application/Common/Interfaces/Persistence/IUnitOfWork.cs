using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task DisposeAsync();
    }
}