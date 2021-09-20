using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Entities;

namespace Backend.Data.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> FindAsync(Guid id);
        Task<Todo> UpdateAsync(Todo todo);
        Task<Todo> AddAsync(Todo todo);
        Task RemoveAsync(Guid id);
        IQueryable<Todo> Get();
    }
}