using System.ComponentModel.DataAnnotations;

namespace Rex3.Models
{
    public class Country
    {
        [Key]
        public string CountryCode { get; set; }
        public string  Name { get; set; }
    }
}
