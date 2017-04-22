using SiteManager.Core.Command;
using SiteManager.Core.Model;
using SiteManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core
{
    public class LabourViewModel : ViewModelBase
    {
        public bool isContractorAdded;
        public bool isWorkTypeAdded;
        private RepositoryManager _repositoryManager;
        public LabourViewModel(int siteId)
        {
            SiteId = siteId;
            _repositoryManager = new RepositoryManager(new SqliteContext());
            var contractors = _repositoryManager.GetContractors().ToList();
            var workTypes = _repositoryManager.GetWorkTypes().ToList();
            _workTypes = new ObservableCollection<WorkType>();
            _labours = new ObservableCollection<Labour>();
            AddContractor = new RelayCommand(AddContractorCmd);
            AddWorkType = new RelayCommand(AddWorkTypeCmd);
            AddLabour = new RelayCommand(AddLabourCmd);
            ContractorToAdd = new Contractor();
            WorkTypeToAdd = new WorkType();
            LabourToAdd = new Labour();
            DeleteContractor = new RelayCommand(DeleteContractorCmd);
            DeleteWorkType = new RelayCommand(DeleteWorkTypecmd);
            contractors.Insert(0, new Contractor { ContractorId = 0, ContractorName = "Select" });
            workTypes.Insert(0, new WorkType { WorkTypeId = 0, WorkTypeName = "Select" });
            _workTypes = new ObservableCollection<WorkType>(workTypes.OrderBy(x => x.WorkTypeId));
            _contractors = new ObservableCollection<Contractor>(contractors.OrderBy(x => x.ContractorId));
            _labours = new ObservableCollection<Labour>(_repositoryManager.GetLabourPayments(SiteId));
        }

        private void DeleteWorkTypecmd(object obj)
        {
            var worktype = obj as WorkType;
            if (OnMessageBoxEvent())
                WorkTypes.Remove(worktype);
        }

        private void DeleteContractorCmd(object obj)
        {
            var contractor = obj as Contractor;
            if (OnMessageBoxEvent())
                _contractors.Remove(contractor);
        }

        private void AddLabourCmd(object model)
        {
            var labour = model as Labour;
            labour.SiteId = SiteId;
            labour.CreateDate = DateTime.Now;

            if (labour.Payment < 1 || labour.PaymentDate == default(DateTime) || string.IsNullOrWhiteSpace(labour.Contractor.ContractorName) || string.IsNullOrWhiteSpace(labour.WorkType.WorkTypeName))
            {
                return;
            }

            _repositoryManager.AddLabourPayment(labour);
            _labours.Add(labour);
            LabourToAdd = new Labour();
            LabourToAdd.Contractor = Contractors.First();
            LabourToAdd.WorkType = WorkTypes.First();
            _labours = new ObservableCollection<Labour>(_repositoryManager.GetLabourPayments(SiteId));
        }

        private void AddWorkTypeCmd(object model)
        {
            var worktype = model as WorkType;
            if (string.IsNullOrWhiteSpace(worktype.WorkTypeName))
            {
                return;
            }

            //_workTypes.Add(worktype);
            _repositoryManager.AddWorkType(worktype);
            WorkTypeToAdd = new WorkType();
            WorkTypes = new ObservableCollection<WorkType>(_repositoryManager.GetWorkTypes());
        }

        private void AddContractorCmd(object model)
        {
            var contractor = model as Contractor;
            contractor.CreateDate = DateTime.Now;
            
            if (string.IsNullOrWhiteSpace(contractor.ContractorName))
            {
                return;
            }

            //_contractors.Add(contractor);
            _repositoryManager.AddContractor(contractor);
            ContractorToAdd = new Contractor();
            Contractors = new ObservableCollection<Contractor>(_repositoryManager.GetContractors());
        }

        private ObservableCollection<Labour> _labours;

        public ObservableCollection<Labour> Labours
        {
            get { return _labours; }
            set { _labours = value; OnPropertyChanged(nameof(Labours)); }
        }

        private ObservableCollection<Contractor> _contractors;

        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set
            {
                _contractors = value;
                OnPropertyChanged(nameof(Contractors));
            }
        }

        private ObservableCollection<WorkType> _workTypes;

        public ObservableCollection<WorkType> WorkTypes
        {
            get { return _workTypes; }
            set
            {
                _workTypes = value;
                OnPropertyChanged(nameof(WorkTypes));
            }
        }

        private Labour _labourToAdd;

        public Labour LabourToAdd
        {
            get { return _labourToAdd; }
            set { _labourToAdd = value; OnPropertyChanged(nameof(LabourToAdd)); }
        }

        private Contractor _contractorToAdd;

        public Contractor ContractorToAdd
        {
            get { return _contractorToAdd; }
            set
            {
                _contractorToAdd = value;
                OnPropertyChanged(nameof(ContractorToAdd));
            }
        }

        private WorkType _workTypeToAdd;

        public WorkType WorkTypeToAdd
        {
            get { return _workTypeToAdd; }
            set { _workTypeToAdd = value; OnPropertyChanged(nameof(WorkTypeToAdd)); }
        }

        public RelayCommand AddContractor { get; set; }

        public RelayCommand AddWorkType { get; set; }

        public RelayCommand AddLabour { get; set; }

        public RelayCommand DeleteContractor { get; set; }

        public RelayCommand DeleteWorkType { get; set; }

    }
}
