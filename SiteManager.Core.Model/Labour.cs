
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Labour
    {
        public Labour()
        {
            PaymentDate = DateTime.Today;
        }
        public int LabourId { get; set; }

        public Contractor Contractor { get; set; }

        public WorkType WorkType { get; set; }

        public string WorkDetail { get; set; }

        public decimal Payment { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
        public int SiteId { get; set; }
    }
}
