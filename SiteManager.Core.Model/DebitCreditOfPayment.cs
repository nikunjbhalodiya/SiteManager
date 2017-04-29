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

        /// <summary>
        /// Amount Remain to pay
        /// </summary>
        public decimal DebitAmount { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        public decimal CreditAmount { get; set; }

        public PaymentMode SelectedMode { get; set; }

        public int SiteId { get; set; }

        public string Remark { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
