using Microsoft.AspNetCore.Mvc;
using todo_list_server.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace todo_list_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: 
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
