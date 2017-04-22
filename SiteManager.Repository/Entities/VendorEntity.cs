using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("VENDOR")]
    public class VendorEntity
    {
        public VendorEntity()
        {
            Materials = new List<MaterialEntity>();
        }

        [Key]
        [Column("VENDOR_ID")]
        public int VendorId { get; set; }
        [Column("VENDOR_NAME")]
        public string VendorName { get; set; }
        [Column("ADDRESS")]
        public string Address { get; set; }
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        public virtual ICollection<MaterialEntity> Materials { get; set; }
    }
}
