using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class MaterialMapper : MapperBase<Material, MaterialEntity>
    {
        public override MaterialEntity Map(Material model)
        {
            return new MaterialEntity
            {
                MaterialId = model.MaterialDetailId,
                SiteId = model.SiteId,
                BillAmount = model.BillAmount,
                BillDate = model.BillDate,
                BillNumber = model.BillNumber,
                MaterialTypeId = model.SelectedMaterialType.Key,
                VendorId = model.SelectedVendor.VendorId,
                UnitId = model.SelectedUnit.Key,
                CreatedDate = model.CreatedDate,
                Quantity = model.Quantity,
                Remark = model.Remark
            };
        }

        public override Material Map(MaterialEntity entity)
        {
            return new Material
            {
                SiteId = entity.SiteId,
                BillAmount = entity.BillAmount,
                BillDate = entity.BillDate,
                BillNumber = entity.BillNumber,
                SelectedMaterialType = new KeyValuePair<int, string>(entity.MaterialType.MaterialTypeId, entity.MaterialType.MaterialTypeName),
                SelectedVendor = new VendorMapper().Map(entity.Vendor),
                SelectedUnit = new KeyValuePair<int, string>(entity.Unit.UnitId,entity.Unit.UnitName),
                CreatedDate = entity.CreatedDate,
                Quantity = entity.Quantity,
                Remark = entity.Remark,
                MaterialDetailId = entity.MaterialId
            };
        }
    }
}
