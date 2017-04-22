using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class DebitCreditOfPayment
    {
        public DebitCreditOfPayment()
        {
            PaymentDate = DateTime.Today;
        }
        public int paymentId { get; set; }

        public EntityType EntityType { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public PaymentMode SelectedMode { get; set; }

        public string Remark { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
