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
        private int count;
        public CustomerViewModel(int siteId)
        {
            _repositoryManager = new RepositoryManager(new SqliteContext());
            SiteId = siteId;
            var customers = _repositoryManager.GetCustomerBySiteId(SiteId);
            count = customers.Any() ? customers.Last().CustomerId : 0;
            _customers = new ObservableCollection<Customer>(customers);
            Add = new RelayCommand(AddCommand);
            CustomerToAdd = new Customer();
        }

        private void AddCommand(object model)
        {
            var customer = model as Customer;
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.HouseNumber) || customer.TotalCost < 1)
            {
                return;
            }
            customer.CreatedDate = DateTime.Now;
            customer.CustomerId = count = count + 1; 
            customer.SiteId = SiteId;
            _repositoryManager.AddCustomer(customer);
            _customers.Add(customer);
            CustomerToAdd = new Customer();
        }

        public RelayCommand Add { get; set; }

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
