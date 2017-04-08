
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Labour
    {
        public int LabourId { get; set; }

        public Dictionary<int, string> Contractors { get; set; }

        public Dictionary<int, string> WorkTypes { get; set; }

        public KeyValuePair<int, string> Contractor { get; set; }

        public KeyValuePair<int, string> WorkType { get; set; }

        public string WorkDetail { get; set; }

        public decimal Payment { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Remark { get; set; }

        public DateTime EntryDate { get; set; }

    }
}
