using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }

        public string SupervisorName { get; set; }

        public decimal MonthlySalary { get; set; }

        public string DutyDescription { get; set; }

        public DateTime CreatedDate { get; set; }

        public int SiteId { get; set; }
    }
}
