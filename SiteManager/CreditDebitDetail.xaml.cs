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
    /// Interaction logic for CreditDebitDetail.xaml
    /// </summary>
    public partial class CreditDebitDetail : Page
    {
        private DebitCreditOfPaymentViewModel _viewModel;
        public CreditDebitDetail()
        {
            InitializeComponent();
            DataContext = _viewModel = new DebitCreditOfPaymentViewModel();
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                _viewModel.EntityClick.Execute(item.Content);
                
            }
        }

        private void lstPaymentDetail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            lblSearchErrMsg.Visibility = Visibility.Hidden;
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text.Length < 4)
            {
                lblSearchErrMsg.Visibility = Visibility.Visible;
            }
        }

        private void cmbEntityType_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.SelectedIndex = 0;
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
    }
}
