using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class SiteModel
    {
        public int SiteId { get; set; } 
        public string SiteName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
