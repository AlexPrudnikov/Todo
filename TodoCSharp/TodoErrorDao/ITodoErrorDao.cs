using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.ErrorDao
{
    public interface ITodoErrorDao
    {
        Task Create(TodoError todoError);
    }
}
