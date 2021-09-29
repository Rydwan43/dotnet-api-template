using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Core.Interfaces;
using Backend.Core.Models.Helpers;
using Backend.Core.Models.DTOs.Todo;
using Backend.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Core.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepository _repository;
        private IUserProfileService _userProfileService;

        public TodoService(ITodoRepository repository, 
        IUserProfileService userProfileService)
        {
            _repository = repository;
            _userProfileService = userProfileService;
        }
        public async Task<TodoModel> CreateTodoAsync(TodoModel todo)
        {
            var user = await _userProfileService.GetProfileInfo();

            if (todo is null || user is null)
            {
                throw new AccessViolationException(nameof(todo));
            }

            var todoEntity = new Data.Entities.Todo
            {
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                UserId = user.Id
            };

            todoEntity = await _repository.AddAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Name = todoEntity.Name,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            var todo = await _repository.FindAsync(todoId);
            var user = await _userProfileService.GetProfileInfo();
            
            if (todo.UserId == user.Id)
            {
                await _repository.RemoveAsync(todoId);    
            }
            
        }

        public async Task<PaginationModel<TodoModel>> GetPaginationTodosAsync(int page, int pageSize)
        {
            var user = await _userProfileService.GetProfileInfo();

            IQueryable<Data.Entities.Todo> query = _repository.Get();
            query = query.Where(x => x.UserId == user.Id);
            var todos = await query.Select(todo => new TodoModel
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            })
            .ToListAsync();
            return new PaginationModel<TodoModel>(page, pageSize, todos.AsQueryable());
        }

        public async Task<TodoModel> GetTodoAsync(Guid todoId)
        {
            var user = await _userProfileService.GetProfileInfo();
            
            var todo = await _repository.FindAsync(todoId);
            
            if (todo is null || todo.UserId != user.Id)
            {
                return null;
            }

            return new TodoModel
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            var user = await _userProfileService.GetProfileInfo();

            IQueryable<Data.Entities.Todo> query = _repository.Get();
            query = query.Where(x => x.UserId == user.Id);
            return await query.Select(todo => new TodoModel
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            })
            .ToListAsync();
        }

        public async Task<TodoModel> UpdateTodoAsync(TodoModel todo)
        {
            var user = await _userProfileService.GetProfileInfo();

            var todoEntity = new Data.Entities.Todo
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                UserId = user.Id
            };

            todoEntity = await _repository.UpdateAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Name = todoEntity.Name,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }
    }
}