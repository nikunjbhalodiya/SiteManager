using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("CUSTOMER")]
    public class CustomerEntity
    {
        [Key]
        [Column("CUSTOMER_ID")]
        public int CustomerId { get; set; }

        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; }

        [Column("HOUSE_NUMBER")]
        public string HouseNumber { get; set; }

        [Column("MOBILE_NUMBER")]
        public string MobileNumber { get; set; }

        [Column("HOUSE_COST")]
        public decimal TotalCost { get; set; }

        [Column("EXTRA_WORK")]
        public string ExtraWork { get; set; }

        [Column("EXTRA_COST")]
        public decimal ExtraCost { get; set; }

        [Column("PURCHASE_DATE")]
        public DateTime PurchaseDate { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [ForeignKey("SITE_ID")]
        public virtual SiteEntity Site { get; set; }
    }
}
