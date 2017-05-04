using SiteManager.Core;
using SiteManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CustomerDetail.xaml
    /// </summary>
    public partial class CustomerDetail : Page
    {
        private readonly CustomerViewModel _viewModel; 
        public CustomerDetail()
        {
            InitializeComponent();
            _viewModel = new CustomerViewModel(SiteDetail.SiteId);
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += (msg) =>
            {
                var result = MessageBox.Show(msg, "Add", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            lblNameErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameErrMsg.Visibility = Visibility.Visible;
            }
        }

        private void txtHouseNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            lblHouseNumberErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtHouseNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHouseNumber.Text))
            {
                lblHouseNumberErrMsg.Visibility = Visibility.Visible;
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {

                var cust = item.Content as Customer;
                DebitCreditInformation debitCredit = new DebitCreditInformation(new Entity { Identity = cust.HouseNumber ,EntityId = cust.CustomerId, EntityTypeId = 1, Name = cust.CustomerName, Date = cust.CreatedDate, TotalAmount = cust.TotalCost, SiteId = SiteDetail.SiteId  });
                debitCredit.ShowDialog();
            }
        }
    }
}
