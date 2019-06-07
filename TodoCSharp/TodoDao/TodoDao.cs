using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoDao
{
    public class TodoDao : ITodoDao
    {
        private readonly TodoContext context;
        public TodoDao(TodoContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Todo>> GetUserTodosAsync(String id)
        {
            IEnumerable<Todo> todos = null;

            todos = await context.Todos
                .Where(todo => todo.ApplicationUserId == id)
                .Include(x => x.Likes)
                .ToListAsync();

            return todos;
        }

        public async Task<Todo> GetTodoAsync(Int32 id)
        {
            Todo todo = await context.Todos
                .Where(x => x.TodoId == id)
                .Include(x => x.Likes)
                .FirstOrDefaultAsync();

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync() =>
            await context.Todos.ToListAsync();

        public async Task<Int32> CreateAsync(String id, Todo todo)
        {
            ApplicationUser user = await context.Users.FindAsync(id);
            user?.Todos.Add(todo);

            await context.SaveChangesAsync();

            return todo.TodoId;                    
        }

        public async Task RemoveAsync(Int32? id)
        {
            Todo todo = await context.Todos
                .Where(t => t.TodoId == id)
                .Include(x => x.Likes)
                .FirstOrDefaultAsync();

            context.Todos.Remove(todo);

            await context.SaveChangesAsync();
        }

        public async Task RemoveAllAsync(String id)
        {
            IEnumerable<Todo> todos = await context.Todos
                .Where(todo => todo.ApplicationUserId == id)
                .ToListAsync();

            context.Todos.RemoveRange(todos);
        }

        public async Task EditAsync(Todo todo)
        {
            context.Entry(todo)
                   .Property(e => e.Name)
                   .IsModified = true;

            context.Entry(todo)
                   .Property(e => e.Done)
                   .IsModified = true;

            context.Entry(todo)
                   .Collection(e => e.Likes)
                   .IsModified = true;

            await context.SaveChangesAsync();
        }
    }
}

