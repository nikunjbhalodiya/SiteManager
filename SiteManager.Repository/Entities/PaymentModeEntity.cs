using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("PAYMENT_MODE")]
    public class PaymentModeEntity
    {
        public PaymentModeEntity()
        {
            DebitCreditInfo = new List<DebitCreditEntity>();        
        }

        [Key]
        [Column("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }
        [Column("PAYMENT_MODE_NAME")]
        public string PaymentModeName { get; set; }

        public virtual ICollection<DebitCreditEntity> DebitCreditInfo { get; set; }
    }
}
