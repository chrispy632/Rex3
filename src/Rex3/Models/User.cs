using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Models
{

    public class User
    {
        [Key]
        [Display(Name = "S3 User Id")]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string email { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }
        [Display(Name = "Creded by")]
        [Required]
        public string CreatedId { get; set; }
        [Display(Name = "Creded On")]
        public DateTime? CreatedDt { get; set; }
        public string UpdatedId { get; set; }
        public DateTime? UpdatedDt { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
