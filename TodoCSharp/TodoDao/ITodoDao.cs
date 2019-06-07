using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoDao
{
    public interface ITodoDao
    {
        Task<IEnumerable<Todo>> GetUserTodosAsync(String id);
        Task<IEnumerable<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoAsync(Int32 id);
        Task<Int32> CreateAsync(String id, Todo todo);
        Task RemoveAsync(Int32? id);
        Task RemoveAllAsync(String id);
        Task EditAsync(Todo todo);
    }
}
