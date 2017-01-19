using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rex3.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountNameAka { get; set; }
        public string HooversDescription { get; set; }
        public string LeadRegionId { get; set; }
        public int? Rank { get; set; }
        public string sfdcStitchingId { get; set; }
    }
}
