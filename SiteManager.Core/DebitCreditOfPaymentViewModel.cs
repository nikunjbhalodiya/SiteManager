using SiteManager.Core.Command;
using SiteManager.Core.Model;
using SiteManager.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SiteManager.Core
{
    public class DebitCreditOfPaymentViewModel : ViewModelBase
    {
        private readonly RepositoryManager _repositoryManager;
        private Entity _entity;
        public DebitCreditOfPaymentViewModel(Entity entity)
        {
            _entity = entity;
            SiteId = _entity.SiteId;
            _repositoryManager = new RepositoryManager(new SqliteContext());
            PaymentToAdd = new DebitCreditOfPayment();
            Add = new RelayCommand(AddCommand);
            Search = new RelayCommand(SearchCommand);
            PaymentGridVisibility = Visibility.Hidden;
            EntityGridVisibility = Visibility.Hidden;
            _entityTypes = _repositoryManager.GetPaymentEntity().ToList();
            _paymentModes = _repositoryManager.GetPaymentMode().ToList();
            EntityClick = new RelayCommand(EntityClickCmd);
            
            EntityGridVisibility = Visibility.Visible;
            SetIdentity(_entity.EntityTypeId);
            _entities = new ObservableCollection<Entity>(new List<Entity> { _entity });
            SelectedEntity = _entityTypes.Single(x => x.EntityTypeId == _entity.EntityTypeId);
            EntityGridHeading = SelectedEntity.EntityTypeName + " Detail";
            GetPaymentSummary();
        }

        private void EntityClickCmd(object obj)
        {
            var entity = obj as Entity;
            _entity = entity;
            GetPaymentSummary();
        }

        private void SearchCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText) || SearchText.Length < 4 || SelectedEntity.EntityTypeId == 0)
            {
                return;
            }

            PaymentGridVisibility = Visibility.Hidden; 
            EntityGridVisibility = Visibility.Visible;
            EntityGridHeading = SelectedEntity.EntityTypeName + " Detail";
            SetIdentity(SelectedEntity.EntityTypeId);
            Entities = new ObservableCollection<Entity>(_repositoryManager.SearchEntity(SelectedEntity.EntityTypeId, SearchText, SiteId));
        }

        private void GetPaymentSummary()
        {
            PaymentGridVisibility = Visibility.Visible;
            PaymentGridHeading = _entity.Name + " Payment Detail";
            PaymentDetails = new ObservableCollection<DebitCreditOfPayment>(_repositoryManager.GetDebitCreditListOfEntity(_entity));
            
            if (PaymentDetails.Count == 0)
            {
                AmountRemain = _entity.TotalAmount;
                DebitAmount = _entity.TotalAmount;
                return;
            }

            AmountRemain = PaymentDetails.Last().DebitAmount;
            DebitAmount = PaymentDetails.Last().DebitAmount;
        }

        private void AddCommand(object obj)
        {
            ErrorMessage = "";
            var payment = obj as DebitCreditOfPayment;
            payment.SiteId = SiteId;
            payment.CreditAmount = CreditAmount;
            payment.DebitAmount = DebitAmount;
            payment.EntityId = _entity.EntityId;
            payment.EntityType = EntityTypes.Single(x => x.EntityTypeId == _entity.EntityTypeId);
            
            if (payment.CreditAmount < 1 || payment.SelectedMode == null || payment.PaymentDate == default(DateTime))
            {
                ErrorMessage = "*Please check the entry. Some field's Values are missing.";
                return;
            }

            _repositoryManager.AddPaymentForEntity(payment);
            PaymentDetails = new ObservableCollection<DebitCreditOfPayment>(_repositoryManager.GetDebitCreditListOfEntity(_entity));
            PaymentToAdd = new DebitCreditOfPayment();
            CreditAmount = 0;
            DebitAmount = payment.DebitAmount;
            AmountRemain = DebitAmount;
        }

        private ObservableCollection<DebitCreditOfPayment> _paymentDetails;

        public ObservableCollection<DebitCreditOfPayment> PaymentDetails
        {
            get { return _paymentDetails; }
            set { _paymentDetails = value; OnPropertyChanged(nameof(PaymentDetails)); }
        }

        private ObservableCollection<Entity> _entities;

        public ObservableCollection<Entity> Entities
        {
            get { return _entities; }
            set { _entities = value; OnPropertyChanged(nameof(Entities)); }
        }

        private List<EntityType> _entityTypes;

        public List<EntityType> EntityTypes
        {
            get { return _entityTypes; }
            set { _entityTypes = value; OnPropertyChanged(nameof(EntityTypes)); }
        }

        private List<PaymentMode> _paymentModes;

        public List<PaymentMode> PaymentModes
        {
            get { return _paymentModes; }
            set { _paymentModes = value; OnPropertyChanged(nameof(PaymentModes)); }
        }

        private string _identityHeader;

        public string IdentityHeader
        {
            get { return _identityHeader; }
            set { _identityHeader = value; OnPropertyChanged(nameof(IdentityHeader)); }
        }


        private decimal _creditAmount;

        public decimal CreditAmount
        {
            get { return _creditAmount; }
            set
            {
                _creditAmount = value;
                DebitAmount = AmountRemain - CreditAmount;
                OnPropertyChanged(nameof(CreditAmount));
            }
        }

        private decimal _debitAmount;

        public decimal DebitAmount
        {
            get { return _debitAmount; }
            set
            {
                _debitAmount = value;

                OnPropertyChanged(nameof(DebitAmount));
            }
        }

        private decimal _amountRemain;

        public decimal AmountRemain
        {
            get { return _amountRemain; }
            set { _amountRemain = value; OnPropertyChanged(nameof(AmountRemain)); }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private EntityType _selectedEntity;

        public EntityType SelectedEntity
        {
            get { return _selectedEntity; }
            set { _selectedEntity = value; OnPropertyChanged(nameof(SelectedEntity)); }
        }


        private DebitCreditOfPayment _paymentToAdd;

        public DebitCreditOfPayment PaymentToAdd
        {
            get { return _paymentToAdd; }
            set { _paymentToAdd = value; OnPropertyChanged(nameof(PaymentToAdd)); }
        }

        private Visibility _entityGridVisibility;

        public Visibility EntityGridVisibility
        {
            get { return _entityGridVisibility; }
            set { _entityGridVisibility = value; OnPropertyChanged(nameof(EntityGridVisibility)); }
        }

        private Visibility _paymentGidVisibility;

        public Visibility PaymentGridVisibility
        {
            get { return _paymentGidVisibility; }
            set { _paymentGidVisibility = value; OnPropertyChanged(nameof(PaymentGridVisibility)); }
        }

        private string _paymentGridHeading;

        public string PaymentGridHeading
        {
            get { return _paymentGridHeading; }
            set { _paymentGridHeading = value; OnPropertyChanged(nameof(PaymentGridHeading)); }
        }

        private string _entityGridHeading;

        public string EntityGridHeading
        {
            get { return _entityGridHeading; }
            set { _entityGridHeading = value; OnPropertyChanged(nameof(EntityGridHeading)); }
        }

        private void SetIdentity(int entityType)
        {
            switch (entityType)
            {
                case 1:
                    IdentityHeader = "House Number";
                    break;
                case 2:
                    IdentityHeader = "Bill Number";
                    break;
                case 3:
                    IdentityHeader = "Supervisor Duty";
                    break;
                default:
                    break;
            }
        }

        public RelayCommand Search { get; set; }
        public RelayCommand Add { get; set; }

        public RelayCommand EntityClick { get; set; }
    }
}
