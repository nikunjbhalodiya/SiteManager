using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("SUPERVISOR")]
    public class SupervisorEntity
    {
        [Key]
        [Column("SUPERVISOR_ID")]
        public int SupervisorId { get; set; }

        [Column("SUPERVISOR_NAME")]
        public string SupervisorName { get; set; }

        [Column("DESCRIPTION")]
        public string DutyDescription { get; set; }

        [Column("Salary")]
        public decimal Salary { get; set; }

        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Column("SITE_ID")]
        public int SiteId { get; set; }

        [ForeignKey("SITE_ID")]
        public virtual SiteEntity Site { get; set; }
    }
}
