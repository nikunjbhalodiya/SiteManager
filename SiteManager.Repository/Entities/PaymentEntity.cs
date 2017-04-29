using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("DEBIT_CREDIT_ENTITIES")]
    public class PaymentEntity
    {
        public PaymentEntity()
        {
            DebitCreditInfo = new List<DebitCreditEntity>();
        }
        [Key]
        [Column("ENTITY_TYPE_ID")]
        public int EntityTypeId { get; set; }
        [Column("ENTITY_TYPE_NAME")]
        public string EntityTypeName { get; set; }

        public virtual ICollection<DebitCreditEntity> DebitCreditInfo { get; set; }
    }
}
