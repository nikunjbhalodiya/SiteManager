using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("LABOUR_PAYMENT")]
    public class LabourEntity
    {
        [Key]
        [Column("LABOUR_PAYMENT_ID")]
        public int LabourId { get; set; }
        [Column("CONTRACTOR_ID")]
        public int? ContractorId { get; set; }
        [Column("WORK_TYPE_ID")]
        public int WorkTypeId { get; set; }


        [Column("WORK_DETAIL")]
        public string WorkDetail { get; set; }

        [Column("PAYMENT")]
        public decimal Payment { get; set; }

        [Column("WORK_DATE")]
        public DateTime PaymentDate { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("SITE_ID")]
        public virtual SiteEntity Site { get; set; }

        [ForeignKey("CONTRACTOR_ID")]
        public virtual ContractorEntity Contractor { get; set; }

        [ForeignKey("WORK_TYPE_ID")]
        public virtual WorkTypeEntity WorkType { get; set; }
    }
}
