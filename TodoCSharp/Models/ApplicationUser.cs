using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Int32 Age { get; set; }
        public Byte[] ImageData { get; set; }
        public String ImageMimeType { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }

        public ApplicationUser() { Todos = new List<Todo>(); }
    }
}
