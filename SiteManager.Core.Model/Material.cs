﻿using System;
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

        public MaterialType SelectedMaterialType { get; set; }

        public Vendor SelectedVendor { get; set; }

        public QuantityUnitType SelectedUnit { get; set; }

        public string BillNumber { get; set; }

        public decimal BillAmount { get; set; }

        public decimal Quantity { get; set; }

        public DateTime BillDate { get; set; }

        public string Remark { get; set; }

        public int SiteId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string QuantityUnit
        {
            get { return Quantity + " " + SelectedUnit.UnitName; }
        }

    }
}
