using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("LABOUR_WORK_TYPE")]
    public class WorkTypeEntity
    {
        public WorkTypeEntity()
        {
            Labours = new List<LabourEntity>();
        }

        [Key]
        [Column("WORK_TYPE_ID")]
        public int WorkTypeId { get; set; }

        [Column("WORK_TYPE_NAME")]
        public string WorkTypeName { get; set; }

        public virtual ICollection<LabourEntity> Labours { get; set; }
    }
}
