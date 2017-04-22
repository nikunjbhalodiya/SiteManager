using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("QUANTITY_UNIT")]
    public class UnitEntity
    {
        public UnitEntity()
        {
            Materials = new List<MaterialEntity>();
        }

        [Key]
        [Column("UNIT_ID")]
        public int UnitId { get; set; }

        [Column("UNIT_NAME")]
        public string UnitName { get; set; }
        
        public virtual ICollection<MaterialEntity> Materials { get; set; }
    }
}
