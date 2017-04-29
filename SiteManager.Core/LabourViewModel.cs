using SiteManager.Core.Command;
using SiteManager.Core.Model;
using SiteManager.Core.TempModel;
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
            var contractors = _repositoryManager.GetContractors();
            var workTypes = _repositoryManager.GetWorkTypes();

            AddContractor = new RelayCommand(AddContractorCmd);
            AddWorkType = new RelayCommand(AddWorkTypeCmd);
            AddLabour = new RelayCommand(AddLabourCmd);
            ContractorToAdd = new Contractor();
            WorkTypeToAdd = new WorkType();
            LabourToAdd = new Labour();
            DeleteContractor = new RelayCommand(DeleteContractorCmd);
            DeleteWorkType = new RelayCommand(DeleteWorkTypecmd);
            
            _contractors = new ObservableCollection<Contractor>(contractors);
            ContractorList = ProcessContractorList(contractors);
            _workTypes = ProcessWorkTypeKeyValue(workTypes);
            _labours = new ObservableCollection<Labour>(_repositoryManager.GetLabourPayments(SiteId));
        }

        private void DeleteWorkTypecmd(object obj)
        {
            var worktype = obj as WorkType;
            if (OnMessageBoxEvent("Do you want to delete this entry?"))
                WorkTypes.Remove(worktype);
        }

        private void DeleteContractorCmd(object obj)
        {
            var contractor = obj as Contractor;
            if (OnMessageBoxEvent("Do you want to delete this entry?"))
                _contractors.Remove(contractor);
        }

        private void AddLabourCmd(object model)
        {
            var labour = model as Labour;
            labour.SiteId = SiteId;
            labour.CreateDate = DateTime.Now;
            labour.Contractor = Contractors.Single(x => x.ContractorId == SelectedContractor.ContractorId);

            if (labour.Payment < 1 || labour.PaymentDate == default(DateTime) || labour.Contractor.ContractorId == 0 || labour.WorkType.WorkTypeId == 0)
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

            _repositoryManager.AddWorkType(worktype);
            WorkTypeToAdd = new WorkType();
            var works = _repositoryManager.GetWorkTypes();
            WorkTypes = ProcessWorkTypeKeyValue(works);
            LabourToAdd.WorkType = WorkTypes.First();
            OnPropertyChanged(nameof(LabourToAdd));
        }

        private void AddContractorCmd(object model)
        {
            var contractor = model as Contractor;
            contractor.CreateDate = DateTime.Now;
            
            if (string.IsNullOrWhiteSpace(contractor.ContractorName))
            {
                return;
            }

            _repositoryManager.AddContractor(contractor);
            ContractorToAdd = new Contractor();
            var con = _repositoryManager.GetContractors();
            Contractors = new ObservableCollection<Contractor>(con);
            ContractorList = ProcessContractorList(con);
            SelectedContractor = ContractorList.First();
        }

        private ObservableCollection<ContractorKeyValue> _contractorList;

        public ObservableCollection<ContractorKeyValue> ContractorList
        {
            get { return _contractorList; }
            set { _contractorList = value; OnPropertyChanged(nameof(ContractorList)); }
        }

        private ContractorKeyValue _selectedContractor;

        public ContractorKeyValue SelectedContractor
        {
            get { return _selectedContractor; }
            set { _selectedContractor = value; OnPropertyChanged(nameof(SelectedContractor)); }
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

        private ObservableCollection<ContractorKeyValue> ProcessContractorList(IEnumerable<Contractor> contractorList)
        {
            var list = new List<ContractorKeyValue>() { new ContractorKeyValue { ContractorId = 0, ContractorName = "Select" } };
            list.AddRange(contractorList.Select(x => 
            new ContractorKeyValue { ContractorId = x.ContractorId, ContractorName = x.ContractorName }));
            return new ObservableCollection<ContractorKeyValue>(list);
        }

        private ObservableCollection<WorkType> ProcessWorkTypeKeyValue(IEnumerable<WorkType> workTypeList)
        {
            var list = new List<WorkType>() { new WorkType { WorkTypeId = 0, WorkTypeName = "Select" } };
            list.AddRange(workTypeList);
            return new ObservableCollection<WorkType>(list);
        }

    }
}
