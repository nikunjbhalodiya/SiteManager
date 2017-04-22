using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("CONTRACTOR")]
    public class ContractorEntity
    {
        public ContractorEntity()
        {
            LaboursPayment = new List<LabourEntity>();
        }
        [Key]
        [Column("CONTRACTOR_ID")]
        public int ContractorId { get; set; }
        [Column("CONTRACTOR_NAME")]
        public string ContractorName { get; set; }
        [Column("DESCRIPTION")]
        public string Detail { get; set; }
        [Column("CREATED_DATE")]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<LabourEntity> LaboursPayment { get; set; }
    }
}
