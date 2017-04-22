using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class VendorMapper : MapperBase<Vendor, VendorEntity>
    {
        public override VendorEntity Map(Vendor model)
        {
            return new VendorEntity
            {
                VendorId = model.VendorId,
                CreatedDate = model.CreatedDate,
                Address = model.Address,
                Remark = model.Remark,
                VendorName = model.VendorName,
            };
        }

        public override Vendor Map(VendorEntity entity)
        {
            return new Vendor
            {
                CreatedDate = entity.CreatedDate,
                Address = entity.Address,
                Remark = entity.Remark,
                VendorName = entity.VendorName,
                VendorId = entity.VendorId
            };
        }
    }
}
