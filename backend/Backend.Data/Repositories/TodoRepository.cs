using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Entities;
using Backend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DatabaseContext _context;

        public TodoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            _context.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> FindAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public IQueryable<Todo> Get()
        {
            return _context.Todos.AsQueryable();
        }

        public async Task RemoveAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is not null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            var local = _context.Todos.FirstOrDefault(entity => entity.Id == todo.Id);
            if (local is not null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}