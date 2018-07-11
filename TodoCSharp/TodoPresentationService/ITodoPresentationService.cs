using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoPresentationService
{
    public interface ITodoPresentationService
    {
        Task<IEnumerable<Todo>> GetTodos(String id, SortState sortState = SortState.NameAsc);
        Task Create(String id, Todo todo);
        Task Remove(Int32? id);
        Task Edit(Todo todo);
    }
}
