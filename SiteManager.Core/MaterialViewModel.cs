using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;
using System.Linq;
using SiteManager.Repository;
using SiteManager.Core.TempModel;

namespace SiteManager.Core
{
    public class MaterialViewModel : ViewModelBase
    {
        private readonly RepositoryManager _repositoryManager;
        private int _materialcount;
        private int _vendorCount;
        public MaterialViewModel(int siteId)
        {
            _repositoryManager = new RepositoryManager(new SqliteContext());
            SiteId = siteId;
            _materialTypes = _repositoryManager.GetMaterialTypes();
            _units = _repositoryManager.GetUnitsOfMaterial();
            var vendors = _repositoryManager.GetVendorBySiteId(SiteId);
            var materials = _repositoryManager.GetMaterialBySiteId(SiteId).ToList();
           
            IsVendorGridEnabled = false;
            VendorToAdd = new Vendor();
            MaterialToAdd = new Material();
            AddVendor = new RelayCommand(AddVendorCommand);
            AddMaterial = new RelayCommand(AddMaterialCommand);
            DeleteVendor = new RelayCommand(DeleteVendorCommand);
            _vendors = new ObservableCollection<Vendor>(vendors);
            _materials = new ObservableCollection<Material>(materials);
            VendorList = new ObservableCollection<VendorKeyValue>(Vendors.Select(x => new VendorKeyValue() { VendorId = x.VendorId, VendorName = x.VendorName }));
            VendorList.Add(new VendorKeyValue { VendorId = 0, VendorName = "Select" });
            VendorList = new ObservableCollection<VendorKeyValue>(VendorList.OrderBy(x => x.VendorId));
            _materialcount = _materials.Any() ? _materials.Last().MaterialDetailId : 0;
            _vendorCount = _vendors.Any() ? _vendors.Last().VendorId : 0;
            SelectedVendor = new VendorKeyValue();
        }

        private void DeleteVendorCommand(object model)
        {
            var vendor = model as Vendor;

            if (OnMessageBoxEvent())
            {
                _repositoryManager.DeleteVendor(vendor);
                _vendors.Remove(vendor);
                VendorList.Remove(VendorList.Single(x => x.VendorId == vendor.VendorId));
            }
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


        private ObservableCollection<VendorKeyValue> _vendorList;

        public ObservableCollection<VendorKeyValue> VendorList
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

        private VendorKeyValue _selectedVendor;

        public VendorKeyValue SelectedVendor
        {
            get { return _selectedVendor; }
            set { _selectedVendor = value; }
        }


        private void AddMaterialCommand(object model)
        {
            var material = model as Material;

            material.SelectedVendor = Vendors.Single(x => x.VendorId == SelectedVendor.VendorId);
            if (material.BillAmount <= 0
                || material.Quantity <= 0
                || string.IsNullOrWhiteSpace(material.BillNumber)
                || material.SelectedVendor.VendorId == 0
                || material.SelectedMaterialType.Key == 0
                || material.SelectedUnit.Key == 0)
            {
                return;
            }
            material.SiteId = SiteId;
            material.CreatedDate = DateTime.Now;
            material.MaterialDetailId = _materialcount = _materialcount + 1;
            _repositoryManager.AddMaterial(material);
            _materials.Add(material);
            MaterialToAdd = new Material();
            MaterialToAdd.SelectedUnit = Units.First();
            SelectedVendor = VendorList.First();
            MaterialToAdd.SelectedMaterialType = MaterialTypes.First();
        }

        private void AddVendorCommand(object model)
        {
            var vendor = model as Vendor;
            if (string.IsNullOrWhiteSpace(vendor.VendorName) || string.IsNullOrWhiteSpace(vendor.Address))
            {
                return;
            }
            vendor.CreatedDate = DateTime.Now;
            vendor.VendorId = _vendorCount = _vendorCount + 1;
            Vendors.Add(vendor);
            _repositoryManager.AddVendor(vendor);
            VendorToAdd = new Vendor();
            VendorList.Add(new VendorKeyValue { VendorId = vendor.VendorId, VendorName =vendor.VendorName });
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
