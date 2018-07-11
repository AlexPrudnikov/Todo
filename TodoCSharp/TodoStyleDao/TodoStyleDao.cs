using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoStyleDao
{
    public class TodoStyleDao : ITodoStyleDao
    {
        private readonly TodoContext db;
        // Конструктор
        public TodoStyleDao(TodoContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<TodoStyle>> GetTodoStylesAsync()
        {
            IEnumerable<TodoStyle> todoStyles = null;
            using (var db = this.db)
            {
                todoStyles = await db.TodoStyles.ToListAsync();
            }

            return todoStyles;
        }
    }
}
