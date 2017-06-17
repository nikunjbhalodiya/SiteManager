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
                MaterialTypeId = model.SelectedMaterialType.MaterialTypeId,
                VendorId = model.SelectedVendor.VendorId,
                UnitId = model.SelectedUnit.UnitId,
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
                SelectedMaterialType = new MaterialType { MaterialTypeId = entity.MaterialType.MaterialTypeId, MaterialTypeName = entity.MaterialType.MaterialTypeName },
                SelectedVendor = entity.Vendor != null ? new VendorMapper().Map(entity.Vendor) : new Vendor(),
                SelectedUnit = new QuantityUnitType { UnitId = entity.Unit.UnitId,  UnitName = entity.Unit.UnitName },
                CreatedDate = entity.CreatedDate,
                Quantity = entity.Quantity,
                Remark = entity.Remark,
                MaterialDetailId = entity.MaterialId
            };
        }
    }
}
