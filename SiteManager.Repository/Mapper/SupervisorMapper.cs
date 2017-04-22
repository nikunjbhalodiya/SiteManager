using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Repository.Mapper
{
    public class SupervisorMapper : MapperBase<Supervisor, SupervisorEntity>
    {
        public override SupervisorEntity Map(Supervisor model)
        {
            return new SupervisorEntity
            {
                SupervisorId = model.SupervisorId,
                DutyDescription = model.DutyDescription,
                Salary = model.MonthlySalary,
                CreatedDate = model.CreatedDate,
                SiteId = model.SiteId,
                SupervisorName = model.SupervisorName
            };
        }

        public override Supervisor Map(SupervisorEntity entity)
        {
            return new Supervisor
            {
                SupervisorId = entity.SupervisorId,
                CreatedDate = entity.CreatedDate,
                DutyDescription = entity.DutyDescription,
                MonthlySalary = entity.Salary,
                SiteId = entity.SiteId,
                SupervisorName = entity.SupervisorName
            };
        }
    }
}
