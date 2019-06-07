using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoCSharp.Models
{
    public class Todo
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]  Отключает автогенерирование первичного ключа
        [FromHeader(Name = "_id")]
        public Int32 TodoId { get; set; }
        public String Name { get; set; }
        public Boolean Done { get; set; }
        public Boolean Public { get; set; } = false;
        public DateTime Time { get; set; }
        public String ApplicationUserId { get; set; }
        public IEnumerable<Like> Likes { get; set; } = new List<Like>();
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
