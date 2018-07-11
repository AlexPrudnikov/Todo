using System;
using System.Collections.Generic;

namespace TodoCSharp.Models
{
    public class ModelFoJs
    {
        public String NameSort { get; set; }
        public String AccomlishedSort { get; set; }
        public IEnumerable<Todo> Todos { get; set; }
    }
}
