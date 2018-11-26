using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.ViewModels;

namespace TodoCSharp.TodoPresentationService
{
    public interface ITodoPresentationService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos(String id, SortState sortState = SortState.NameAsc);
        Task Create(String id, Todo todo);
        Task Remove(Int32? id);
        Task RemoveAll(String id);
        Task Edit(Todo todo);
    }
}
