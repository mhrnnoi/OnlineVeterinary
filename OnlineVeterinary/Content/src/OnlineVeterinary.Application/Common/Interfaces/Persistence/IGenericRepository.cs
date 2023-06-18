using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Application.Common.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}