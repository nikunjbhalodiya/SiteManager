using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Entities
{
    [Table("MaterialType")]
    public class MaterialTypeEntity
    {
        public MaterialTypeEntity()
        {
            Materials = new List<MaterialEntity>();
        }

        [Key]
        [Column("Material_Id")]
        public int MaterialTypeId { get; set; }

        [Column("MATERIAL_NAME")]
        public string MaterialTypeName { get; set; }

        public virtual ICollection<MaterialEntity> Materials { get; set; }
    }
}
