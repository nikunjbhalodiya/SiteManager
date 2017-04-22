using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class WorkTypeMapper : MapperBase<WorkType, WorkTypeEntity>
    {
        public override WorkTypeEntity Map(WorkType model)
        {
            return new WorkTypeEntity
            {
                WorkTypeId = model.WorkTypeId,
                WorkTypeName = model.WorkTypeName
            };
        }

        public override WorkType Map(WorkTypeEntity entity)
        {
            return new WorkType
            {
                WorkTypeName = entity.WorkTypeName,
                WorkTypeId = entity.WorkTypeId
            };
        }
    }
}
