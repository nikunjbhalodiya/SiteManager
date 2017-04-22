using SiteManager.Core.Command;
using SiteManager.Core.Model;
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
        public DebitCreditOfPaymentViewModel()
        {
            PaymentToAdd = new DebitCreditOfPayment();
            Add = new RelayCommand(AddCommand);
            Search = new RelayCommand(SearchCommand);
            PaymentGridVisibility = Visibility.Hidden;
            EntityGridVisibility = Visibility.Hidden;
            _entityTypes = new List<EntityType> { new EntityType { EntityId=0, EntityName="Select" }, new EntityType { EntityId=1, EntityName="Customer" }, new EntityType { EntityId =2, EntityName= "Material" } };
            _paymentModes = new List<PaymentMode> { new PaymentMode { Content = "Cash", IsSelected=false }, new PaymentMode { Content="Cheque", IsSelected=false }, new PaymentMode { Content="Loan", IsSelected=false } };
            _entities = new ObservableCollection<Entity>();
            EntityClick = new RelayCommand(EntityClickCmd);

        }

        private void EntityClickCmd(object obj)
        {
            var entity = obj as Entity;
            PaymentGridVisibility = Visibility.Visible;
            PaymentGridHeading = entity.Name + " Payment Detail";
            PaymentDetails = new ObservableCollection<DebitCreditOfPayment>(new List<DebitCreditOfPayment> { new DebitCreditOfPayment { Name="Nikunj", PaymentDate= DateTime.Today, SelectedMode= new PaymentMode { Content ="Cash" }, CreditAmount= 1000000, DebitAmount=1500000 } });
            TotalAmount = entity.TotalAmount;
            if (PaymentDetails.Count == 0)
            {
                CreditAmount = entity.TotalAmount;
                return;
            }
            
            CreditAmount = PaymentDetails.Last().CreditAmount;
        }

        private void SearchCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText) || SearchText.Length < 4 || SelectedEntity.EntityId == 0)
            {
                return;
            }

            EntityGridVisibility = Visibility.Visible;
            EntityGridHeading = SelectedEntity.EntityName + " Detail";

            Entities = new ObservableCollection<Entity>(new List<Entity> { new Entity { Name="Nikunj", Date = DateTime.Today, TotalAmount=2500000 } });

        }

        private void AddCommand(object obj)
        {
            var payment = obj as DebitCreditOfPayment;
            payment.CreditAmount = CreditAmount;
            payment.DebitAmount = DebitAmount;
            if (string.IsNullOrWhiteSpace(payment.Name) || payment.DebitAmount < 1 || payment.SelectedMode == null || payment.PaymentDate == default(DateTime))
            {
                return;
            }

            _paymentDetails.Add(payment);
            PaymentToAdd = new DebitCreditOfPayment();
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
            set { _paymentModes = value;  OnPropertyChanged(nameof(PaymentModes)); }
        }

        private decimal _creditAmount;

        public decimal CreditAmount
        {
            get { return _creditAmount; }
            set { _creditAmount = value; OnPropertyChanged(nameof(CreditAmount)); }
        }

        private decimal _debitAmount;

        public decimal DebitAmount
        {
            get { return _debitAmount; }
            set
            {
                _debitAmount = value;
                CreditAmount = TotalAmount - DebitAmount;
                OnPropertyChanged(nameof(DebitAmount));
            }
        }

        private decimal _totalAmount;

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
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


        public RelayCommand Search { get; set; }
        public RelayCommand Add { get; set; }

        public RelayCommand EntityClick { get; set; }
    }
}
