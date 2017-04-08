using SiteManager.Core;
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
    /// Interaction logic for MaterialDetail.xaml
    /// </summary>
    public partial class MaterialDetail : Page
    {
        private readonly MaterialViewModel _viewModel;
        public MaterialDetail()
        {
            InitializeComponent();
            _viewModel = new MaterialViewModel();
            DataContext = _viewModel;
            _viewModel.MessageBoxEvent += () =>
            {
                var result = MessageBox.Show("Would you like to delete it?", "Delete", MessageBoxButton.YesNo);
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

        private void txtBillNbr_GotFocus(object sender, RoutedEventArgs e)
        {
            lblBillErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtBillNbr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillNbr.Text))
                lblBillErrMsg.Visibility = Visibility.Visible;
        }

        private void txtQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void txtBillAmt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtVendorName_GotFocus(object sender, RoutedEventArgs e)
        {
            lblNameErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtVendorName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVendorName.Text))
                lblNameErrMsg.Visibility = Visibility.Visible;
        }

        private void txtAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            lblAddressErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
                lblAddressErrMsg.Visibility = Visibility.Visible;
        }

        private void btnAddVendor_Click(object sender, RoutedEventArgs e)
        {
            VenorGrid.IsEnabled = true;
        }

        private void btnAddVendorDetail_Click(object sender, RoutedEventArgs e)
        {
            VenorGrid.IsEnabled = _viewModel.IsVendorGridEnabled;
        }
    }
}
