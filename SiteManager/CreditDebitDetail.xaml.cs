using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiteManager
{
    /// <summary>
    /// Interaction logic for CreditDebitDetail.xaml
    /// </summary>
    public partial class CreditDebitDetail : Page
    {
        private List<CustomerModel> _customers;
        public CreditDebitDetail()
        {
            InitializeComponent();
            _customers = new List<CustomerModel>
            {
                new CustomerModel { CustomerId = 1, CustomerName="Nikunj B", DateOfPurchase= DateTime.Today, MobileNumber="9819536365" },
                new CustomerModel { CustomerId = 2, CustomerName="Nikunj S", DateOfPurchase= DateTime.Today.AddDays(-1), MobileNumber="9016548798" },
            };
            listViewDetail.ItemsSource = _customers;
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                
            }
        }

    }

    public class CustomerModel {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }

    public class PaymentDetail {
        public int Id { get; set; }
        public int CusotmerId { get; set; }

        public string CustomerName { get; set; }

        public string PaymentMode { get; set; }

        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
