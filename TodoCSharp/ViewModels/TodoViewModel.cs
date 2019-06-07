using System;
using System.Collections.Generic;
using TodoCSharp.Models;

namespace TodoCSharp.ViewModels
{
    public class TodoViewModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Boolean Done { get; set; }
        public Boolean Public { get; set; }
        public DateTime Time { get; set; }
        public String ApplicationUserId { get; set; }
        public IEnumerable<Like> Likes { get; set; }
    }
}
