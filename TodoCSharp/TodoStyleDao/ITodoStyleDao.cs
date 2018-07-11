using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoStyleDao
{
    public interface ITodoStyleDao
    {
        Task<IEnumerable<TodoStyle>> GetTodoStylesAsync();
    }
}
