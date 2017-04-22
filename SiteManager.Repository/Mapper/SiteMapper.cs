using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SiteManager.Repository.Mapper
{
    public class SiteMapper : MapperBase<SiteModel, SiteEntity>
    {
        public override SiteEntity Map(SiteModel model)
        {
            return new SiteEntity
            {
                SiteId = model.SiteId,
                SiteName = model.SiteName,
                Address = model.Address,
                CreatedDate = model.CreatedDate
            };
        }

        public override SiteModel Map(SiteEntity entity)
        {
            return new SiteModel
            {
                SiteId = entity.SiteId,
                SiteName = entity.SiteName,
                Address = entity.Address,
                CreatedDate = entity.CreatedDate
            };
        }
    }
}
