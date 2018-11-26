using System;
using System.Collections.Generic;
using TodoCSharp.ViewModels;

namespace TodoCSharp.Models
{
    public class ModelFoJs
    {
        public String NameSort { get; set; }
        public String AccomlishedSort { get; set; }
        public IEnumerable<TodoViewModel> Todos { get; set; }
    }
}
