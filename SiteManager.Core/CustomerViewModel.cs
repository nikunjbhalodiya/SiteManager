using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;
using System.Linq;
using SiteManager.Repository;

namespace SiteManager.Core
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly RepositoryManager _repositoryManager;

        public CustomerViewModel(int siteId)
        {
            _repositoryManager = new RepositoryManager(new SqliteContext());
            SiteId = siteId;
            var customers = _repositoryManager.GetCustomerBySiteId(SiteId);
            _customers = new ObservableCollection<Customer>(customers);
            Add = new RelayCommand(AddCommand);
            Update = new RelayCommand(UpdateCommand);
            DeleteCustomer = new RelayCommand(DeleteCustomerCmd);
            CustomerToAdd = new Customer();
        }

        private void UpdateCommand(object obj)
        {
            ErrorMessage = "";
            var customer = obj as Customer;
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.HouseNumber) || customer.TotalCost < 1)
            {
                ErrorMessage = "*Please check the entry. Some field's Values are missing.";
                return;
            }
            customer.CreatedDate = DateTime.Now;
            customer.SiteId = SiteId;

            _repositoryManager.UpdateCustomer(customer);
            Customers = new ObservableCollection<Customer>(_repositoryManager.GetCustomerBySiteId(SiteId));
            CustomerToAdd = new Customer();

        }

        private void DeleteCustomerCmd(object obj)
        {
            var customer = obj as Customer;
            if (OnMessageBoxEvent("Do you want to delete this entry?"))
            {
                _repositoryManager.DeleteCustomer(customer);
                _customers.Remove(customer);
            }
        }

        private void AddCommand(object model)
        {
            ErrorMessage = "";
            var customer = model as Customer;
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.HouseNumber) || customer.TotalCost < 1)
            {
                ErrorMessage = "*Please check the entry. Some field's Values are missing.";
                return;
            }
            customer.CreatedDate = DateTime.Now;
            customer.SiteId = SiteId;
            if (_customers.Any(x => x.HouseNumber == customer.HouseNumber))
            {
                if (!OnMessageBoxEvent("House Number you have entered is already booked. Do you still want to continue to add this customer?"))
                {
                    return;
                }
            }

            _repositoryManager.AddCustomer(customer);
            Customers = new ObservableCollection<Customer>(_repositoryManager.GetCustomerBySiteId(SiteId));
            CustomerToAdd = new Customer();
        }

        public RelayCommand Add { get; set; }

        public RelayCommand Update { get; set; }

        public RelayCommand DeleteCustomer { get; set; }

        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer>  Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged(nameof(Customers)); }
        }

        private Customer _customerToAdd;

        public Customer CustomerToAdd
        {
            get { return _customerToAdd; }
            set { _customerToAdd = value; OnPropertyChanged(nameof(CustomerToAdd)); }
        }


    }
}
