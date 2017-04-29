using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class DebitCreditMapper : MapperBase<DebitCreditOfPayment, DebitCreditEntity>
    {
        public override DebitCreditEntity Map(DebitCreditOfPayment model)
        {
            return new DebitCreditEntity
            {
                EntityId = model.EntityId,
                EntityTypeId = model.EntityType.EntityTypeId,
                paymentId = model.paymentId,
                PaymentDate = model.PaymentDate,
                PaymentModeId = model.SelectedMode.PaymentModeId,
                CreditAmount = model.CreditAmount,
                DebitAmount = model.DebitAmount,
                SiteId = model.SiteId,
                Remark = model.Remark
            };
        }

        public override DebitCreditOfPayment Map(DebitCreditEntity entity)
        {
            return new DebitCreditOfPayment
            {
                EntityId = entity.EntityId,
                EntityType = new EntityType { EntityTypeId = entity.EntityType.EntityTypeId, EntityTypeName = entity.EntityType.EntityTypeName },
                paymentId = entity.paymentId,
                PaymentDate = entity.PaymentDate,
                SelectedMode = new PaymentMode { PaymentModeId = entity.PaymentMode.PaymentModeId, Content = entity.PaymentMode.PaymentModeName },
                CreditAmount = entity.CreditAmount,
                DebitAmount = entity.DebitAmount,
                SiteId = entity.SiteId,
                Remark = entity.Remark
            };
        }
    }
}
