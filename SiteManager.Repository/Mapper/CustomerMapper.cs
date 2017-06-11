using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class CustomerMapper : MapperBase<Customer, CustomerEntity>
    {
        public override CustomerEntity Map(Customer model)
        {
            return new CustomerEntity
            {
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                HouseNumber = model.HouseNumber,
                ExtraCost = model.ExtraCost,
                ExtraWork = model.ExtraWork,
                MobileNumber = model.MobileNumber,
                CreatedDate = model.CreatedDate,
                TotalCost = model.TotalCost,
                SiteId = model.SiteId,
                PurchaseDate = model.PurchaseDate
            };
        }

        public override Customer Map(CustomerEntity entity)
        {
            return new Customer
            {
                CustomerId = entity.CustomerId,
                CustomerName = entity.CustomerName,
                HouseNumber = entity.HouseNumber,
                ExtraCost = entity.ExtraCost,
                ExtraWork = entity.ExtraWork,
                MobileNumber = entity.MobileNumber,
                CreatedDate = entity.CreatedDate,
                TotalCost = entity.TotalCost,
                SiteId = entity.SiteId,
                PurchaseDate = entity.PurchaseDate
            };
        }

        public void Map(Customer model, CustomerEntity entity)
        {
            entity.CustomerName = model.CustomerName;
            entity.ExtraCost = model.ExtraCost;
            entity.ExtraWork = model.ExtraWork;
            entity.MobileNumber = model.MobileNumber;
            entity.PurchaseDate = model.PurchaseDate;
            entity.TotalCost = model.TotalCost;
            entity.CreatedDate = model.CreatedDate;
        }
    }
}
