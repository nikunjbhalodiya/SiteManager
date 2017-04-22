using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Contractor
    {
        public int ContractorId { get; set; }

        public string ContractorName { get; set; }

        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
