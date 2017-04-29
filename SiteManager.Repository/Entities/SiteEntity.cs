using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("SITE")]
    public class SiteEntity
    {
        public SiteEntity()
        {
            Supervisors = new List<SupervisorEntity>();
            Customers = new List<CustomerEntity>();
            Materials = new List<MaterialEntity>();
            LaboursPayment = new List<LabourEntity>();
            DebitCreditInfo = new List<DebitCreditEntity>();
        }

        [Key]
        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [Column("SITE_NAME")]
        public string SiteName { get; set; }

        [Column("SITE_ADDRESS")]
        public string Address { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }

        public virtual ICollection<SupervisorEntity> Supervisors { get; set; }

        public virtual ICollection<MaterialEntity> Materials { get; set; }

        public virtual ICollection<LabourEntity> LaboursPayment { get; set; }

        public virtual ICollection<DebitCreditEntity> DebitCreditInfo { get; set; }
    }
}
