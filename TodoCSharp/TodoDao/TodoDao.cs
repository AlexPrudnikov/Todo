using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoDao
{
    public class TodoDao : ITodoDao
    {
        private readonly TodoContext db;

        // Конструктор
        public TodoDao(TodoContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Тестовый метод
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Todo>> GetTodosAsync(String id)
        {
            IEnumerable<Todo> todos = null;
            using (var db = this.db)
            {
                // Проблема в закоментированом коде, т.к. не успевает прийти ответ с сервера в JavaScript
                //await db.Todos.LoadAsync();
                //todos = (await db.Users.FindAsync(id)).Todos.ToList();

                // Разобратся с методом FindAsync() в класс DbContext
                //todos = await db.FindAsync(

                //todos = (await db.Users.LoadAsync())

                // Так работает
                todos = (await db.Todos.ToListAsync()).Where(todo => todo.ApplicationUserId == id);
            }

            return todos;
        }

        public async Task CreateAsync(String id, Todo todo)
        {
            using (var db = this.db)
            {
                ApplicationUser user = await db.Users.FindAsync(id);
                user.Todos.Add(todo);
                await db.SaveChangesAsync();
            }

            //var s = new ServiceProvider();
            //using (var dbContext = new TodoContext())
            //{
            //    ApplicationUser user = await dbContext.Users.FindAsync(id);
            //    user.Todos.Add(todo);
            //    await dbContext.SaveChangesAsync();

            //}
        }

        public async Task RemoveAsync(Int32? id)
        {
            using (var db = this.db)
            {
                Todo todo = await db.Todos.FirstOrDefaultAsync(t => t.TodoId == id);
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
            }       
        }

        public async Task EditAsync(Todo todo)
        {
            using (var db = this.db)
            {
                db.Entry(todo).Property(e => e.Name).IsModified = true;
                db.Entry(todo).Property(e => e.Accomlished).IsModified = true;
                
                await db.SaveChangesAsync();
            }
        }
    }
}
