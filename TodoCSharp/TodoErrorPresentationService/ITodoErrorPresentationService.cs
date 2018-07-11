using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.TodoErrorPresentationService
{
    public interface ITodoErrorPresentationService
    {
        Task Create(TodoError error);
    }
}
