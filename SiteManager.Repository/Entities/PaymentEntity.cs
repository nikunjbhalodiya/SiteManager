using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("DEBIT_CREDIT_ENTITIES")]
    public class PaymentEntity
    {
        [Column("ENTITY_ID")]
        public int EntityId { get; set; }
        [Column("ENTITY_NAME")]
        public string EntityName { get; set; }
    }
}
