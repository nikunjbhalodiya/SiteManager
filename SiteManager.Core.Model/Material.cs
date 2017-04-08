using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core.Model
{
    public class Material
    {
        public Material()
        {
            BillDate = DateTime.Today;
        }
        public int MaterialDetailId { get; set; }
        public Dictionary<int, string> MaterialTypes { get; set; }

        public Dictionary<int, string> Vendors { get; set; }

        public Dictionary<int, string> Units { get; set; }

        public KeyValuePair<int, string> SelectedMaterialType { get; set; }

        public KeyValuePair<int, string> SelectedVendor { get; set; }

        public KeyValuePair<int, string> SelectedUnit { get; set; }

        public string BillNumber { get; set; }

        public decimal BillAmount { get; set; }

        public decimal Quantity { get; set; }

        public DateTime BillDate { get; set; }

        public string Remark { get; set; }
    }
}
