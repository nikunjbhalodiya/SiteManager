using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class DeletedCustomerMapper : MapperBase<CustomerEntity, CustomerDeleted>
    {
        public override CustomerDeleted Map(CustomerEntity entity)
        {
            return new CustomerDeleted
            {
                CustomerId = entity.CustomerId,
                CustomerName = entity.CustomerName,
                HouseNumber = entity.HouseNumber,
                MobileNumber = entity.MobileNumber,
                PurchaseDate = entity.PurchaseDate,
                CreatedDate = DateTime.Now,
                ExtraCost = entity.ExtraCost,
                ExtraWork = entity.ExtraWork,
                SiteId = entity.SiteId,
                TotalCost = entity.TotalCost
            };
        }

        public override CustomerEntity Map(CustomerDeleted entity)
        {
            return new CustomerEntity
            {
                CustomerId = entity.CustomerId,
                CustomerName = entity.CustomerName,
                HouseNumber = entity.HouseNumber,
                MobileNumber = entity.MobileNumber,
                PurchaseDate = entity.PurchaseDate,
                CreatedDate = DateTime.Now,
                ExtraCost = entity.ExtraCost,
                ExtraWork = entity.ExtraWork,
                SiteId = entity.SiteId,
                TotalCost = entity.TotalCost
            };
        }
    }
}
