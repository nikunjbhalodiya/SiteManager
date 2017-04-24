using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Entity
    {
        public int EntityId { get; set; }

        public int EntityTypeId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
