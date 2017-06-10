using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("MATERIAL_PURCHASE_DETAIL")]
    public class MaterialEntity
    {
        [Key]
        [Column("PURCHASE_ID")]
        public int MaterialId { get; set; }

        [Column("VENDOR_ID")]
        public int? VendorId { get; set; }
        
        [Column("MATERIAL_TYPE_ID")]
        public int MaterialTypeId { get; set; }

        [Column("BILL_NUMBER")]
        public string BillNumber { get; set; }

        [Column("BILL_DATE")]
        public DateTime BillDate { get; set; }

        [Column("QUANTITY")]
        public decimal Quantity { get; set; }

        [Column("UNIT_ID")]
        public int UnitId { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        [Column("BILL_AMOUNT")]
        public decimal BillAmount { get; set; }

        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("SITE_ID")]
        public virtual SiteEntity Site { get; set; }

        [ForeignKey("Material_Id")]
        public virtual MaterialTypeEntity MaterialType { get; set; }

        [ForeignKey("VENDOR_ID")]
        public virtual VendorEntity Vendor { get; set; }

        [ForeignKey("UNIT_ID")]
        public virtual UnitEntity Unit { get; set; }
    }
}
