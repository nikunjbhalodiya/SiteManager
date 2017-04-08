using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string HouseNumber { get; set; }
        public string MobileNumber { get; set; }
        public decimal TotalCost { get; set; }
        public string ExtraWork { get; set; }
        public decimal ExtraCost { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
