using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("DEBIT_CREDIT")]
    public class DebitCreditEntity
    {
        [Key]
        [Column("PAYMENT_ID")]
        public int paymentId { get; set; }

        [Column("ENTITY_ID")]
        public int EntityId { get; set; }

        [Column("ENTITY_TYPE_ID")]
        public int EntityTypeId { get; set; }

        [Column("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [Column("DEBITAMOUNT")]
        public decimal DebitAmount { get; set; }

        [Column("CREDITAMOUNT")]
        public decimal CreditAmount { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        [Column("PAYMENT_DATE")]
        public DateTime PaymentDate { get; set; }

        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [ForeignKey("SITE_ID")]
        public virtual SiteEntity Site { get; set; }
        
        [ForeignKey("ENTITY_TYPE_ID")]
        public virtual PaymentEntity EntityType { get; set; }

        [ForeignKey("PAYMENT_MODE_ID")]
        public virtual PaymentModeEntity PaymentMode { get; set; }
    }
}
