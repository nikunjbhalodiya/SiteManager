using SiteManager.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiteManager.Core.Command;
using System.Windows;
using System;
using System.Linq;

namespace SiteManager.Core
{
    public class CustomerViewModel : ViewModelBase
    {

        public CustomerViewModel()
        {
            _customers = new ObservableCollection<Customer>(new List<Customer> { new Customer { CustomerId =1, HouseNumber="E-708", CustomerName= "Nik", EntryDate=DateTime.Today, ExtraCost=2000, ExtraWork="Window work", MobileNumber= "894562321", TotalCost=2500000 } });
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
            customer.EntryDate = DateTime.Now;
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
