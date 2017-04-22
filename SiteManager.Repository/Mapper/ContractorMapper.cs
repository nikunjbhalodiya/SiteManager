using SiteManager.Core.Model;
using SiteManager.Repository.Entities;

namespace SiteManager.Repository.Mapper
{
    public class ContractorMapper : MapperBase<Contractor, ContractorEntity>
    {
        public override ContractorEntity Map(Contractor model)
        {
            return new ContractorEntity
            {
                ContractorId = model.ContractorId,
                ContractorName = model.ContractorName,
                Detail = model.Detail,
                CreateDate = model.CreateDate
            };
        }

        public override Contractor Map(ContractorEntity entity)
        {
            return new Contractor
            {
                ContractorId = entity.ContractorId,
                ContractorName = entity.ContractorName,
                Detail = entity.Detail,
                CreateDate = entity.CreateDate
            };
        }
    }
}
