using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.ErrorDao;
using TodoCSharp.Models;

namespace TodoCSharp.TodoErrorPresentationService
{
    public class TodoErrorPresentationService : ITodoErrorPresentationService
    {
        private readonly ITodoErrorDao todoErrorDao;
        public TodoErrorPresentationService(ITodoErrorDao todoErrorDao)
        {
            this.todoErrorDao = todoErrorDao;
        }
        public async Task Create(TodoError todoError)
        {
            todoError.Time = DateTime.Now;
            await todoErrorDao.Create(todoError);
        }
    }
}
