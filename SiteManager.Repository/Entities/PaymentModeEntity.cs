using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("PAYMENT_MODE")]
    public class PaymentModeEntity
    {
        [Column("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }
        [Column("PAYMENT_MODE_NAME")]
        public string PaymentModeName { get; set; }
    }
}
