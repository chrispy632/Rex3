﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "Priority Order")]
        public int PriorityOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
