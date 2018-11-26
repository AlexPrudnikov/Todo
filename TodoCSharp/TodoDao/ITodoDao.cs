﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoDao
{
    public interface ITodoDao
    {
        Task<IEnumerable<Todo>> GetTodosAsync(String id);
        Task CreateAsync(String id, Todo todo);
        Task RemoveAsync(Int32? id);
        Task RemoveAllAsync(String id);
        Task EditAsync(Todo todo);
    }
}