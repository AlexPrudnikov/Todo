using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.ViewModels;

namespace TodoCSharp.TodoPresentationService
{
    public interface ITodoPresentationService
    {
        Task<IEnumerable<TodoViewModel>> GetUserTodos(String id, SortState sortState = SortState.NameAsc);
        Task<IEnumerable<TodoViewModel>> GetPublicTodos();
        Task<Int32> GetTodo(String userId, Int32 todoId);
        Task<Int32> Create(String id, Todo todo);
        Task Remove(Int32? id);
        Task RemoveAll(String id);
        Task Edit(Todo todo);
    }
}
