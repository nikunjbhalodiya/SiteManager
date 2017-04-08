using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;
using System.Linq;

namespace SiteManager.Core
{
    public class MaterialViewModel : ViewModelBase
    {
        public MaterialViewModel()
        {
            _materialTypes = new Dictionary<int, string>
            {
                {  0, "Select" },
                {  1, "Sand" },
                {  2, "Bricks" },
                {  3, "Cement" },
                {  4, "Metal" },
                {  5, "Marbles" },
                {  6, "Playwood" },
            };

            _units = new Dictionary<int, string>
            {
                {  0, "Select" },
                {  1, "Tonne" },
                {  2, "Kg" },
                {  3, "Liter" },
            };

            IsVendorGridEnabled = false;
            VendorToAdd = new Vendor();
            MaterialToAdd = new Material();
            AddVendor = new RelayCommand(AddVendorCommand);
            AddMaterial = new RelayCommand(AddMaterialCommand);
            DeleteVendor = new RelayCommand(DeleteVendorCommand);
            _vendors = new ObservableCollection<Vendor>(new List<Vendor> { new Vendor { VendorId = 1, VendorName = "Ni", Address = "Meshana", Remark = "Brick sender" } });
            _materials = new ObservableCollection<Material>(new List<Material> { new Material { BillAmount = 2500, BillDate = DateTime.Today, BillNumber = "0458", Quantity = 10, SelectedVendor = new KeyValuePair<int, string>(1, "Umiyadgfdgfdggd Treders LTD"), SelectedMaterialType = new KeyValuePair<int, string>(2, "Bricksfghfhhfghfghfghfghfghfgh"), SelectedUnit = new KeyValuePair<int, string>(3, "Tonne"), } });
            VendorList = Vendors.Select(x => new { Key = x.VendorId, Value = x.VendorName }).ToDictionary(x => x.Key, x => x.Value);
            VendorList.Add(0, "Select");
        }

        private void DeleteVendorCommand(object model)
        {
            var vendor = model as Vendor;

            if (OnMessageBoxEvent())
                _vendors.Remove(vendor);
        }

        private ObservableCollection<Vendor> _vendors;

        public ObservableCollection<Vendor> Vendors
        {
            get { return _vendors; }
            set
            {
                _vendors = value;
                OnPropertyChanged(nameof(Vendors));
            }
        }


        private Dictionary<int, string> _vendorList;

        public Dictionary<int, string> VendorList
        {
            get { return _vendorList; }
            set
            {
                _vendorList = value;
                OnPropertyChanged(nameof(VendorList));
            }
        }


        private Dictionary<int, string> _materialTypes;

        public Dictionary<int, string> MaterialTypes
        {
            get { return _materialTypes; }
            set { _materialTypes = value; OnPropertyChanged(nameof(MaterialTypes)); }
        }

        private Dictionary<int, string> _units;

        public Dictionary<int, string> Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

        private ObservableCollection<Material> _materials;

        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set { _materials = value; OnPropertyChanged(nameof(Materials)); }
        }


        private void AddMaterialCommand(object model)
        {
            var material = model as Material;
            if (material.BillAmount <= 0
                || material.Quantity <= 0
                || string.IsNullOrWhiteSpace(material.BillNumber)
                || material.SelectedVendor.Key == 0
                || material.SelectedMaterialType.Key == 0
                || material.SelectedUnit.Key == 0)
            {
                return;
            }

            _materials.Add(material);
            MaterialToAdd = new Material();
        }

        private void AddVendorCommand(object model)
        {
            var vendor = model as Vendor;
            if (string.IsNullOrWhiteSpace(vendor.VendorName) || string.IsNullOrWhiteSpace(vendor.Address))
            {
                return;
            }
            Vendors.Add(vendor);
            VendorToAdd = new Vendor();
            VendorList = Vendors.Select(x => new { Key = x.VendorId, Value = x.VendorName }).ToDictionary(x => x.Key, x => x.Value);
            IsVendorGridEnabled = false;
        }

        public RelayCommand AddVendor { get; set; }
        public RelayCommand AddMaterial { get; set; }

        public RelayCommand DeleteMaterial { get; set; }

        public RelayCommand DeleteVendor { get; set; }

        private Vendor _vendorToAdd;

        public Vendor VendorToAdd
        {
            get { return _vendorToAdd; }
            set { _vendorToAdd = value; OnPropertyChanged(nameof(VendorToAdd)); }
        }

        private Material _materialToAdd;

        public Material MaterialToAdd
        {
            get { return _materialToAdd; }
            set { _materialToAdd = value; OnPropertyChanged(nameof(MaterialToAdd)); }
        }

        private bool _isVendorGridEnabled;

        public bool IsVendorGridEnabled
        {
            get { return _isVendorGridEnabled; }
            set { _isVendorGridEnabled = value; OnPropertyChanged(nameof(IsVendorGridEnabled)); }
        }
    }
}
