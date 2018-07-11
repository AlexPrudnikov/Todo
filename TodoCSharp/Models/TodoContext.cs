using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.Models
{
    public class TodoContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoError> Errors { get; set; }
        public DbSet<TodoStyle> TodoStyles { get; set; } // Проверить будем мы ли это свойство использовать???
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
    }
}
