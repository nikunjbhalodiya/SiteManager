using SiteManager.Core.Model;
using SiteManager.Repository.Entities;

namespace SiteManager.Repository.Mapper
{
    public class LaboursPaymentMapper : MapperBase<Labour, LabourEntity>
    {
        public override LabourEntity Map(Labour model)
        {
            return new LabourEntity
            {
                ContractorId = model.Contractor.ContractorId,
                CreateDate = model.CreateDate,
                LabourId = model.LabourId,
                Payment = model.Payment,
                PaymentDate = model.PaymentDate,
                Remark = model.Remark,
                SiteId = model.SiteId,
                WorkTypeId = model.WorkType.WorkTypeId,
                WorkDetail = model.WorkDetail
            };

        }

        public override Labour Map(LabourEntity entity)
        {
            return new Labour
            {
                CreateDate = entity.CreateDate,
                LabourId = entity.LabourId,
                Contractor = entity.Contractor != null ?  new ContractorMapper().Map(entity.Contractor) : new Contractor(),
                WorkType = new WorkTypeMapper().Map(entity.WorkType),
                Payment = entity.Payment,
                PaymentDate = entity.PaymentDate,
                Remark = entity.Remark,
                SiteId = entity.SiteId,
                WorkDetail = entity.WorkDetail
            };
        }
    }
}
