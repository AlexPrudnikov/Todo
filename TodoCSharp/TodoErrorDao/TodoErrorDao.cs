using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.ErrorDao;
using TodoCSharp.Models;

namespace TodoCSharp.TodoErrorDao
{
    public class TodoErrorDao : ITodoErrorDao
    {
        private readonly TodoContext context;
        public TodoErrorDao(TodoContext context)
        {
            this.context = context;
        }

        public async Task Create(TodoError todoError)
        {
            using (var db = this.context)
            {
                await db.Errors.AddAsync(todoError);
            }
        }
    }
}
