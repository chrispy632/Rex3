using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Models
{
    public class UserRole
    {
        public string UserId { get; set; }

        public int RoleId { get; set; }
        public string CreatedId { get; set; }
        public DateTime CreatedDt { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
