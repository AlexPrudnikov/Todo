using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCSharp.Models
{
    public class RoleModificationModel
    {
        [Required]
        public String RoleName { get; set; }
        public String RoleId { get; set; }
        public String[] IdsToAdd { get; set; }
        public String[] IdsToDelete { get; set; }
    }
}
