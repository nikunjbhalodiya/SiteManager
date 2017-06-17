using SiteManager.Core.Model;
using SiteManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteManager.Repository.Mapper;


namespace SiteManager.Repository
{
    public class RepositoryManager
    {
        SqliteContext _context;
        Repository<SiteEntity> _siteRepo;
        Repository<MaterialEntity> _materialRepo;
        Repository<CustomerEntity> _customerRepo;
        Repository<VendorEntity> _vendorRepo;
        Repository<SupervisorEntity> _supervisorRepo;
        Repository<ContractorEntity> _contractorRepo;
        Repository<WorkTypeEntity> _workTypeRepo;
        Repository<LabourEntity> _labourRepo;
        Repository<MaterialTypeEntity> _materialTypeRepo;
        Repository<UnitEntity> _unitTypeRepo;
        Repository<DebitCreditEntity> _debitCreditRepo;
        Repository<CustomerDeleted> _deletedCustomerRepo;
        
        public RepositoryManager(SqliteContext context)
        {
            _context = context;
            _siteRepo = new Repository<SiteEntity>(_context);
            _materialRepo = new Repository<MaterialEntity>(_context);
            _customerRepo = new Repository<CustomerEntity>(_context);
            _vendorRepo = new Repository<VendorEntity>(_context);
            _supervisorRepo = new Repository<SupervisorEntity>(_context);
            _contractorRepo = new Repository<ContractorEntity>(_context);
            _workTypeRepo = new Repository<WorkTypeEntity>(_context);
            _labourRepo = new Repository<LabourEntity>(_context);
            _materialTypeRepo = new Repository<MaterialTypeEntity>(_context);
            _unitTypeRepo = new Repository<UnitEntity>(_context);
            _debitCreditRepo = new Repository<DebitCreditEntity>(_context);
            _deletedCustomerRepo = new Repository<CustomerDeleted>(_context);

        }

        public IEnumerable<SiteModel> GetSites()
        {
            _context.Database.ExecuteSqlCommand("PRAGMA foreign_keys = ON");
            var sites = _siteRepo.GetAll().ToList();
            var mapper = new SiteMapper();
            return mapper.Map(sites);
        }

        public void DeleteLabour(Labour labour)
        {
            var mapper = new LaboursPaymentMapper();
            var entity = mapper.Map(labour);
            var entityToDelete = _labourRepo.Get(entity.LabourId);
            _labourRepo.Delete(entityToDelete);
            _labourRepo.Save();
        }

        public void AddSite(SiteModel site)
        {
            var repository = new Repository<SiteEntity>(_context);
            var mapper = new SiteMapper();
            var entity = mapper.Map(site);
            _siteRepo.Add(entity);
            _siteRepo.Save();
        }

        public void DeleteContractor(Contractor contractor)
        {
            var mapper = new ContractorMapper();
            var entity = mapper.Map(contractor);
            var entityToDelete = _contractorRepo.Get(entity.ContractorId);
            _contractorRepo.Delete(entityToDelete);
            var labourEntity = _labourRepo.Find(x => x.ContractorId == entityToDelete.ContractorId);
            labourEntity.ToList().ForEach(x => x.ContractorId = null);
            _labourRepo.Save();
            _contractorRepo.Save();
        }

        public void DeleteCustomer(Customer customer)
        {
            var mapper = new CustomerMapper();
            var entity = mapper.Map(customer);
            var entityToDelete = _customerRepo.Get(entity.CustomerId);
            _customerRepo.Delete(entityToDelete);
            _customerRepo.Save();

            AddDeletedCustomerToBackupTable(entityToDelete);
        }

        private void AddDeletedCustomerToBackupTable(CustomerEntity entityToDelete)
        {
            var mapper = new DeletedCustomerMapper();
            var entityToAdd = mapper.Map(entityToDelete);
            _deletedCustomerRepo.Add(entityToAdd);
            _deletedCustomerRepo.Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            IEnumerable<DebitCreditEntity> creditDebitEntity = Enumerable.Empty<DebitCreditEntity>();
            var mapper = new CustomerMapper();
            var cust = _customerRepo.Get(customer.CustomerId);
            if (cust.HouseNumber == customer.HouseNumber || cust.ExtraCost != customer.ExtraCost || cust.TotalCost != customer.TotalCost)
            {
              creditDebitEntity = _debitCreditRepo.Find(x => x.EntityId == customer.CustomerId && x.EntityTypeId == 1 && x.SiteId == customer.SiteId);
            }

            mapper.Map(customer, cust);
            _customerRepo.Update(cust);
            _customerRepo.Save();

            UpdateCreditDebitEntity(creditDebitEntity, cust.TotalCost + cust.ExtraCost);
        }

        private void UpdateCreditDebitEntity(IEnumerable<DebitCreditEntity> creditDebitEntity, decimal totalAmt)
        {
            if (!creditDebitEntity.Any())
                return;

            var ordereddate = creditDebitEntity.OrderByDescending(x => x.DebitAmount);
            decimal remainingAmt = totalAmt;
            for (int i = 0; i < ordereddate.Count(); i++)
            {
                var element = ordereddate.ElementAt(i);
                remainingAmt = remainingAmt - element.CreditAmount;
                element.DebitAmount = remainingAmt;
                _debitCreditRepo.Update(element);
                _debitCreditRepo.Save();
            }
        }

        public IEnumerable<Supervisor> GetSupervisorsBySiteId(int siteId)
        {
            var supervisor = _supervisorRepo.Find(x => x.SiteId == siteId);
            var mapper = new SupervisorMapper();
            return mapper.Map(supervisor.ToList());
        }

        public void AddSupervisor(Supervisor supervisor)
        {
            var mapper = new SupervisorMapper();
            var entity = mapper.Map(supervisor);
            _supervisorRepo.Add(entity);
            _supervisorRepo.Save();
        }

        public IEnumerable<Customer> GetCustomerBySiteId(int siteId)
        {
            var customers = _customerRepo.Find(x => x.SiteId == siteId);
            var mapper = new CustomerMapper();
            return mapper.Map(customers.ToList());
        }



        public void AddCustomer(Customer customer)
        {
            var mapper = new CustomerMapper();
            var entity = mapper.Map(customer);
            _customerRepo.Add(entity);
            _customerRepo.Save();
        }

        public IEnumerable<Material> GetMaterialBySiteId(int siteId)
        {
            var materials = _materialRepo.Find(x => x.SiteId == siteId);
            var mapper = new MaterialMapper();
            return mapper.Map(materials.ToList());
        }

        public void DeleteMaterial(Material material)
        {
            var mapper = new MaterialMapper();
            var entity = mapper.Map(material);
            var entityToDelete = _materialRepo.Get(entity.MaterialId);
            _materialRepo.Delete(entityToDelete);
            _materialRepo.Save();
        }

        public void DeleteSupervisor(Supervisor supervisorModel)
        {
            var mapper = new SupervisorMapper();
            var entity = mapper.Map(supervisorModel);
            var entityToDelete = _supervisorRepo.Get(entity.SupervisorId);
            _supervisorRepo.Delete(entityToDelete);
            _supervisorRepo.Save();
        }

        public void DeleteSite(SiteModel site)
        {
            var mapper = new SiteMapper();
            var entity = mapper.Map(site);
            var entityToDelete = _siteRepo.Get(entity.SiteId);
            _context.Entry(entityToDelete).Collection(y => y.Customers).Load();
            _context.Entry(entityToDelete).Collection(y => y.Supervisors).Load();
            _siteRepo.Delete(entityToDelete);
            _siteRepo.Save();
        }


        public IEnumerable<MaterialType> GetMaterialTypes()
        {
            return _materialTypeRepo.GetAll().Select(x => new MaterialType { MaterialTypeId = x.MaterialTypeId, MaterialTypeName = x.MaterialTypeName });
        }

        public IEnumerable<QuantityUnitType> GetUnitsOfMaterial()
        {
            return _unitTypeRepo.GetAll().Select(x => new QuantityUnitType { UnitId = x.UnitId, UnitName = x.UnitName });
        }

        public void AddMaterialType(MaterialType materialType)
        {
            var entity = new MaterialTypeEntity { MaterialTypeId = materialType.MaterialTypeId, MaterialTypeName = materialType.MaterialTypeName };
            _materialTypeRepo.Add(entity);
            _materialTypeRepo.Save();
        }

        public void AddUnitType(QuantityUnitType unit)
        {
            var entity = new UnitEntity { UnitId = unit.UnitId, UnitName = unit.UnitName };
            _unitTypeRepo.Add(entity);
            _unitTypeRepo.Save();
        }

        public IEnumerable<Vendor> GetVendorBySiteId(int siteId)
        {
            var vendors = _vendorRepo.GetAll();
            var mapper = new VendorMapper();
            return mapper.Map(vendors.ToList());
        }

        public void AddVendor(Vendor vendor)
        {
            var mapper = new VendorMapper();
            var entity = mapper.Map(vendor);
            _vendorRepo.Add(entity);
            _vendorRepo.Save();
        }

        public void DeleteVendor(Vendor vendor)
        {
            var mapper = new VendorMapper();
            var entity = mapper.Map(vendor);
            var entityToDelete = _vendorRepo.Get(vendor.VendorId);
            _vendorRepo.Delete(entityToDelete);
            var materialEntity = _materialRepo.Find(x => x.VendorId == entityToDelete.VendorId);
            materialEntity.ToList().ForEach(x => x.VendorId = null);
            _materialRepo.Save();
            _vendorRepo.Save();
        }

        public void AddMaterial(Material material)
        {
            var mapper = new MaterialMapper();
            var entity = mapper.Map(material);
            _materialRepo.Add(entity);
            _materialRepo.Save();
        }

        public IEnumerable<WorkType> GetWorkTypes()
        {
            var entities = _workTypeRepo.GetAll();
            var mapper = new WorkTypeMapper();
            return mapper.Map(entities.ToList());
        }

        public IEnumerable<Contractor> GetContractors()
        {
            var entities = _contractorRepo.GetAll();
            var mapper = new ContractorMapper();
            return mapper.Map(entities.ToList());
        }

        public IEnumerable<Labour> GetLabourPayments(int siteId)
        {
            var entities = _labourRepo.Find(x => x.SiteId == siteId);
            var mapper = new LaboursPaymentMapper();
            return mapper.Map(entities.ToList());
        }

        public void AddLabourPayment(Labour labour)
        {
            var mapper = new LaboursPaymentMapper();
            var entity = mapper.Map(labour);
            _labourRepo.Add(entity);
            _labourRepo.Save();
        }

        public void AddWorkType(WorkType worktype)
        {
            var mapper = new WorkTypeMapper();
            var entity = mapper.Map(worktype);
            _workTypeRepo.Add(entity);
            _workTypeRepo.Save();
        }

        public void AddContractor(Contractor contractor)
        {
            var mapper = new ContractorMapper();
            var entity = mapper.Map(contractor);
            _contractorRepo.Add(entity);
            _contractorRepo.Save();
        }

        public IEnumerable<EntityType> GetPaymentEntity()
        {
            var paymentEntityRepo = new Repository<PaymentEntity>(_context);
            var list = paymentEntityRepo.GetAll();
            var model = list.Select(x => new EntityType { EntityTypeId = x.EntityTypeId, EntityTypeName = x.EntityTypeName }).ToList();
            model.Insert(0, new EntityType { EntityTypeId = 0, EntityTypeName = "Select" });
            return model.OrderBy(x => x.EntityTypeId);
        }

        public IEnumerable<PaymentMode> GetPaymentMode()
        {
            var paymentEntityRepo = new Repository<PaymentModeEntity>(_context);
            return paymentEntityRepo.GetAll().Select(x => new PaymentMode { PaymentModeId = x.PaymentModeId, Content = x.PaymentModeName });
        }

        public IEnumerable<DebitCreditOfPayment> GetDebitCreditListOfEntity(Entity entity)
        {
            var list = _debitCreditRepo.Find(x => x.EntityId == entity.EntityId
            && x.EntityTypeId == entity.EntityTypeId
            && x.SiteId == entity.SiteId);

            var mapper = new DebitCreditMapper();
            return mapper.Map(list.ToList());
        }

        public void AddPaymentForEntity(DebitCreditOfPayment paymentInfo)
        {
            var mapper = new DebitCreditMapper();
            var entity = mapper.Map(paymentInfo);
            _debitCreditRepo.Add(entity);
            _debitCreditRepo.Save();
        }

        public IEnumerable<Entity> SearchEntity(int entityTypeId, string searchText, int siteId)
        {
            var entity = Enumerable.Empty<Entity>();
            switch (entityTypeId)
            {
                case 1:
                    var custEntity = _customerRepo.Find(x => x.CustomerName.ToLower().Contains(searchText.ToLower()) && x.SiteId == siteId);
                    entity = custEntity.Select(x => new Entity { Identity = x.HouseNumber, EntityId = x.CustomerId, EntityTypeId = entityTypeId, Date = x.CreatedDate, Name = x.CustomerName, TotalAmount = x.TotalCost + x.ExtraCost, SiteId = siteId });
                    break;
                case 2:
                    var materialEntity = _materialRepo.Find(x => x.MaterialType.MaterialTypeName.ToLower().Contains(searchText.ToLower()) && x.SiteId == siteId);
                    entity = materialEntity.Select(x => new Entity { Identity = x.BillNumber, EntityId = x.MaterialId, EntityTypeId = entityTypeId, Date = x.CreatedDate, Name = x.MaterialType.MaterialTypeName, TotalAmount = x.BillAmount, SiteId = siteId });
                    break;
                case 3:
                    var supervisorEntity = _supervisorRepo.Find(x => x.SupervisorName.ToLower().Contains(searchText.ToLower()) && x.SiteId == siteId);
                    entity = supervisorEntity.Select(x => new Entity { Identity = x.DutyDescription, EntityId = x.SupervisorId, EntityTypeId = entityTypeId, Date = x.CreatedDate, Name = x.SupervisorName, TotalAmount = x.Salary, SiteId = siteId });
                    break;
                default:
                    throw new ArgumentException("Invalid entity type.");
            }

            return entity;
        }
    }
}
