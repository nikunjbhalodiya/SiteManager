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
        
        public MaterialViewModel(int siteId)
        {
            _repositoryManager = new RepositoryManager(new SqliteContext());
            SiteId = siteId;
            _materialTypes =  ProcessMaterialTypes(_repositoryManager.GetMaterialTypes());
            _units = ProcessUnitType( _repositoryManager.GetUnitsOfMaterial());
            var vendors = _repositoryManager.GetVendorBySiteId(SiteId);
            var materials = _repositoryManager.GetMaterialBySiteId(SiteId).ToList();
           
            VendorToAdd = new Vendor();
            MaterialToAdd = new Material();
            AddVendor = new RelayCommand(AddVendorCommand);
            AddMaterial = new RelayCommand(AddMaterialCommand);
            AddMaterialType = new RelayCommand(AddMaterialTypeCmd);
            AddUnitType = new RelayCommand(AddUnitTypeCmd);
            DeleteVendor = new RelayCommand(DeleteVendorCommand);
            _vendors = new ObservableCollection<Vendor>(vendors);
            _materials = new ObservableCollection<Material>(materials);
            VendorList = ProcessVendors(vendors);
            SelectedVendor = new VendorKeyValue();
            MaterialToAdd.SelectedMaterialType = MaterialTypes.First();
            MaterialToAdd.SelectedUnit = Units.First();
            MaterialTypeToAdd = new MaterialType();
            UnitTypeToAdd = new QuantityUnitType();
        }

        private void AddUnitTypeCmd(object obj)
        {
            var unitType = obj as QuantityUnitType;
            if (string.IsNullOrWhiteSpace(unitType.UnitName))
            {
                return;
            }
            _repositoryManager.AddUnitType(unitType);

            UnitTypeToAdd = new QuantityUnitType();
            Units = ProcessUnitType(_repositoryManager.GetUnitsOfMaterial());
            MaterialToAdd.SelectedUnit = Units.First();
            OnPropertyChanged(nameof(MaterialToAdd));
        }

        private void AddMaterialTypeCmd(object obj)
        {
            var materialType = obj as MaterialType;
            if (string.IsNullOrWhiteSpace(materialType.MaterialTypeName))
            {
                return;
            }
            _repositoryManager.AddMaterialType(materialType);

            MaterialTypeToAdd = new MaterialType();
            MaterialTypes = ProcessMaterialTypes(_repositoryManager.GetMaterialTypes());
            MaterialToAdd.SelectedMaterialType = MaterialTypes.First();
            OnPropertyChanged(nameof(MaterialToAdd));
        }

        private void DeleteVendorCommand(object model)
        {
            var vendor = model as Vendor;

            if (OnMessageBoxEvent("Do you want to delete this entry?"))
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

        private ObservableCollection<QuantityUnitType> _units;

        public ObservableCollection<QuantityUnitType> Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

        private ObservableCollection<MaterialType> _materialTypes;

        public ObservableCollection<MaterialType> MaterialTypes
        {
            get { return _materialTypes; }
            set { _materialTypes = value; OnPropertyChanged(nameof(MaterialTypes)); }
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
                || material.SelectedMaterialType.MaterialTypeId == 0
                || material.SelectedUnit.UnitId == 0)
            {
                return;
            }
            material.SiteId = SiteId;
            material.CreatedDate = DateTime.Now;
            _repositoryManager.AddMaterial(material);
            Materials = new ObservableCollection<Material>(_repositoryManager.GetMaterialBySiteId(SiteId));
            MaterialToAdd = new Material();
            MaterialToAdd.SelectedUnit = Units.First();
            SelectedVendor = VendorList.First();
            MaterialToAdd.SelectedMaterialType = MaterialTypes.First();
            OnPropertyChanged(nameof(MaterialToAdd));
        }

        private void AddVendorCommand(object model)
        {
            var vendor = model as Vendor;
            if (string.IsNullOrWhiteSpace(vendor.VendorName) || string.IsNullOrWhiteSpace(vendor.Address))
            {
                return;
            }
            vendor.CreatedDate = DateTime.Now;
            _repositoryManager.AddVendor(vendor);
            var list = _repositoryManager.GetVendorBySiteId(SiteId);
            Vendors =  new ObservableCollection<Vendor>(list);
            VendorToAdd = new Vendor();
            VendorList = ProcessVendors(list);
            SelectedVendor = VendorList.First();
            OnPropertyChanged(nameof(SelectedVendor));
        }

        public RelayCommand AddVendor { get; set; }
        public RelayCommand AddMaterial { get; set; }

        public RelayCommand DeleteMaterial { get; set; }

        public RelayCommand DeleteVendor { get; set; }

        public RelayCommand AddMaterialType { get; set; }

        public RelayCommand AddUnitType { get; set; }

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

        private MaterialType _materialTypeToAdd;

        public MaterialType MaterialTypeToAdd
        {
            get { return _materialTypeToAdd; }
            set { _materialTypeToAdd = value; OnPropertyChanged(nameof(MaterialTypeToAdd)); }
        }

        private QuantityUnitType _unitTypeToAdd;

        public QuantityUnitType UnitTypeToAdd
        {
            get { return _unitTypeToAdd; }
            set { _unitTypeToAdd = value; OnPropertyChanged(nameof(UnitTypeToAdd)); }
        }


        private ObservableCollection<MaterialType> ProcessMaterialTypes(IEnumerable<MaterialType> materialTypes)
        {
            var list = new List<MaterialType> { new MaterialType { MaterialTypeId = 0, MaterialTypeName = "Select" } };
            list.AddRange(materialTypes);
            return new ObservableCollection<MaterialType>(list);
        }

        private ObservableCollection<QuantityUnitType> ProcessUnitType(IEnumerable<QuantityUnitType> unitTypes)
        {
            var list = new List<QuantityUnitType> { new QuantityUnitType { UnitId = 0, UnitName = "Select" } };
            list.AddRange(unitTypes);
            return new ObservableCollection<QuantityUnitType>(list);
        }

        private ObservableCollection<VendorKeyValue> ProcessVendors(IEnumerable<Vendor> vendors)
        {
            var list = new List<VendorKeyValue> { new VendorKeyValue { VendorId = 0, VendorName = "Select" } };
            list.AddRange(vendors.Select(x => new VendorKeyValue { VendorId = x.VendorId, VendorName = x.VendorName  }));
            return new ObservableCollection<VendorKeyValue>(list);
        }
    }
}
