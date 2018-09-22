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

        public async Task<IEnumerable<Todo>> GetTodosAsync(String id)
        {
            IEnumerable<Todo> todos = null;

            todos = await context.Todos
                .Where(todo => todo.ApplicationUserId == id)
                .ToListAsync();

            return todos;
        }

        public async Task CreateAsync(String id, Todo todo)
        {
            ApplicationUser user = await context.Users.FindAsync(id);
            user?.Todos.Add(todo);

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Int32? id)
        {
            Todo todo = await context.Todos.FindAsync(id);
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
                .Property(e => e.Accomlished)
                .IsModified = true;

            await context.SaveChangesAsync();
        }
    }
}

