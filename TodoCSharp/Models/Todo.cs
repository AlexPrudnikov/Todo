using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.Models
{
    public class Todo
    {
        [FromHeader(Name = "_id")]
        public Int32 TodoId { get; set; }
        public String Name { get; set; }
        public Boolean Accomlished { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
