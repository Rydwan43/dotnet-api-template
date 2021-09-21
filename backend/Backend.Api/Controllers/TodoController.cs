using System;
using System.Threading.Tasks;
using Backend.Core.Interfaces;
using Backend.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("{id}")]
        [ActionName("GetTodoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoModel>> GetTodoAsync(Guid id)
        {
            var todo = await _todoService.GetTodoAsync(id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoModel>> GetTodosAsync()
        {
            var todos = await _todoService.GetTodosAsync();
            return Ok(todos);
        }
    }
}